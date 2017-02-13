﻿// Attached to Hero

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroAnimation : MonoBehaviour {

	bool isBleeding = false;
	GameObject bloodPrefab;
	System.Random random = new System.Random();

	// Use this for initialization
	void Start () {
		bloodPrefab = Resources.Load<GameObject> ("Prefabs/Blood");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SplashBlood();
		}

		if (isBleeding) {
			for (int i = 0; i < 10; i++) {
				GameObject go = Instantiate (bloodPrefab);

				go.transform.position = gameObject.transform.position;

				float size = (float)random.NextDouble () * 0.1f + 0.2f;
				go.transform.localScale = new Vector3 (size, size, size);

				Vector3 velocity = new Vector3 (
					                   (float)random.NextDouble () * 30f - 15f,
					                   (float)random.NextDouble () * 30f - 15f,
					                   0);
				go.GetComponent<Rigidbody> ().velocity = velocity;
			}
		}
	}

	public void SplashBlood() {
		isBleeding = true;
		StartCoroutine ("WaitAndStopBleeding", 0.05f);
	}

	IEnumerator WaitAndStopBleeding(float t) {
		yield return new WaitForSeconds (t);
		isBleeding = false;
	}
}
