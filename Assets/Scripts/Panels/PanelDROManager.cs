﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDROManager : MonoBehaviour {

	[Header("User Input")]
	public Animator droTargetButton;
	public InputField timeInteval;
	public InputField executionTime;
	public InputField order;
	public InputField droPoints;

	[Header("Target Button Sprites")]
	public Image buttonImage;
	public Sprite[] buttonSprites;


	private string buttonSelected;
	public string ButtonSelected{
		get {return buttonSelected;}
		set {buttonSelected = value;}
	}

	public void toggleDroTargetButtonDropDown(){
		bool isHidden = droTargetButton.GetBool ("isHidden");
		droTargetButton.SetBool ("isHidden", !isHidden);
	}

	public void selectButton(string buttonColor){
		this.ButtonSelected = buttonColor;
		this.toggleDroTargetButtonDropDown();
		//change the button color to show a feedback to user
		foreach (Sprite s in this.buttonSprites) {
			if(s.ToString().Contains(buttonColor))
				this.buttonImage.overrideSprite = s;
		}
	}

	void Start(){
		this.buttonSelected = "black";
	}
}
