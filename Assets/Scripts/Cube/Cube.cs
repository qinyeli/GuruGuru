// Attached to Blood

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	void Start() {
		GetComponent<Renderer>().sortingOrder = -1;
	}

	void OnCollisionEnter(Collision coll) {
		if (LayerMask.LayerToName(coll.gameObject.layer) != "Cube") {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (LayerMask.LayerToName(other.gameObject.layer) != "Cube") {
			Destroy (gameObject);
		}
	}
}
