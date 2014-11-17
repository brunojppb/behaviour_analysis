using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Base module.
/// implement the interface IComparable to sort the elements
/// </summary>
public abstract class BaseModule : MonoBehaviour, IComparable<BaseModule>{

	public BaseModule(){
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
		Debug.Log("Base module Constructor...");
	}

	public int executionTime{
		get { return executionTime; }
		set { executionTime = value; }
	}

	public int order{
		get { return order; }
		set { order = value; }
	}

	public int score{
		get { return score;}
		set { score = value;}
	}

	public Dictionary<string, int> buttonCount{
		get { return buttonCount; }
		set { buttonCount = value; }
	}

	//Icomparable method
	public int CompareTo(BaseModule other){
		if (other == null) {
			return 1;
		}
		else {
			if(this.order < other.order){
				return this.order;
			}
			else{
				return other.order;
			}
		}
	}

	public abstract void outputData (string fileName);
}
