﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PenaltyModule : BaseModule {

	private string targetButton;
	public string TargetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}

	private int pointsToDelivery;
	public int PointsToDelivery{
		get {return pointsToDelivery;}
		set {pointsToDelivery = value;}
	}

	public override void StartModule (){ 

		this.Score = 0;
		
		if (this.Report == null)
			this.Report = new ModuleReport();

		this.Report.EarnedPoints = 0;
		this.Report.LostPoints = 0;

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
	}

	public override void StopModule (){
		//loop through the buttons to sum total cliks
		foreach(string key in this.ButtonCount.Keys){
			this.Report.ButtonCount[key] += this.ButtonCount[key];
		}
	}

	public override void ButtonClicked (string buttonColor){ 
		//search for the button...
		//Debug.Log ("button clicked");
		if (this.ButtonCount.ContainsKey (buttonColor)) {
			//Debug.Log ("button Found");
			//...increment the counter
			this.ButtonCount[buttonColor]++;
			//if the user clicks on the target button
			if(this.targetButton == buttonColor){
					//... he loses 1 point
					this.Score -= this.pointsToDelivery;
			}
		} 
	}

	public override void OutputData (string filename){ 
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "Penalty Module";
		text += "\n=================================================\n";
		text += "\nExecution Time: " + timeInMinutes + " minutes";
		text += "\nTarget Button: " + this.TargetButton;
		text += "\nEarned Points: " + this.Report.EarnedPoints;
		text += "\nLost Points: " + this.Report.LostPoints;
		text += "\nScore: " + this.Score;;
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
		return "Penalty";
	}
}
