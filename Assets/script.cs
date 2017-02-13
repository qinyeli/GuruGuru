using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {

	public GameObject bloodPrefab;

	void SplashBlood() {
		for (int i = 0; i < 30; i++) {
			GameObject go = Instantiate (bloodPrefab);
			go.transform.position = gameObject.transform.position;

			float size = Random.Range (0.1f, 0.2f);
			go.transform.localScale = new Vector3 (size, size, size);

			Vector3 velocity = new Vector3 (Random.Range(3f, 3f), Random.Range(3f, 3f), 0);
			go.GetComponent<Rigidbody> ().velocity = velocity;
		}
	}
}
