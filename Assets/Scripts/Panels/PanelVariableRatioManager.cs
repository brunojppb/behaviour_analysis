using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelVariableRatioManager : MonoBehaviour {

	public Animator targetButtonDropDown;

	public InputField variableRatio;
	public InputField executionTime;
	public InputField order;

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
	}
}
