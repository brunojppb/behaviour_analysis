﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SessionManager : MonoBehaviour {

	//PANEL MODULES
	public PanelVariableRatioManager variableRatioManager;
	public PanelDROManager DROManager;
	public PanelDROVRManager DROVRManager;
	public PanelExtinctionManager extinctionManager;
	public PanelFixedTimeManager fixedTimeManager;
	public PanelPenaltyManager penaltyManager;

	//PANEL GAMEOBJECTS
	public GameObject variableRatio;
	public GameObject DRO;
	public GameObject DROVR;
	public GameObject fixedTime;
	public GameObject extinction;

	//User data
	public InputField participantName;
	public Text score;

	//list of buttons that will perform actions based on modules
	public Button[] buttons;

	//list of modules that will execute 
	private List<BaseModule> modules;

	//Total time of the session to generate a log
	private int sessionTime;
	private int actualSessionTime;

	//Log generated after each click
	private string sessionLog;

	public void SetUpSession(){

		//initialize a list of modules
		this.modules = new List<BaseModule> ();

		//check if each module will run on the session
		//and setup each Module Object with its paramters
		//=================================================
		//First module - Variable Ratio
		//=================================================
		if (this.variableRatio.activeSelf) {
			int variableRatio = int.Parse(variableRatioManager.variableRatio.text.ToString());
			string targetButton = variableRatioManager.ButtonSelected;
			int execTime = int.Parse(variableRatioManager.executionTime.text.ToString());
			int order = int.Parse(variableRatioManager.order.text.ToString());

			VariableRatioModule vr = this.variableRatioManager.gameObject.AddComponent("VariableRatioModule") as VariableRatioModule;
			vr.VariableRatio = variableRatio;
			vr.TargetButton = targetButton;
			vr.ExecutionTime = execTime;
			vr.Order = order;

			modules.Add(vr);
		}

		//=================================================
		//Second module - DRO
		//=================================================
		if(DRO.activeSelf){
			//instantiate the module and put into the array
			int timeInterval = int.Parse(this.DROManager.timeInteval.text.ToString());
			string targetButton = this.DROManager.ButtonSelected;
			int execTime = int.Parse(this.DROManager.executionTime.text.ToString());
			int order = int.Parse(this.DROManager.order.text.ToString());

			DROModule dro = this.DROManager.gameObject.AddComponent("DROModule") as DROModule;
			dro.TimeInterval = timeInterval;
			dro.TargetButton = targetButton;
			dro.ExecutionTime = execTime;
			dro.Order = order;

			modules.Add(dro);
		}

		//=================================================
		//Third module - DROVR
		//=================================================
		if (DROVR.activeSelf) {
			int timeInterval = int.Parse(this.DROVRManager.timeInterval.text.ToString());
			int variableRatio = int.Parse(this.DROVRManager.variableRatio.text.ToString());
			string targetButton = this.DROVRManager.TargetButton;
			//string droTargetButton = this.DROVRManager.DroTargetButtonSelected;
			int execTime = int.Parse(this.DROVRManager.executionTime.text.ToString());
			int order = int.Parse(this.DROVRManager.order.text.ToString());

			DROVRModule droVr = this.DROVRManager.gameObject.AddComponent("DROVRModule") as DROVRModule;
			droVr.TimeInterval = timeInterval;
			droVr.VariableRatio = variableRatio;
			droVr.TargetButton = targetButton;
			//droVr.DroTargetButton = droTargetButton;
			droVr.ExecutionTime = execTime;
			droVr.Order = order;

			modules.Add(droVr);
		}

		//=================================================
		//Fourth Module - Fixed Time
		//================================================
		if (fixedTime.activeSelf) {
			int timeInterval = int.Parse(this.fixedTimeManager.timeInteval.text.ToString());
			int execTime = int.Parse(this.fixedTimeManager.executionTime.text.ToString());
			int order = int.Parse(this.fixedTimeManager.order.text.ToString());

			FixedTimeModule fixedTimeModule = this.fixedTimeManager.gameObject.AddComponent("FixedTimeModule") as FixedTimeModule;
			fixedTimeModule.TimeInterval = timeInterval;
			fixedTimeModule.ExecutionTime = execTime;
			fixedTimeModule.Order = order;

			modules.Add(fixedTimeModule);
		}

		//=================================================
		//Fifth Module - Extinction
		//================================================
		if (extinction.activeSelf) {
			int execTime = int.Parse(this.extinctionManager.executionTime.text.ToString());
			int order = int.Parse(this.extinctionManager.order.text.ToString());

			ExtinctionModule extinctionModule = this.extinctionManager.gameObject.AddComponent("ExtinctionModule") as ExtinctionModule;
			extinctionModule.ExecutionTime = execTime;
			extinctionModule.Order = order;

			this.modules.Add(extinctionModule);
		}
		//........

		//after process each module
		//sort them to execute following the order
		this.modules.Sort ();

		//compute the session time
		this.sessionTime = 0;
		foreach (BaseModule m in this.modules)
			this.sessionTime += m.ExecutionTime;

		//initialize the Log
		this.sessionLog = "\nEVENT RECORDING START\n";

		//start a Coroutine to decrement the session time and generate a log
		StartCoroutine ("SessionTimer");

		//start a coroutine to iterate each module with its own time
		StartCoroutine("ExecuteModules");
	}

	IEnumerator ExecuteModules(){
		int moduleIndex = 0;
		while (moduleIndex < this.modules.Count) {
			BaseModule actualModule = modules[moduleIndex];
			Debug.Log("Module " + actualModule.ToString() + " Running...");
			Debug.Log("Exec Time: " + actualModule.ExecutionTime);

			//start specialized functions for each module
			actualModule.StartModule();

			//before to add listeners, write on the Log the actual module
			this.sessionLog += "\n" + actualModule.ToString() + ":\n";

			//add a callback method to each button based on the module
			foreach(Button button in this.buttons){
				this.addListener(button, actualModule);
			}

			//execute the module using its own execution time
			yield return new WaitForSeconds(modules[moduleIndex].ExecutionTime);

			//stop specialized functions for each module
			actualModule.StopModule();

			//remove all callbacks from the buttons
			foreach(Button button in this.buttons){
				button.onClick.RemoveAllListeners();
			}

			//jump for the next module
			moduleIndex++;
		}

		//End of the session
		Debug.Log("End of the session...");
		//write in file the results

		//write session information
		this.outputSesionData("result.txt");

		//write each module
		foreach(BaseModule module in modules)
			module.OutputData("result.txt");
	}


	//Add a callback to the button
	void addListener(Button b, BaseModule module){
	
		b.onClick.AddListener (() => module.ButtonClicked(b.name));
		b.onClick.AddListener (() => this.ButtonClickedOnSession(b.name));

	}


	void Update(){
		//update the user score
		int actualScore = int.Parse (score.text.ToString ());

		if (this.modules != null && this.modules.Count > 0) {
			int scoreSum = 0;
			foreach (BaseModule module in this.modules) {
				scoreSum += module.Score;
			}

			if(scoreSum > actualScore){
				this.score.text = scoreSum.ToString ();
				//TODO - Call the score animation and sound to earn points
			}
			else if(scoreSum < actualScore){
				this.score.text = scoreSum.ToString ();
				//TODO - Call the score animation and sound to lose points
			}
				
		}
	}

	void ButtonClickedOnSession(string buttonName){
		this.sessionLog += buttonName + "\t" + this.actualSessionTime + " s\n";
	}

	IEnumerator SessionTimer(){
		this.actualSessionTime = 0;
		while (this.actualSessionTime < this.sessionTime) {
			yield return new WaitForSeconds(1);
			this.actualSessionTime++;
		}
	}

	void outputSesionData(string filename){
		using (StreamWriter file = new StreamWriter (filename, true)) {
			string text = "";
			text += "Participant Name: " + this.participantName.text.ToString();
			text += "\nTotal Score: " + this.score.text.ToString();
			text += "\nSession LOG:";
			file.WriteLine(text);
			file.WriteLine(this.sessionLog);

		}
	}
}



















