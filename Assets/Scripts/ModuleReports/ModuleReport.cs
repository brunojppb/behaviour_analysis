using UnityEngine;
using System.Collections.Generic;

public class ModuleReport {

	private int earnedPoints;
	public int EarnedPoints{
		get { return earnedPoints;}
		set { earnedPoints = value; }
	}

	private int lostPoints;
	public int LostPoints{
		get { return lostPoints;}
		set { lostPoints = value; }
	}

	private int time;
	public int Time{
		get { return time;}
		set { time = value; }
	}
	
	private Dictionary<string, int> buttonCount;
	public Dictionary<string, int> ButtonCount{
		get { return buttonCount; }
		set { buttonCount = value; }
	}

	public ModuleReport(){
		this.earnedPoints = 0;
		this.lostPoints = 0;
		this.time = 0;

		//initialize the button counter
		this.buttonCount = new Dictionary<string, int> ();
		this.buttonCount.Add ("blue", 0);
		this.buttonCount.Add ("green", 0);
		this.buttonCount.Add ("yellow", 0);
		this.buttonCount.Add ("pink", 0);
		this.buttonCount.Add ("black", 0);
		this.buttonCount.Add ("red", 0);
		this.buttonCount.Add ("purple", 0);
		this.buttonCount.Add ("orange", 0);
		this.buttonCount.Add ("white", 0);
	}

}
