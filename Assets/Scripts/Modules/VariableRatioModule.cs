using UnityEngine;
using System.Collections.Generic;

public class VariableRatioModule : BaseModule {


	public string targetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}

	public int variableRatio{
		get { return variableRatio; }
		set { variableRatio = value; }
	}

	public override void outputData(string filename){
		//Output the computed data when the module ends;
	}

}
