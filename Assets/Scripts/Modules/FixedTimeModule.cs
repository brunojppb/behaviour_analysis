﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FixedTimeModule : BaseModule {

	private int timeInteval;
	public int TimeInterval{
		get {return timeInteval;}
		set {timeInteval = value;}
	}

	private int pointsToDelivery;
	public int PointsToDelivery{
		get {return pointsToDelivery;}
		set {pointsToDelivery = value;}
	}

	//Delive points every timeInterval (seconds)
	IEnumerator DeliveryPoints(){
		while (true) {
			yield return new WaitForSeconds(TimeInterval);
			this.Score += this.pointsToDelivery;
		}
	}

	private void RepeatPoints() {
		this.Score += this.pointsToDelivery;
	}

	//================================================================
	//Overwritten Methods
	//================================================================
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

//		StartCoroutine("DeliveryPoints");
//		InvokeRepeating ("RepeatPoints", TimeInterval, TimeInterval+1);
	}
	public override void StopModule (){ 
//		StopCoroutine ("DeliveryPoints");
//		CancelInvoke ("RepeatPoints");

		//loop through the buttons to sum total cliks
		foreach(string key in this.ButtonCount.Keys){
			this.Report.ButtonCount[key] += this.ButtonCount[key];
		}
	}

	public override void OutputData (string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "Fixed Time Module";
		text += "\n=================================================\n";
		text += "Time Interval: " + this.timeInteval;
		text += "\nExecution Time: " + timeInMinutes + " minutes";
//		text += "\nEarned Points: " + this.Report.EarnedPoints;
//		text += "\nLost Points: " + this.Report.LostPoints;
//		text += "\nScore: " + this.Score;
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
	public override void ButtonClicked (string color){ 
		//search for the button...
		if (this.ButtonCount.ContainsKey (color)) {
				//...increment the counter
				this.ButtonCount [color]++;
		}
	}

	public override string ToString ()
	{
		return "Fixed Time";
	}
}
