using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DROModule : BaseModule {

	private float timeInteval;
	public float TimeInterval{
		get {return timeInteval;}
		set {timeInteval = value;}
	}

	private float lastTimeClicked = 0.0f;
	private float lastTimeUpdate = 0.0f;
	private bool moduleRunning = false;

	private int pointsToDelivery;
	public int PointsToDelivery{
		get {return pointsToDelivery;}
		set {pointsToDelivery = value;}
	}


	private string targetButton;
	public string TargetButton{
		get { return targetButton; }
		set { targetButton = value; }
	}

	IEnumerator DeliveryPoints(){
		while (true) {
			yield return new WaitForSeconds(TimeInterval);
			this.Score += this.pointsToDelivery;
		}
	}

	public override void StartModule ()
	{
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
		this.moduleRunning = true;
//		StartCoroutine ("DeliveryPoints");
	}

	public override void StopModule(){
		this.moduleRunning = false;
//		StopCoroutine ("DeliveryPoints");
		//loop through the buttons to sum total cliks
		foreach(string key in this.ButtonCount.Keys){
			this.Report.ButtonCount[key] += this.ButtonCount[key];
		}
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
				this.lastTimeClicked = this.lastTimeUpdate;
				//reset the timer and start again
//				StopCoroutine("DeliveryPoints");
//				StartCoroutine("DeliveryPoints");
			}
		}


	}

	private void calculatePoints(){
		Debug.Log ("Last time: " + this.lastTimeClicked + " - Click: " + this.lastTimeClicked);
		if (this.lastTimeUpdate >= (this.lastTimeClicked + this.timeInteval)) {
			this.lastTimeClicked = this.lastTimeUpdate;
			this.Score += this.pointsToDelivery;
		}
	}

	public override void UpdateObserverTime (float time)
	{
		if(moduleRunning){
			lastTimeUpdate = time;
			this.calculatePoints ();
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
		return "DRO";
	}


}
