using System.Collections.Generic;

public class VariableRatioModule : BaseModule{

	private string targetButton;
	public string TargetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}
	
	private int variableRatio;
	public int VariableRatio{
		get { return variableRatio; }
		set { variableRatio = value; }
	}

	public VariableRatioModule(){

	}

	public override void OutPutData(string filename){
		//Output the computed data when the module ends;
	}

}
