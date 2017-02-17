// Attached to Ground

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground: MonoBehaviour {

	bool isRotatingLeft = false;
	bool isRotatingRight = false;
	float rotatingAngle;
	float rotatingSpeed = 3f;

	GameObject hero;
	Audio audio;

	void Start () {
		hero = GameObject.Find ("Hero");
		audio = GameObject.Find ("GameManager").GetComponent<Audio> ();
	}

	void FixedUpdate () {
		HandleMovement ();
	}

	void HandleMovement() {
		if (isRotatingLeft) {
			transform.RotateAround (hero.transform.position, Vector3.forward, Mathf.Min(rotatingSpeed, rotatingAngle));
			rotatingAngle = Mathf.Max (0, rotatingAngle - rotatingSpeed);
			if (rotatingAngle <= 0) {
				isRotatingLeft = false;
			}
		} else if (isRotatingRight) {
			transform.RotateAround (hero.transform.position, Vector3.back, Mathf.Min(rotatingSpeed, rotatingAngle));
			rotatingAngle = Mathf.Max (0, rotatingAngle - rotatingSpeed);
			if (rotatingAngle <= 0) {
				isRotatingRight = false;
			} 
		}
	}

	public void Rotate(float degree) {
		if (IsRotating ()) {
			return;
		}

		audio.Play ("rotate");
		if (degree > 0) {
			isRotatingLeft = true;
		} else {
			isRotatingRight = true;
		}
		rotatingAngle = Mathf.Abs(degree);
	}

	public bool IsRotating() {
		return isRotatingLeft || isRotatingRight;
	}
}
