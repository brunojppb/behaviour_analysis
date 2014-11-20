﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DROModule : BaseModule {

	private int timeInteval;
	public int TimeInterval{
		get {return timeInteval;}
		set {timeInteval = value;}
	}

	private string targetButton;
	public string TargetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}

	public DROModule(int timeInteval, string targetButton, int execTime, int order){
		this.timeInteval = timeInteval;
		this.targetButton = targetButton;
		this.ExecutionTime = execTime;
		this.Order = order;
	}

	IEnumerator DeliveryPoints(){
		while (true) {
			yield return new WaitForSeconds(TimeInterval);
			this.Score++;
		}
	}

	public override void StartModule ()
	{
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

		StartCoroutine ("DeliveryPoints");
	}

	public override void StopModule(){
		StopCoroutine ("DeliveryPoints");
	}
	
	public override void ButtonClicked(string color){

		//search for the button...
		//Debug.Log ("button clicked");
		if (this.ButtonCount.ContainsKey (color)) {
			//Debug.Log ("button Found");
			//...increment the counter
			this.ButtonCount [color]++;
			//if the button clicked was the target button
			if(color == this.TargetButton){
				//reset the timer and start again
				StopCoroutine("DeliveryPoints");
				StartCoroutine("DeliveryPoints");
			}
		}


	}


	public override void OutputData(string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "DRO Module";
		text += "\n=================================================\n";
		text += "Time Interval: " + this.timeInteval;
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


}
