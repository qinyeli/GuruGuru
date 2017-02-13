using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour {
	public float maxSpeed = 5f;
	public Vector3 gravity = new Vector3(0, -20f, 0);

	Rigidbody rigid;

	void Start () {
		GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", new Color (1f, 1f, 1f, Random.Range(0f, 1f)));
		GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, Random.Range(0f, 1f));

		rigid = GetComponent<Rigidbody> ();

		// Set the velocity
		Vector3 velocity = new Vector3 (
			Random.Range(-maxSpeed, maxSpeed),
			Random.Range(-maxSpeed, maxSpeed),
			0);
		rigid.velocity = velocity + gameObject.GetComponent<Rigidbody> ().velocity;
	}

	void FixedUpdate() {
		rigid.velocity = rigid.velocity + gravity * Time.deltaTime;
	}
}
