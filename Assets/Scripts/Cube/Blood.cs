using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {
	public float maxSpeed = 15f;
	public float minSize = 0.15f;
	public float maxSize = 0.35f;
	public Vector3 gravity = new Vector3(0, -20f, 0);

	Rigidbody rigid;

	void Start () {
		GetComponent<MeshRenderer>().material.SetColor ("_EmissionColor", new Color (0.5f, 0f, 0f));

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
