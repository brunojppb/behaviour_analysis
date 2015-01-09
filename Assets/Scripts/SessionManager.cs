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
	public PanelPenaltyVRManager penaltyVRManager;

	//PANEL GAMEOBJECTS
	public GameObject variableRatio;
	public GameObject DRO;
	public GameObject DROVR;
	public GameObject fixedTime;
	public GameObject extinction;
	public GameObject penalty;
	public GameObject penaltyVR;

	public GameObject EndOfSessionPanel;
	public GameObject EndOfSessionResetButton;
	public GameObject EndOfSessionQuitButton;

	//Sounds
	public AudioSource buttonClick;
	public AudioSource getPoint;
	public AudioSource losePoint;

	//User data
	public InputField participantName;
	public InputField sessionNumber;
	public InputField numberOfLoopsInput;
	public Text score;

	//number of loops the program will execute
	private int numberOfLoops;

	//list of buttons that will perform actions based on modules
	public Button[] buttons;

	//list of modules that will execute 
	private List<BaseModule> modules;

	//Total time of the session to generate a log
	private int sessionTime;
	private float actualSessionTime;
	private int actualScore;

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

		//=================================================
		//Sixth Module - Penalty
		//================================================
		if (penalty.activeSelf) {
			int execTime = int.Parse(this.penaltyManager.executionTime.text.ToString());
			int order = int.Parse(this.penaltyManager.order.text.ToString());
			string targetButton = this.penaltyManager.TargetButton;

			PenaltyModule penaltyModule = this.penaltyManager.gameObject.AddComponent("PenaltyModule") as PenaltyModule;
			penaltyModule.ExecutionTime = execTime;
			penaltyModule.Order = order;
			penaltyModule.TargetButton = targetButton;

			this.modules.Add(penaltyModule);
		}

		//=================================================
		//Seventh Module - Penalty + VR
		//================================================
		if (penaltyVR.activeSelf) {
			int execTime = int.Parse(this.penaltyVRManager.executionTime.text.ToString());
			int order = int.Parse(this.penaltyVRManager.order.text.ToString());
			string targetButton = this.penaltyVRManager.ButtonSelected;
			int vr = int.Parse(this.penaltyVRManager.variableRatio.text.ToString());

			PenaltyVRModule penaltyVRModule = this.penaltyVRManager.gameObject.AddComponent("PenaltyVRModule") as PenaltyVRModule;
			penaltyVRModule.ExecutionTime = execTime;
			penaltyVRModule.Order = order;
			penaltyVRModule.TargetButton = targetButton;
			penaltyVRModule.VariableRatio = vr;

			this.modules.Add(penaltyVRModule);

		}

		//======================================================
		//after process each module
		//======================================================
		//sort them to execute following the order
		this.modules.Sort ();

		//compute the session time
		this.sessionTime = 0;
		foreach (BaseModule m in this.modules)
			this.sessionTime += m.ExecutionTime;

		//initialize the Log string
		this.sessionLog = "\nEVENT RECORDING START\n\n";

		//start a coroutine to iterate each module with its own time
		StartCoroutine("ExecuteModules");
	}

	IEnumerator ExecuteModules(){
		//begin the output file
		string fileName = string.Format("session_{0}_partipant_{1}.txt", sessionNumber.text, participantName.text);
		this.outputParticipantData (fileName);

		//setting up the number of loops the program will execut
		this.numberOfLoops = this.numberOfLoopsInput.text == "" ? 1 : int.Parse (this.numberOfLoopsInput.text);

		for (int i = 0; i < numberOfLoops; i++) {
			//start a Coroutine to decrement the session time and generate a log
			StopCoroutine("SessionTimer");
			StartCoroutine ("SessionTimer");

			this.sessionLog += string.Format("\nLOOP {0}:\n\n", i+1);
			int moduleIndex = 0;
			while (moduleIndex < this.modules.Count) {
				BaseModule actualModule = modules[moduleIndex];
				Debug.Log("Module " + actualModule.ToString() + " Running...");
				Debug.Log("Exec Time: " + actualModule.ExecutionTime);
				
				//start specialized functions for each module
				actualModule.StartModule();
				
				//before to add listeners, write on the Log the actual module
				this.sessionLog +=  string.Format("Module: {0}:\n", actualModule.ToString());
				
				//add a callback method to each button based on the module
				foreach(Button button in this.buttons){
					this.addListener(button, actualModule);
				}
				
				//execute the module using its own execution time
				yield return new WaitForSeconds(modules[moduleIndex].ExecutionTime + 1);
				
				//stop specialized functions for each module
				actualModule.StopModule();
				
				//remove all callbacks from the buttons
				foreach(Button button in this.buttons){
					button.onClick.RemoveAllListeners();
				}

				//write on the file the results
				//write loop information on file
				this.outputSesionData(fileName);
				
				//write each module on the log
				foreach(BaseModule module in modules)
					module.OutputData(fileName);
				
				//jump for the next module
				moduleIndex++;
			}
		}

		//output the total session information


		//End of the session
		Debug.Log("End of the session...");

		//show the end of the session panel
		//and let the user exit the program or restart
		StartCoroutine ("ShowEndOfTheSessionPanel");
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
				//Call the score animation and sound to earn points
				this.getPoint.Play();
			}
			else if(scoreSum < actualScore){
				this.score.text = scoreSum.ToString ();
				//Call the score animation and sound to lose points
				this.losePoint.Play();
			}
				
		}
	}

	void ButtonClickedOnSession(string buttonName){
		this.sessionLog += buttonName + "\t" + this.actualSessionTime.ToString("0.0") + " s\n";
		//button sound
		this.buttonClick.Play ();
	}

	//===================================================================
	//Trigger a session timer to catch each button clicked and the time
	// it was clicked
	//===================================================================
	IEnumerator SessionTimer(){
		this.actualSessionTime = 0;
		while (this.actualSessionTime < this.sessionTime) {
			yield return new WaitForSeconds(0.1f);
			this.actualSessionTime += 0.1f;

		}
	}

	void outputSesionData(string filename){

		using (StreamWriter file = new StreamWriter (filename, true)) {
			this.sessionLog = this.sessionLog.Replace("\n", System.Environment.NewLine);
			file.WriteLine(this.sessionLog);
		}
	}

	void outputParticipantData(string filename){
		using (StreamWriter file = new StreamWriter (filename, true)) {
			string text = "";
			text += "\nParticipant Name: " + this.participantName.text.ToString();
			text += "\nSession number: " + this.sessionNumber.text.ToString();
			text += "\nTotal Score: " + this.score.text.ToString() + " point(s)";
			text += "\nSession Time: " + this.sessionTime + " seconds";
			text += "\nNumber of Loops: " + this.numberOfLoops;
			text += "\n\nSession LOG:";
			text = text.Replace("\n", System.Environment.NewLine);
			file.WriteLine(text);
		}
	}

	void outputTotalSessionInformation(string filename){
		ModuleReport report = new ModuleReport ();
		foreach (BaseModule module in modules) {
			report.Score += module.Report.Score;
			foreach(KeyValuePair<string, int> entry in module.ButtonCount){
				report.ButtonCount[entry.Key] += entry.Value;
			}
		}

		using (StreamWriter file = new StreamWriter (filename, true)) {
			string text = "\n\n===============SESSION OUTPUT=======================\n\n";
			text = "\nTotal Score: " + report.Score;
			text += "";
			text = text.Replace("\n", System.Environment.NewLine);
			file.WriteLine(text);
		}
	}

	//===================================================================
	//Show the end of session panel and than wait for 3 seconds
	//to show the buttons. (prevent the user to click on them by mistake)
	//===================================================================
	IEnumerator ShowEndOfTheSessionPanel(){
		this.EndOfSessionPanel.SetActive (true);
		this.EndOfSessionResetButton.SetActive (false);
		this.EndOfSessionQuitButton.SetActive (false);
		yield return new WaitForSeconds (3);
		this.EndOfSessionResetButton.SetActive (true);
		this.EndOfSessionQuitButton.SetActive (true);
	}
}



















