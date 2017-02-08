// Attached to Tile

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	static Dictionary<char, Sprite> sprites;
	static GameObject ground;
	BoxCollider bc;
	SpriteRenderer sprend;
	GameManager gameManager;

	char type;

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

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Start() {
		if (ground == null) {
			ground = GameObject.Find ("Ground");
		}

		gameObject.transform.parent = ground.transform;
	}

	public void Initialize(char c, float x, float y) {
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
			bc.center = new Vector3 (1f, 0.05f, 0f);
			bc.size = new Vector3 (1f, 0.1f, 0f);
			bc.isTrigger = enabled;
			break;
		default:
			break;
		}

		type = c;
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Hero") {
			if (type == 'd') {
				gameManager.NextLevel ();
			} else if (type == 'w') {
				print ("Hero is hurt!");
			}
		}
	}
}
