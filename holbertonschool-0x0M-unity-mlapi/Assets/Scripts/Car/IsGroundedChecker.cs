using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedChecker : MonoBehaviour {

	void OnTriggerStay(Collider col){
		if (col.tag != "Player") {
			//Debug.Log ("Is Grounded");
			GetComponentInParent<Drive> ().isGrounded = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag != "Player") {
			//Debug.Log ("Is Flying");
			GetComponentInParent<Drive> ().isGrounded = false;
		}
	}

}
