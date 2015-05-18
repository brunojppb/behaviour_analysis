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

	private SessionManager observableSession;
	public SessionManager ObservableSession{
		get { return observableSession;}
		set { observableSession = value;}
	}

	private int order;
	public int Order{
		get { return order; }
		set { order = value; }
	}

	private int score;
	public int Score{
		get { return score;}
		set { 
			if(value == 0){
				//do nothing
			}
			else if(value > score){
				this.observableSession.WriteOnSessionLog("won " + (value - score), this);
				this.report.EarnedPoints += (value - score);
			}
			else if (value < score){
				this.observableSession.WriteOnSessionLog("lost " + (score - value), this);
				this.report.LostPoints += (score - value);
			}
			//update score
			score = value;
		}
	}

	private Dictionary<string, int> buttonCount;
	public Dictionary<string, int> ButtonCount{
		get { return buttonCount; }
		set { buttonCount = value; }
	}

	private ModuleReport report;
	public ModuleReport Report{
		get { return report; }
		set { report = value; }
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
	public virtual void UpdateObserverTime(float time){Debug.Log ("UpdateObserverTime... You must override this method");}
	public virtual void StartModule (){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void StopModule (){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void OutputData (string fileName){ Debug.Log ("BaseModule method caled... You must override this method"); }
	public virtual void ButtonClicked (string buttonColor){ Debug.Log ("BaseModule method caled... You must override this method"); }
}
