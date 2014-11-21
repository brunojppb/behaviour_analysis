using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDROVRManager : MonoBehaviour {

	public Animator vrTargetButton;
	public Animator droTargetButton;

	public InputField timeInterval;
	public InputField variableRatio;
	public InputField executionTime;
	public InputField order;

	private string vrTargetButtonSelected;
	public string VrTargetButtonSelected{
		get {return vrTargetButtonSelected;}
		set {vrTargetButtonSelected = value;}
	}

	private string droTargetButtonSelected;
	public string DroTargetButtonSelected{
		get {return droTargetButtonSelected;}
		set {droTargetButtonSelected = value;}
	}


	public void selectVrButton(string buttonColor){
		this.vrTargetButtonSelected = buttonColor;
		this.toggleVrTargetButtonMenu();
	}

	public void selectDroButton(string buttonColor){
		this.droTargetButtonSelected = buttonColor;
		this.toggledroTargetButtonMenu();
	}


	public void toggleVrTargetButtonMenu(){
		bool isHidden = vrTargetButton.GetBool ("isHidden");
		vrTargetButton.SetBool ("isHidden", !isHidden);
	}

	public void toggledroTargetButtonMenu(){
		bool isHidden = droTargetButton.GetBool ("isHidden");
		droTargetButton.SetBool ("isHidden", !isHidden);
	}
}
