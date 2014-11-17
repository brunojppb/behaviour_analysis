﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelVariableRatioManager : MonoBehaviour {

	public Animator targetButtonDropDown;
	public InputField sessionTime;
	public InputField order;

	private string buttonSelected{
		get {return buttonSelected;}
		set {buttonSelected = value;}
	}

	public void toggleVrTargetButtonDropDown(){
		bool isHidden = targetButtonDropDown.GetBool ("isHidden");
		targetButtonDropDown.SetBool ("isHidden", !isHidden);
	}

	public void selectButton(string buttonColor){
		this.buttonSelected = buttonColor;
		this.toggleVrTargetButtonDropDown();
	}
}
