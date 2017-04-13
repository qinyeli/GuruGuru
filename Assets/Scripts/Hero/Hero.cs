// Attached to Hero

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	float runSpeed = 10f;
	float dashSpeed = 20f;
	float jumpSpeed = 20f;
	float deathSpeed = 60f;

	float acceleration = 350f;
	float deceleration = 300f;

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

		// Hero is dead becuase of falling out of the map
		if (rigid.velocity.y < -deathSpeed) {
			Die ();
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
		HandleDash ();

		if (!isDashing) {
			HandleDirection ();
		}

		HandleJump ();
	}

	void HandleDirection() {
		// Run right
		Vector3 vel = rigid.velocity;

		if (Input.GetKey (KeyCode.RightArrow)) {
			isRight = true;

			if (rigid.velocity.x < runSpeed) { // Increase speed if too slow
				vel.x = Mathf.Min (rigid.velocity.x + acceleration * Time.deltaTime, runSpeed);
			} else { // Decrease speed if too fast
				vel.x = Mathf.Max (rigid.velocity.x - deceleration * Time.deltaTime, runSpeed);
			}

			rigid.velocity = vel;

		// Run left
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			isRight = false;

			if (rigid.velocity.x < runSpeed) {
				vel.x = Mathf.Max (rigid.velocity.x - acceleration * Time.deltaTime, -runSpeed);
			} else {
				vel.x = Mathf.Min (rigid.velocity.x + deceleration * Time.deltaTime, -runSpeed);
			}

			rigid.velocity = vel;

		// Stay idle
		} else {
			if (grounded) {
				if (rigid.velocity.x > 0) {
					vel.x = Mathf.Max(rigid.velocity.x - deceleration * Time.deltaTime, 0);
				} else if (rigid.velocity.x < 0) {
					vel.x = Mathf.Min(rigid.velocity.x + deceleration * Time.deltaTime, 0);
				}

				rigid.velocity = vel;
			}
		}
	}

	void HandleDash() {
		if (Input.GetKey (GameManager.dashKey)) {
			
			// Dash right
			if (isRight) {
				isDashing = true;
				Vector3 vel = rigid.velocity;
				//vel.x = Mathf.Min(rigid.velocity.x + acceleration, dashSpeed);
				vel.x = dashSpeed;
				rigid.velocity = vel;

			// Dash left
			} else {
				isDashing = true;
				Vector3 vel = rigid.velocity;
				//vel.x = Mathf.Max(rigid.velocity.x - acceleration, -dashSpeed);
				vel.x = -dashSpeed;
				rigid.velocity = vel;
			}
		
		// Stop dashing
		} else if (Input.GetKeyUp (GameManager.dashKey)) {
			StartCoroutine ("WaitAndStopDashing", 0.1f);
		}
	}

	IEnumerator WaitAndStopDashing (float t) {
		yield return new WaitForSeconds (t);
		isDashing = false;
	}

	void HandleJump() {
		if (grounded && Input.GetKeyDown(GameManager.jumpKey)) {
			audio.Play ("jump");
			Vector3 vel = rigid.velocity;
			vel.y = jumpSpeed;
			rigid.velocity = vel;
		}

		// Start falling down if jump key released
		if (!grounded && rigid.velocity.y > 0 && !Input.GetKey (GameManager.jumpKey)) {
			if (transform.position.y - startJumpHeight > jumpHeight) {
				Vector3 vel = rigid.velocity;
				vel.y = Mathf.Max (vel.y - 60f * Time.deltaTime, 0f);
				rigid.velocity = vel;
			}
		}
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
			if (coll.gameObject.tag == "Tile") {

				Tile t = coll.gameObject.GetComponent<Tile> ();
				if (t.type == 'w') { // Die
					Die ();
					return;
				} else if (isGrounded ()) {
					if (startJumpHeight - transform.position.y > deathHeight) { // Die
						Die ();
						return;
					}
				}

				if (isDashing || Mathf.Abs (rigid.velocity.x) > runSpeed) { // Rotate
					if (Mathf.Abs (coll.transform.position.y - transform.position.y) < 0.6) {
						anim.SplashSparkle ();
						startJumpHeight = transform.position.y;
						isDashing = false;
						if (coll.transform.position.x - transform.position.x < 0) {
							ground.Rotate (90);
						} else {
							ground.Rotate (-90);
						}
					}
				}
			} else if (coll.transform.tag == "Saw") {
				Die ();
				return;
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
		anim.SplashRainbow ();
		StartCoroutine ("WaitAndNextLevel", 1.5f);
	}

	IEnumerator WaitAndNextLevel(float t) {
		yield return new WaitForSeconds(t);
		GameManager.NextLevel ();
	}
}
