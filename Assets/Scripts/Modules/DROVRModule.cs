﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DROVRModule : BaseModule {

	//===========================================
	//DRO Module Variables
	//===========================================
	private int timeInteval;
	public int TimeInterval{
		get {return timeInteval;}
		set {timeInteval = value;}
	}

	private int droPointsToDelivery;
	public int DROPointsToDelivery{
		get {return droPointsToDelivery;}
		set {droPointsToDelivery = value;}
	}

	private float lastTimeClicked = 0.0f;
	private float lastTimeUpdate = 0.0f;
	private bool moduleRunning = false;
	
	private int pointsToDelivery;
	public int PointsToDelivery{
		get {return pointsToDelivery;}
		set {pointsToDelivery = value;}
	}

	private bool pointFromDRO = false;
	public bool PointFromDRO {
		get {return pointFromDRO; }
		set {pointFromDRO = value;}
	}


	//===========================================
	//VR Module Variables
	//===========================================
	private string targetButton;
	public string TargetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}

	private int vrPointsToDelivery;
	public int VRPointsToDelivery{
		get {return vrPointsToDelivery;}
		set {vrPointsToDelivery = value;}
	}



	private int variableRatio;
	public int VariableRatio{
		get { return variableRatio; }
		set { variableRatio = value; }
	}
	
	//random number between (VariableRatio-2) and (VariableRatio+2)
	private int randomVariation;
	private int clickCount;

	public override void ButtonClicked (string buttonColor){ 
		//VR Module logic
		//search for the button...
		//Debug.Log ("button clicked");
		if (this.ButtonCount.ContainsKey (buttonColor)) {
			//Debug.Log ("button Found");
			//...increment the counter
			this.ButtonCount[buttonColor]++;

			//if the user achieve the variable ratio...
			if(this.targetButton == buttonColor){

				//DRO logic
				//if the button clicked was the target button
				this.lastTimeClicked = this.lastTimeUpdate;

				//VR Module logic
				if(clickCount == randomVariation){
					//... player earns vr delivery points
					this.PointFromDRO = false;
					this.Score += this.vrPointsToDelivery;
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

	public override void StartModule (){

		this.Score = 0;

		//DRO Module Configuration
		this.moduleRunning = true;
		
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

		//VR Module Configuration
		randomVariation = this.generateRandomVR();
		clickCount = 0;
	}

	public override void StopModule(){
		this.moduleRunning = false;
		//loop through the buttons to sum total cliks
		foreach(string key in this.ButtonCount.Keys){
			this.Report.ButtonCount[key] += this.ButtonCount[key];
		}
	}

	//========================================================
	//Delivery DRODeliveryPoints based on the time interval
	//========================================================
	private void calculatePoints(){
		Debug.Log("Last Update: " + this.lastTimeUpdate + " | Time click: " + lastTimeClicked + " | Time interval: " + this.timeInteval);
		if (this.lastTimeUpdate >= (this.lastTimeClicked + this.timeInteval)) {
			Debug.Log("True ");
			this.lastTimeClicked = this.lastTimeUpdate;
			this.PointFromDRO = true;
			this.Score += this.droPointsToDelivery;
		}
	}
	
	public override void UpdateObserverTime (float time)
	{
		if(moduleRunning){
			lastTimeUpdate = time;
			this.calculatePoints ();
		}
	}

	//==========================================================
	//Auxiliar function to generate the number inside the range
	//==========================================================
	private int generateRandomVR(){
		return Random.Range (this.variableRatio - 2, (this.variableRatio + 2) + 1);
	}

	public override void OutputData (string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "DROVR Module";
		text += "\n=================================================\n";
		text += "VR: " + this.VariableRatio;
		text += "\nTime Inteval: " + this.timeInteval;
		text += "\nExecution Time: " + timeInMinutes + " minutes";
//		text += "\nDRO Target Button: " + this.DroTargetButton;
		text += "\nTarget Button: " + this.targetButton;
		text += "\nEarned Points: " + this.Report.EarnedPoints;
		text += "\nLost Points: " + this.Report.LostPoints;
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
		return "DRO+VR";
	}

}
