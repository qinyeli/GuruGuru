using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingSaw : MonoBehaviour {

	float jumpSpeed = 20f;
	float jumpHeight = 1.5f;

	Ground ground;
	Rigidbody rigid;
	SphereCollider collider;

	public float startJumpHeight;
	public bool grounded;

	// Use this for initialization
	void Start () {
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();
		rigid = gameObject.GetComponent<Rigidbody> ();
		collider = GetComponent<SphereCollider> ();
	}

	// Update is called once per frame
	void Update () {
		grounded = isGrounded ();

		if (ground.IsRotating ()) {
			rigid.isKinematic = true;
		} else {
			rigid.isKinematic = false;
		}

		if (grounded && Input.GetKeyDown(GameManager.jumpKey)) {
			Jump ();
		}

		if (!grounded && rigid.velocity.y > 0 && !Input.GetKey (GameManager.jumpKey)) {
			if (transform.position.y - startJumpHeight > jumpHeight) {
				Vector3 vel = rigid.velocity;
				vel.y = Mathf.Max (vel.y - 60f * Time.deltaTime, 0f);
				rigid.velocity = vel;
			}
		}
	}

	void Jump() {
		Vector3 vel = rigid.velocity;
		vel.y = jumpSpeed;
		rigid.velocity = vel;
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
