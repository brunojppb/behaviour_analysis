using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDROVRManager : MonoBehaviour {

	public Animator targetButtonAnimator;

	[Header("User Input")]
	public InputField timeInterval;
	public InputField variableRatio;
	public InputField executionTime;
	public InputField order;
	public InputField droPoints;
	public InputField VrPoints;

	[Header("Target Button Sprites")]
	public Image buttonImage;
	public Sprite[] buttonSprites;

	private string targetButton;
	public string TargetButton{
		get {return targetButton;}
		set {targetButton = value;}
	}


	public void toggleTargetButtonMenu(){
		bool isHidden = targetButtonAnimator.GetBool ("isHidden");
		targetButtonAnimator.SetBool ("isHidden", !isHidden);
	}

	public void selectButton(string buttonColor){
		this.targetButton = buttonColor;
		this.toggleTargetButtonMenu();
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
