// Attached to Hero

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	float runSpeed = 10f;
	float dashSpeed = 15f;
	float jumpSpeed = 18f;

	float acceleration = 10f;
	float deceleration = 10f;


	Rigidbody rigid;
	SphereCollider coll;
	Ground ground;

	public float startJumpHeight;
	public bool grounded;
	//public bool isRunning;
	public bool isDashing;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		coll = GetComponent<SphereCollider> ();
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();

		// Set the initail value of startJumpHeight to avoid the ground from rotating at start
		startJumpHeight = transform.position.y;
	}


	void Update() {
		grounded = isGrounded ();
	}

	void FixedUpdate () {
		// Freeze Hero when the ground is rotating
		if (ground.IsRotating ()) {
			rigid.isKinematic = true;	
			return;
		}

		rigid.isKinematic = false;
		HandleInput ();
	}

	bool isGrounded() {
		return (Physics.Raycast (transform.position, Vector3.down, coll.radius * 1.2f));
	}

	void HandleInput() {
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (Input.GetKey (KeyCode.S)) {
				DashRight ();
			} else {
				RunRight ();
			}
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			if (Input.GetKey (KeyCode.S)) {
				DashLeft ();
			} else {
				RunLeft ();
			}
		} else {
			StayIdle ();
		}

		if (Input.GetKey(KeyCode.A) && grounded) {
			Jump ();
		}

		// Set isDashing here
		if (Mathf.Abs (rigid.velocity.x) > runSpeed) {
			isDashing = true;
		} else if (grounded) {
			isDashing = false;
		}
	}

	void StayIdle() {
		if (grounded) {
			Vector3 vel = rigid.velocity;

			if (rigid.velocity.x > 0) {
				vel.x = Mathf.Max(rigid.velocity.x - deceleration, 0);
			} else if (rigid.velocity.x < 0) {
				vel.x = Mathf.Min(rigid.velocity.x + deceleration, 0);
			}

			rigid.velocity = vel;
		}
	}

	void RunRight() {
		Vector3 vel = rigid.velocity;

		if (rigid.velocity.x < runSpeed) { // Increase speed if too slow
			vel.x = Mathf.Min (rigid.velocity.x + acceleration, runSpeed);
		} else { // Decrease speed if too fast
			vel.x = Mathf.Max (rigid.velocity.x - deceleration, runSpeed);
		}

		rigid.velocity = vel;
	}

	void RunLeft() {
		Vector3 vel = rigid.velocity;

		if (rigid.velocity.x < runSpeed) {
			vel.x = Mathf.Max (rigid.velocity.x - acceleration, -runSpeed);
		} else {
			vel.x = Mathf.Min (rigid.velocity.x + deceleration, -runSpeed);
		}

		rigid.velocity = vel;
	}

	void DashRight() {
		Vector3 vel = rigid.velocity;
		vel.x = Mathf.Min(rigid.velocity.x + acceleration, dashSpeed);
		rigid.velocity = vel;
	}

	void DashLeft() {
		Vector3 vel = rigid.velocity;
		vel.x = Mathf.Max(rigid.velocity.x - acceleration, -dashSpeed);
		rigid.velocity = vel;
	}

	void Jump() {
		Vector3 vel = rigid.velocity;
		vel.y = jumpSpeed;
		startJumpHeight = transform.position.y;
		rigid.velocity = vel;
	}

	void OnCollisionEnter(Collision coll) {
		//if (transform.position.y - startJumpHeight > 1 && coll.transform.root.name == "Ground") {

		if (!grounded && isDashing && coll.transform.root.name == "Ground") {
			if (coll.transform.position.x - transform.position.x < 0) {
				ground.Rotate (90);
			} else {
				ground.Rotate (-90);
			}
			startJumpHeight = transform.position.y;
		}
	}
}
