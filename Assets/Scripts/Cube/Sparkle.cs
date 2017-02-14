using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour {
	public float maxSpeed = 10f;
	public float minSize = 0.15f;
	public float maxSize = 0.2f;
	public Vector3 gravity = new Vector3(0, -10f, 0);

	Rigidbody rigid;

	void Start () {
		GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", new Color (1f, 1f, 1f, Random.Range(0f, 1f)));
		GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, Random.Range(0.3f, 1f));

		rigid = GetComponent<Rigidbody> ();

		// Set the size
		float size = Random.Range(minSize, maxSize);
		transform.localScale = new Vector3 (size, size, size);

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
