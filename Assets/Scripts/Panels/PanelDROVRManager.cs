using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDROVRManager : MonoBehaviour {

	public Animator targetButtonAnimator;

	public InputField timeInterval;
	public InputField variableRatio;
	public InputField executionTime;
	public InputField order;

	private string targetButton;
	public string TargetButton{
		get {return targetButton;}
		set {targetButton = value;}
	}


	public void selectButton(string buttonColor){
		this.targetButton = buttonColor;
		this.toggleTargetButtonMenu();
	}


	public void toggleTargetButtonMenu(){
		bool isHidden = targetButtonAnimator.GetBool ("isHidden");
		targetButtonAnimator.SetBool ("isHidden", !isHidden);
	}

}
