// Attached to Main Camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	Transform poi;
	//public Vector3 offset = new Vector3(0,1,-10);
	Vector3 offset = new Vector3(0,1,-12);

	void Start () {
		poi = GameObject.Find ("Hero").transform;
	}
	
	void FixedUpdate () {
		gameObject.transform.position = poi.position + offset;
		print (transform.position);
	}
}
