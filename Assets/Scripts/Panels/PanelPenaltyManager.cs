using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPenaltyManager : MonoBehaviour {

	public Animator targetButtonDropDown;

	[Header("User Input")]
	public InputField executionTime;
	public InputField order;
	public InputField penaltyPoints;

	[Header("Target Button Sprites")]
	public Image buttonImage;
	public Sprite[] buttonSprites;

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
		//change the button color to show a feedback to user
		foreach (Sprite s in this.buttonSprites) {
			if(s.ToString().Contains(buttonColor))
				this.buttonImage.overrideSprite = s;
		}
	}

	void Start(){
		this.targetButton = "black";
	}
}
