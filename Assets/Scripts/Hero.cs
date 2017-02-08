// Attached to Hero

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	float runSpeed = 10f;
	float jumpSpeed = 10f;
	Rigidbody rigid;
	Ground ground;

	public float startJumpHeight;
	public bool grounded;
	SphereCollider coll;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		coll = GetComponent<SphereCollider> ();
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();

		// Set the initail value of startJumpHeight to avoid the ground from rotating at start
		startJumpHeight = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		grounded = isGrounded ();
		HandleInput ();
	}

	bool isGrounded() {
		return (Physics.Raycast (transform.position, Vector3.down, coll.radius * 1.2f));
	}

	void HandleInput() {
		if (Input.GetKey (KeyCode.RightArrow)) {
			RunRight ();
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			RunLeft ();
		}

		if (Input.GetKey(KeyCode.A) && grounded) {
			Jump ();
		}
	}

	void RunRight() {
		Vector3 vel = rigid.velocity;
		vel.x = runSpeed;
		rigid.velocity = vel;
	}

	void RunLeft() {
		Vector3 vel = rigid.velocity;
		vel.x = -runSpeed;
		rigid.velocity = vel;
	}

	void Jump() {
		Vector3 vel = rigid.velocity;
		vel.y = jumpSpeed;
		startJumpHeight = transform.position.y;
		rigid.velocity = vel;
	}

	void OnCollisionEnter(Collision coll) {
		if (transform.position.y - startJumpHeight > 1 && coll.transform.root.name == "Ground") {

			if (coll.transform.position.x - transform.position.x < 0) {
				ground.Rotate (90);
			} else {
				ground.Rotate (-90);
			}
			startJumpHeight = transform.position.y;
		}
	}
}
