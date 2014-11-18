using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Animator mainScreen;
	public Animator modulesScreen;
	public Animator buttonsScreen;

	public void toggleMainScreen(){
		mainScreen.enabled = true;
		bool isHidden = mainScreen.GetBool ("isHidden");
		mainScreen.SetBool ("isHidden", !isHidden);
	}

	public void toggleModulesScreen(){
		modulesScreen.enabled = true;
		bool isHidden = modulesScreen.GetBool ("isHidden");
		modulesScreen.SetBool ("isHidden", !isHidden);
	}

	public void toggleButtonsScreen(){
		buttonsScreen.enabled = true;
		bool isHidden = buttonsScreen.GetBool ("isHidden");
		buttonsScreen.SetBool ("isHidden", !isHidden);
	}


	public void closeProgram(){
		Application.Quit ();
	}

}
