using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col){

		if (col.transform.tag == "DestructibleSection") {
			//Debug.Log (col.name);
			Destroy (col.gameObject);
		}
	}
}
