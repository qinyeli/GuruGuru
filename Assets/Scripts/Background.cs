// Attached to Background

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	static 	Sprite sprite;
	static GameObject ground;

	public void Initialize(float h, float w) {
		if (sprite == null) {
			sprite = Resources.Load<Sprite> ("Sprites/Gray");
		}
		if (ground == null) {
			ground = GameObject.Find ("Ground");
		}

		gameObject.transform.parent = ground.transform;
		transform.localScale = new Vector3 (w, h, 1);
		transform.position = new Vector3 (((float)w - 1) / 2, - ((float)h - 1) / 2, 0);
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
	}
}
