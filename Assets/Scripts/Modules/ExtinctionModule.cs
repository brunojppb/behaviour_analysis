﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExtinctionModule : BaseModule {

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
	}

	public override void StopModule (){ 
	
	}

	public override void OutputData (string filename){
		//Output the computed data when the module ends;
		double timeInMinutes = this.ExecutionTime/60.0;
		string text = "";
		text += "\n=================================================\n";
		text += "Extinction Module";
		text += "\n=================================================\n";
		text += "\nExecution Time: " + timeInMinutes + " minutes";
		text += "\nScore: " + this.Score;
		// The using statement automatically closes the stream and calls  
		// IDisposable.Dispose on the stream object. 
		using (StreamWriter file = new StreamWriter (filename, true)) {
			file.WriteLine (text);
			string tableTitle = "Button\t\tResponse Count\t\tResponse Rate(responses per minute)";
			file.WriteLine (tableTitle);
			foreach(string key in this.ButtonCount.Keys){
				file.WriteLine(key + "\t\t" + this.ButtonCount[key] + "\t\t\t" + this.ButtonCount[key] / timeInMinutes);
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
		return "Extinction";
	}

}