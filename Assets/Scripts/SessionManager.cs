using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour {

	public PanelVariableRatioManager variableRatioManager;
	public PanelDROManager DROManager;
	public PanelDROVRManager DROVRManager;
	public PanelExtinctionManager extinctionManager;
	public PanelFixedTimeManager fixedTimeManager;
	public PanelPenaltyManager penaltyManager;

	//User data
	public InputField username;
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

			Debug.Log ("DRO ORDER: " + order);

			modules.Add(dro);
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
}



















