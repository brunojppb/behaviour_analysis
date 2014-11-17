using UnityEngine;
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
		this.modules = new List<BaseModule> ();
		//check witch module will run on the session
		//and setup each Module Object with its paramters
		if (variableRatioManager.enabled) {
			VariableRatioModule v = new VariableRatioModule();
			modules.Add(v);
		}

		if(DROManager.enabled){
			//instantiate the module and put into the array
		}
	}


}
