using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class LivingSaw : MonoBehaviour {

	float jumpSpeed = 20f;
	float jumpHeight = 1.5f;

	Ground ground;
	Rigidbody rigid;
	SphereCollider collider;

	public float startJumpHeight;
	public bool grounded;

	InputDevice inputDevice = null;

	void Start () {
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();
		rigid = gameObject.GetComponent<Rigidbody> ();
		collider = GetComponent<SphereCollider> ();
	}

	void Update () {
		inputDevice = InputManager.ActiveDevice;

		grounded = isGrounded ();

		if (ground.IsRotating ()) {
			rigid.isKinematic = true;
		} else {
			rigid.isKinematic = false;
		}

		HandleJump ();
	}

	void HandleJump() {
		//if (grounded && Input.GetKeyDown(GameManager.jumpKey)) {
		if (grounded && inputDevice.Action1.WasPressed) {
			Vector3 vel = rigid.velocity;
			vel.y = jumpSpeed;
			rigid.velocity = vel;
		}

		// Start falling down if jump key released
		//if (!grounded && rigid.velocity.y > 0 && !Input.GetKey (GameManager.jumpKey)) {
		if (!grounded && rigid.velocity.y > 0 && !inputDevice.Action1.IsPressed) {
			if (transform.position.y - startJumpHeight > jumpHeight) {
				Vector3 vel = rigid.velocity;
				vel.y = Mathf.Max (vel.y - 60f * Time.deltaTime, 0f);
				rigid.velocity = vel;
			}
		}
	}

	bool isGrounded() {
		bool result = (Physics.Raycast (transform.position, Vector3.down, collider.radius * 1.2f));
		if (result && !grounded) {
			//anim.SplashSparkle ();
			startJumpHeight = transform.position.y;
		}

		return result;
	}
}
