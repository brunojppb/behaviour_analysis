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

		this.Score = 0;
		this.order = 0;
		this.ExecutionTime = 0;
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

	public abstract void OutPutData (string fileName);
}
