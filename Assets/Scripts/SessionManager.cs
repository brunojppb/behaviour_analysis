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
		if (variableRatioManager.enabled) {
			int variableRatio = int.Parse(variableRatioManager.variableRatio.text.ToString());
			string targetButton = variableRatioManager.ButtonSelected;
			VariableRatioModule vr = new VariableRatioModule(variableRatio, targetButton);

			vr.ExecutionTime = int.Parse(variableRatioManager.executionTime.text.ToString());
			vr.Order = int.Parse(variableRatioManager.order.text.ToString());

			modules.Add(vr);
		}

		//.......
		if(DROManager.enabled){
			//instantiate the module and put into the array
		}
		//........

		//sort the modules to execute following the order that was inputted
		this.modules.Sort ();

		//start a coroutine to iterate each module with its own time
		StartCoroutine("ExecuteModules");
	}

	IEnumerator ExecuteModules(){
		int moduleIndex = 0;
		while (moduleIndex < this.modules.Count) {
			BaseModule actualModule = modules[moduleIndex];
			Debug.Log("Module " + actualModule.GetType().Name + " Running...");

			//add a callback method to each button based on the module
			foreach(Button button in this.buttons){
				this.addListener(button, actualModule);
			}

			//if the actual module is a instance of VariableRatioModule
			//execute its specialized methods
			//if(modules[moduleIndex] is VariableRatioModule){
				//VariableRatioModule vr = modules[moduleIndex] as VariableRatioModule;

			//}

			//execute the module using its own execution time
			yield return new WaitForSeconds(modules[moduleIndex].ExecutionTime);

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
			module.OutPutData("result.txt");
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



















