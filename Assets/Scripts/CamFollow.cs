// Attached to Main Camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	GameObject hero;
	public Vector3 offset = new Vector3 (0, 1, -12);

	void Start() {
		hero = GameObject.Find ("Hero");
	}

	void FixedUpdate () {
		if (hero != null) {
			gameObject.transform.position = hero.transform.position + offset;
		}
	}
}
