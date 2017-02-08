// Attached to Main Camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	Transform poi;
	//public Vector3 offset = new Vector3(0,1,-10);
	public Vector3 offset = new Vector3(0,1,-10);

	void Start () {
		poi = GameObject.Find ("Hero").transform;
	}
	
	void FixedUpdate () {
		transform.position = poi.position + offset;
	}
}
