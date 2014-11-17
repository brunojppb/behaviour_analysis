using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Base module.
/// implement the interface IComparable to sort the elements
/// </summary>
public abstract class BaseModule : MonoBehaviour, IComparable<BaseModule>{

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
