using UnityEngine;
using System.Collections;

public class PanelDROManager : MonoBehaviour {

	public Animator droTargetButton;

	public void toggledroTargetButtonMenu(){
		bool isHidden = droTargetButton.GetBool ("isHidden");
		droTargetButton.SetBool ("isHidden", !isHidden);
	}
}
