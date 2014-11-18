using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour {

	public PanelVariableRatioManager variableRatioManager;
	public PanelDROManager DROManager;
	public PanelDROVRManager DROVRManager;
	public PanelExtinctionManager extinctionManager;
	public PanelFixedTimeManager fixedTimeManager;
	public PanelPenaltyManager penaltyManager;
	
	private List<BaseModule> modules;

	public void SetUpSession(){

		//initialize a list of modules
		this.modules = new List<BaseModule> ();

		//check if each module will run on the session
		//and setup each Module Object with its paramters
		if (variableRatioManager.enabled) {
			VariableRatioModule vr = new VariableRatioModule();
			vr.ExecutionTime = int.Parse(variableRatioManager.executionTime.text.ToString());
			vr.Order = int.Parse(variableRatioManager.order.text.ToString());
			vr.TargetButton = variableRatioManager.ButtonSelected;
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
			yield return new WaitForSeconds(modules[moduleIndex].ExecutionTime);
		}
	}
}
