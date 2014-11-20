using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDROManager : MonoBehaviour {

	public Animator droTargetButton;
	public InputField timeInteval;
	public InputField executionTime;
	public InputField order;
	
	private string buttonSelected;
	public string ButtonSelected{
		get {return buttonSelected;}
		set {buttonSelected = value;}
	}


	public void selectButton(string buttonColor){
		this.ButtonSelected = buttonColor;
		this.toggleDroTargetButtonDropDown();
	}


	public void toggleDroTargetButtonDropDown(){
		bool isHidden = droTargetButton.GetBool ("isHidden");
		droTargetButton.SetBool ("isHidden", !isHidden);
	}
}
