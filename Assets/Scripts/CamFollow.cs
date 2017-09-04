// Attached to Main Camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	Transform poi;
	public Vector3 offset = new Vector3(0,1,-12);
	
	void FixedUpdate () {
		poi = GameObject.Find ("Hero").transform;
		gameObject.transform.position = poi.position + offset;
	}
}
