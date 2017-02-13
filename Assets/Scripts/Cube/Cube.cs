// Attached to Blood

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	void Start() {
		GetComponent<Renderer>().sortingOrder = -1;

		// Set the size
		float size = Random.Range(0.2f, 0.3f);
		transform.localScale = new Vector3 (size, size, size);
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
