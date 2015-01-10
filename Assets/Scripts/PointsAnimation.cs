using UnityEngine;
using System.Collections;

public class PointsAnimation : MonoBehaviour {

	public void StartPointsAnimation(string animationName){
		(GetComponent<Animator> () as Animator).SetTrigger (animationName);
	}

	IEnumerator DestroyAnimation(){
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
}
