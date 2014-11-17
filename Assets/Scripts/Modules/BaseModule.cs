using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Base module.
/// implement the interface IComparable to sort the elements
/// </summary>
public abstract class BaseModule : IComparable<BaseModule>{

	private int executionTime;
	public int ExecutionTime{
		get { return executionTime; }
		set { executionTime = value; }
	}

	private int order;
	public int Order{
		get { return order; }
		set { order = value; }
	}

	private int score;
	public int Score{
		get { return score;}
		set { score = value;}
	}

	private Dictionary<string, int> buttonCount;
	public Dictionary<string, int> ButtonCount{
		get { return buttonCount; }
		set { buttonCount = value; }
	}

	public BaseModule(){
		Debug.Log("Base constructor called...");
		this.ButtonCount = new Dictionary<string, int> ();
		this.ButtonCount.Add ("BLUE", 0);
		this.ButtonCount.Add ("GREEN", 0);
		this.ButtonCount.Add ("YELLOW", 0);
		this.ButtonCount.Add ("PINK", 0);
		this.ButtonCount.Add ("BLACK", 0);
		this.ButtonCount.Add ("RED", 0);
		this.ButtonCount.Add ("PURPLE", 0);
		this.ButtonCount.Add ("ORANGE", 0);
		this.ButtonCount.Add ("WHITE", 0);
	}

	//Icomparable method
	public int CompareTo(BaseModule other){
		if (other == null) {
			return 1;
		}
		else {
			return (this.Order < other.Order) ? this.Order : other.order; 
		}
	}

	public abstract void outputData (string fileName);
}
