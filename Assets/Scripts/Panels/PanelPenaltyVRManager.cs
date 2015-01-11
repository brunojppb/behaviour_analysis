using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelPenaltyVRManager : MonoBehaviour {

	public Animator targetButtonDropDown;

	[Header("User Input")]
	public InputField variableRatio;
	public InputField executionTime;
	public InputField order;
	public InputField penaltyPoints;
	public InputField vrPoints;

	[Header("Target Button Sprites")]
	public Image buttonImage;
	public Sprite[] buttonSprites;
	
	private string buttonSelected;
	public string ButtonSelected{
		get {return buttonSelected;}
		set {buttonSelected = value;}
	}
	
	public void toggleVrTargetButtonDropDown(){
		bool isHidden = targetButtonDropDown.GetBool ("isHidden");
		targetButtonDropDown.SetBool ("isHidden", !isHidden);
	}
	
	public void selectButton(string buttonColor){
		this.ButtonSelected = buttonColor;
		this.toggleVrTargetButtonDropDown();
		//change the button color to show a feedback to user
		foreach (Sprite s in this.buttonSprites) {
			if(s.ToString().Contains(buttonColor))
				this.buttonImage.overrideSprite = s;
		}
	}

	void Start(){
		this.ButtonSelected = "black";
	}
}
