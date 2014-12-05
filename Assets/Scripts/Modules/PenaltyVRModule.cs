﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PenaltyVRModule : BaseModule {

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

	public override void StartModule (){ 
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

	public override void ButtonClicked (string buttonColor){
		//search for the button...
		//Debug.Log ("button clicked");
		if (this.ButtonCount.ContainsKey (buttonColor)) {
			//Debug.Log ("button Found");
			//...increment the counter
			this.ButtonCount[buttonColor]++;
			//if the user achieve the target button...
			if(this.targetButton == buttonColor){

				//... he loses 1 point ( Penalty Module Logic)
				this.Score--;

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

	private int generateRandomVR(){
		return Random.Range (this.variableRatio - 2, (this.variableRatio + 2) + 1);
	}

	public override void StopModule (){ 
		Debug.Log ("Penalty VR Module Stoped"); 
	}

	public override void OutputData(string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "Penalty + VR Module";
		text += "\n=================================================\n";
		text += "VR: " + this.VariableRatio;
		text += "\nExecution Time: " + timeInMinutes + " minutes";
		text += "\nTarget Button: " + this.TargetButton;
		text += "\nScore: " + this.Score;
		// The using statement automatically closes the stream and calls  
		// IDisposable.Dispose on the stream object. 
		using (StreamWriter file = new StreamWriter (filename, true)) {
			text = text.Replace("\n", System.Environment.NewLine);
			file.WriteLine (text);
			string tableTitle = "Button\t\tResponse Count\t\tResponse Rate(responses per minute)";
			tableTitle = tableTitle.Replace("\n", System.Environment.NewLine);
			file.WriteLine (tableTitle);
			foreach(string key in this.ButtonCount.Keys){
				string newLine = key + "\t\t" + this.ButtonCount[key] + "\t\t\t" + this.ButtonCount[key] / timeInMinutes;
				newLine = newLine.Replace("\n", System.Environment.NewLine);
				file.WriteLine(newLine);
			}
			
		}
	}

	public override string ToString ()
	{
		return "Penalty + VR";
	}

}