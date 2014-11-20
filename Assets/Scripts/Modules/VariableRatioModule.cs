using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

//	public VariableRatioModule(int _variableRatio, string target, int execTime, int order){
//		this.variableRatio = _variableRatio;
//		this.targetButton = target;
//		this.ExecutionTime = execTime;
//		this.Order = order;
//		randomVariation = this.generateRandomVR();
//		clickCount = 0;
//		Debug.Log ("Random Number: " + this.randomVariation);
//		Debug.Log ("Target Button: " + this.TargetButton);
//	}

	public override void StartModule(){
		//initialize the button counter
		this.ButtonCount = new Dictionary<string, int> ();
		this.ButtonCount.Add ("blue", 0);
		this.ButtonCount.Add ("green", 0);
		this.ButtonCount.Add ("yellow", 0);
		this.ButtonCount.Add ("pink", 0);
		this.ButtonCount.Add ("black", 0);
		this.ButtonCount.Add ("red", 0);
		this.ButtonCount.Add ("purple", 0);
		this.ButtonCount.Add ("orange", 0);
		this.ButtonCount.Add ("white", 0);



		randomVariation = this.generateRandomVR();
		clickCount = 0;
		Debug.Log ("Random Number: " + this.randomVariation);
		Debug.Log ("Target Button: " + this.TargetButton);
	}

	public override void StopModule(){}

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


	public override void OutputData(string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "VR Module";
		text += "\n=================================================\n";
		text += "VR: " + this.VariableRatio;
		text += "\nExecution Time: " + timeInMinutes + " minutes";
		text += "\nTarget Button: " + this.TargetButton;
		// The using statement automatically closes the stream and calls  
		// IDisposable.Dispose on the stream object. 
		using (StreamWriter file = new StreamWriter (filename, true)) {
			file.WriteLine (text);
			foreach(string key in this.ButtonCount.Keys){
				file.WriteLine("\nButton: " + key);
				file.WriteLine("Response Count: " + this.ButtonCount[key]);
				file.WriteLine("Response Rate: " + this.ButtonCount[key] / timeInMinutes + " responses per minute");
			}

		}
	}

	private int generateRandomVR(){
		return Random.Range (this.variableRatio - 2, (this.variableRatio + 2) + 1);
	}
}
