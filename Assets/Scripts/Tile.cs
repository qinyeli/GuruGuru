// Attached to Tile

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	static Dictionary<char, Sprite> sprites;
	static GameObject ground;
	BoxCollider bc;
	SpriteRenderer sprend;

	public char type;

	void Awake() {
		if (sprites == null) {
			sprites = new Dictionary<char, Sprite> {
				{'x', Resources.Load<Sprite>("Sprites/Black")},
				{'d', Resources.Load<Sprite>("Sprites/White")},
				{'w', Resources.Load<Sprite>("Sprites/Spikes2")}
			};
		}

		bc = GetComponent<BoxCollider>();
		sprend = GetComponent<SpriteRenderer>();
	}

	void Start() {
		if (ground == null) {
			ground = GameObject.Find ("Ground");
		}

		gameObject.transform.parent = ground.transform;
	}

	public void Initialize(char c, char o, float x, float y) {
		if (!sprites.ContainsKey (c)) {
			print ("Error: undefined block: " + c);
			return;
		}

		sprend.sprite = sprites [c];
		transform.position = new Vector3 (x, y, 0);

		switch (c) {
		case 'x':
			bc.center = Vector3.zero;
			bc.size = Vector3.one;
			break;
		case 'd':
			bc.center = Vector3.zero;
			bc.size = Vector3.one;
			bc.isTrigger = enabled;
			break;
		case 'w':
			bc.center = new Vector3 (0f, -0.4f, 0f);
			bc.size = new Vector3 (0.9f, 0.1f, 0f);
			break;
		default:
			break;
		}

		type = c;

		switch (o) {
		case '1':
			gameObject.transform.Rotate (new Vector3 (0, 0, 270));
			break;
		case '2':
			gameObject.transform.Rotate (new Vector3 (0, 0, 180));
			break;
		case '3':
			gameObject.transform.Rotate (new Vector3 (0, 0, 90));
			break;
		default:
			break;
		}
	}
}
