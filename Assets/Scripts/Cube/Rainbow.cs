using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {
	public float maxSpeed = 10f;
	public float minSize = 0.2f;
	public float maxSize = 0.3f;

	public Vector3 gravity = new Vector3(0, -20f, 0);

	Color[] colors = new Color[] {
		new Color(1f, 0.125f, 0f), // red
		new Color(1f, 0.625f, 0f), // orange
		new Color(1f, 0.875f, 0f), // yellow
		new Color(0f, 1f, 0f), // green
		new Color(0f, 0.85f, 1f), // blue
		new Color(0.875f, 0f, 1f), // purple
	};
	Rigidbody rigid;

	void Start () {
		GetComponent<MeshRenderer>().material.SetColor ("_EmissionColor",
			colors[Random.Range(0, colors.Length)]);

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
