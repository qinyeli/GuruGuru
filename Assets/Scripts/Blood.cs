// Attached to Blood

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

	Vector3 gravity = new Vector3(0, -20f, 0);
	Rigidbody rigid;

	void Start() {
		GetComponent<Renderer>().sortingOrder = -1;
		rigid = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		rigid.velocity = rigid.velocity + gravity * Time.deltaTime;
	}

	void OnCollisionEnter(Collision coll) {
		if (LayerMask.LayerToName(coll.gameObject.layer) != "Blood") {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (LayerMask.LayerToName(other.gameObject.layer) != "Blood") {
			Destroy (gameObject);
		}
	}
}
