using UnityEngine;
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

	//random number between VariableRatio-2 and VariableRatio+2
	private int randomVariation;
	private int clickCount;

	public VariableRatioModule(int _variableRatio, string target){
		this.variableRatio = _variableRatio;
		this.targetButton = target;
		randomVariation = this.generateRandomVR();
		clickCount = 0;
		Debug.Log ("Random Number: " + this.randomVariation);
		Debug.Log ("Target Button: " + this.TargetButton);
	}

	public override void ButtonClicked (string buttonColor){
		//search for the button...
		//Debug.Log ("button clicked");
		if (this.ButtonCount.ContainsKey (buttonColor)) {
			//Debug.Log ("button Found");
			//...increment the counter
			this.ButtonCount[buttonColor]++;
			//if the user achieve the variable ratio...
			if(this.targetButton == buttonColor){
				if(clickCount == randomVariation){
					//... he earns 1 point
					this.Score++;
					//and the click counter reset
					this.clickCount = 0;
					//generates a new random number
					this.randomVariation = this.generateRandomVR();
					Debug.Log ("Score: " + this.Score);
					Debug.Log ("random variation: " + this.randomVariation);
				}
				else{
					//increment the click counter
					this.clickCount++;
				}
			}
		}
	}

	public override void OutPutData(string filename){
		//Output the computed data when the module ends;
	}

	private int generateRandomVR(){
		return Random.Range (this.variableRatio - 2, (this.variableRatio + 2) + 1);
	}
}
