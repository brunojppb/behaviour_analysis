using UnityEngine;
using System.Collections;

public class PanelDROVRManager : MonoBehaviour {

	public Animator vrTargetButton;
	public Animator droTargetButton;

	public void toggleVrTargetButtonMenu(){
		bool isHidden = vrTargetButton.GetBool ("isHidden");
		vrTargetButton.SetBool ("isHidden", !isHidden);
	}

	public void toggledroTargetButtonMenu(){
		bool isHidden = droTargetButton.GetBool ("isHidden");
		droTargetButton.SetBool ("isHidden", !isHidden);
	}
}
