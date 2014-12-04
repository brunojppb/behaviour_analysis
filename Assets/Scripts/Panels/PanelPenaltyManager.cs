using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPenaltyManager : MonoBehaviour {

	public Animator targetButtonDropDown;
	public InputField executionTime;
	public InputField order;

	private string targetButton;
	public string TargetButton{
		get {return targetButton;}
		set {targetButton = value;}
	}
	
	public void toggleVrTargetButtonDropDown(){
		bool isHidden = targetButtonDropDown.GetBool ("isHidden");
		targetButtonDropDown.SetBool ("isHidden", !isHidden);
	}
	
	public void selectButton(string buttonColor){
		this.targetButton = buttonColor;
		this.toggleVrTargetButtonDropDown();
	}
}
