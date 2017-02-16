using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

	Ground ground;
	Rigidbody rigid;

	// Use this for initialization
	void Start () {
		ground = GameObject.Find ("Ground").GetComponent<Ground> ();
		rigid = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ground.IsRotating ()) {
			rigid.isKinematic = true;
		} else {
			rigid.isKinematic = false;
		}
	}
}
