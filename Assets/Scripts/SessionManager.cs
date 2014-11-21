using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SessionManager : MonoBehaviour {

	public PanelVariableRatioManager variableRatioManager;
	public PanelDROManager DROManager;
	public PanelDROVRManager DROVRManager;
	public PanelExtinctionManager extinctionManager;
	public PanelFixedTimeManager fixedTimeManager;
	public PanelPenaltyManager penaltyManager;

	//User data
	public InputField participantName;
	public Text score;

	//list of buttons that will perform actions based on modules
	public Button[] buttons;

	//list of modules that will execute 
	private List<BaseModule> modules;

	public void SetUpSession(){

		//initialize a list of modules
		this.modules = new List<BaseModule> ();

		//check if each module will run on the session
		//and setup each Module Object with its paramters
		//=================================================
		//First module - Variable Ratio
		//=================================================
		if (variableRatioManager.enabled) {
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
		if(DROManager.enabled){
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
		if (DROVRManager.enabled) {
			int timeInterval = int.Parse(this.DROVRManager.timeInterval.text.ToString());
			int variableRatio = int.Parse(this.DROVRManager.variableRatio.text.ToString());
			string vrTargetButton = this.DROVRManager.VrTargetButtonSelected;
			string droTargetButton = this.DROVRManager.DroTargetButtonSelected;
			int execTime = int.Parse(this.DROVRManager.executionTime.text.ToString());
			int order = int.Parse(this.DROVRManager.order.text.ToString());

			DROVRModule droVr = this.DROVRManager.gameObject.AddComponent("DROVRModule") as DROVRModule;
			droVr.TimeInterval = timeInterval;
			droVr.VariableRatio = variableRatio;
			droVr.VrTargetButton = vrTargetButton;
			droVr.DroTargetButton = droTargetButton;
			droVr.ExecutionTime = execTime;
			droVr.Order = order;

			modules.Add(droVr);
		}
		//........

		//after process each module
		//sort them to execute following the order
		this.modules.Sort ();

		//start a coroutine to iterate each module with its own time
		StartCoroutine("ExecuteModules");
	}

	IEnumerator ExecuteModules(){
		int moduleIndex = 0;
		while (moduleIndex < this.modules.Count) {
			BaseModule actualModule = modules[moduleIndex];
			Debug.Log("Module " + actualModule.GetType().Name + " Running...");
			Debug.Log("Exec Time: " + actualModule.ExecutionTime);
			//add a callback method to each button based on the module
			foreach(Button button in this.buttons){
				this.addListener(button, actualModule);
			}

			//start specialized functions for each module
			actualModule.StartModule();

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

	}


	void Update(){
		//update the user score
		if (this.modules != null && this.modules.Count > 0) {
			int scoreSum = 0;
			foreach (BaseModule module in this.modules) {
				scoreSum += module.Score;
			}
			this.score.text = scoreSum.ToString ();
		}
	}

	void outputSesionData(string filename){
		using (StreamWriter file = new StreamWriter (filename, true)) {
			string text = "";
			text += "Participant Name: " + this.participantName.text.ToString();
			text += "\nTotal Score: " + this.score.text.ToString();
			file.WriteLine(text);
		}
	}
}



















