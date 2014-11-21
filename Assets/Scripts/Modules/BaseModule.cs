using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Base module.
/// implement the interface IComparable to sort the elements
/// </summary>
public class BaseModule : MonoBehaviour, IComparable<BaseModule>{

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

	//Icomparable method
	public int CompareTo(BaseModule other){
		if (other == null)
			return 1;

		if (this.Order < other.Order)
			return -1;
		else
			return 1;

	

	}
	public virtual void StartModule (){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void StopModule (){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void OutputData (string fileName){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void ButtonClicked (string buttonColor){ Debug.Log ("BaseModule method caled... You must override this method"); }
}
