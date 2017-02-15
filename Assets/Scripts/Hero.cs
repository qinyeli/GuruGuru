// Attached to Hero

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	float runSpeed = 10f;
	float dashSpeed = 15f;
	float jumpSpeed = 20f;

	float acceleration = 10f;
	float deceleration = 10f;

	float deathHeight = 7f;
	float jumpHeight = 1.5f;

	Rigidbody rigid;
	SphereCollider collider;
	Ground ground;
	HeroAnimation anim;
	Audio audio;

	public float startJumpHeight;
	public bool grounded;
	//public bool isRunning;
	public bool isDashing;
	public bool isDead = false;
	public bool isGoal = false;
	public bool isRight = true;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		collider = GetComponent<SphereCollider> ();
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();
		anim = GetComponent<HeroAnimation> ();
		audio = GameObject.Find ("GameManager").GetComponent<Audio> ();

		// Set the initail value of startJumpHeight to avoid the ground from rotating at start
		startJumpHeight = transform.position.y;
	}

	void Update() {
		if (isDead || isGoal) {
			return;
		}

		// Freeze Hero when the ground is rotating
		if (ground.IsRotating ()) {
			rigid.isKinematic = true;	
			return;
		}
			
		grounded = isGrounded ();
		rigid.isKinematic = false;
		HandleInput ();
	}

	bool isGrounded() {
		bool result = (Physics.Raycast (transform.position, Vector3.down, collider.radius * 1.2f));
		if (result && !grounded) {
			if (startJumpHeight - transform.position.y > deathHeight) { // Die
				Die ();
			} else {
				anim.SplashSparkle ();
				startJumpHeight = transform.position.y;
			}
		}

		return result;
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

		if (grounded && Input.GetKeyDown(KeyCode.A)) {
			Jump ();
		}

		if (!grounded && rigid.velocity.y > 0 && !Input.GetKey (KeyCode.A)) {
			if (transform.position.y - startJumpHeight > jumpHeight) {
				Vector3 vel = rigid.velocity;
				vel.y = Mathf.Max (vel.y - 60f * Time.deltaTime, 0f);
				rigid.velocity = vel;
			}
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
		isRight = true;

		Vector3 vel = rigid.velocity;

		if (rigid.velocity.x < runSpeed) { // Increase speed if too slow
			vel.x = Mathf.Min (rigid.velocity.x + acceleration, runSpeed);
		} else { // Decrease speed if too fast
			vel.x = Mathf.Max (rigid.velocity.x - deceleration, runSpeed);
		}

		rigid.velocity = vel;
	}

	void RunLeft() {
		isRight = false;

		Vector3 vel = rigid.velocity;

		if (rigid.velocity.x < runSpeed) {
			vel.x = Mathf.Max (rigid.velocity.x - acceleration, -runSpeed);
		} else {
			vel.x = Mathf.Min (rigid.velocity.x + deceleration, -runSpeed);
		}

		rigid.velocity = vel;
	}

	void DashRight() {
		isRight = true;

		Vector3 vel = rigid.velocity;
		vel.x = Mathf.Min(rigid.velocity.x + acceleration, dashSpeed);
		rigid.velocity = vel;
	}

	void DashLeft() {
		isRight = false;

		Vector3 vel = rigid.velocity;
		vel.x = Mathf.Max(rigid.velocity.x - acceleration, -dashSpeed);
		rigid.velocity = vel;
	}

	void Jump() {
		audio.Play ("jump");
		Vector3 vel = rigid.velocity;
		vel.y = jumpSpeed;
		rigid.velocity = vel;
	}

	public void Die() {
		rigid.velocity = Vector3.zero;
		rigid.isKinematic = true;
		isDead = true;
		anim.SplashBlood ();
		audio.Play ("death");
		StartCoroutine ("WaitAndReload", 1f);
	}

	IEnumerator WaitAndReload(float t) {
		yield return new WaitForSeconds(t);
		GameManager.Reload ();
	}
		
	void OnCollisionEnter(Collision coll) {
		if (coll.transform.root.name == "Ground") {
			Tile t = coll.gameObject.GetComponent<Tile> ();
			if (t.type == 'w') { // Die
				Die ();
				return;
			} else if (isGrounded()) {
				if (startJumpHeight - transform.position.y > deathHeight) { // Die
					Die ();
					return;
				}
			}

			if (isDashing) { // Rotate
				if (Mathf.Abs (coll.transform.position.y - transform.position.y) < 0.6) {
					anim.SplashRainbow ();
					startJumpHeight = transform.position.y;
					if (coll.transform.position.x - transform.position.x < 0) {
						ground.Rotate (90);
					} else {
						ground.Rotate (-90);
					}
				}
			}
		}
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.transform.root.name == "Ground") {
			Tile t = other.gameObject.GetComponent<Tile> ();
			if (t.type == 'd') {
				Goal ();
			}
		}
	}

	public void Goal() {
		rigid.velocity = Vector3.zero;
		rigid.isKinematic = true;
		isGoal = true;
		audio.Play ("goal");
		StartCoroutine ("WaitAndNextLevel", 1.5f);
	}

	IEnumerator WaitAndNextLevel(float t) {
		yield return new WaitForSeconds(t);
		GameManager.NextLevel ();
	}
}
