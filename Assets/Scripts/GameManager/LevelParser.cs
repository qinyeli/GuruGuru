// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LevelParser : MonoBehaviour {

	static GameObject tilePrefab;
	static GameObject heroPrefab;
	//static GameObject backgroundPrefab;
	static GameObject sawPrefab;
	static GameObject livingSawPrefab;
	//static Sprite graySprite;

	void Awake() {
		tilePrefab = Resources.Load<GameObject> ("Prefabs/Tile");
		heroPrefab = Resources.Load<GameObject> ("Prefabs/Hero");
		//backgroundPrefab = Resources.Load<GameObject> ("Prefabs/Background");
		sawPrefab = Resources.Load<GameObject> ("Prefabs/Saw");
		livingSawPrefab = Resources.Load<GameObject> ("Prefabs/LivingSaw");
	}

	static public void Load(int level) {
		// Load the map file and analyze basic info
		TextAsset map = Resources.Load<TextAsset> ("Levels/Level_" + level);
		string[] lines = map.text.Split('\n');
		int h = lines.Length;
		int w = lines[0].Split(',').Length;

		// Parse the map file and generate tiles, the background and the hero
		Tile[,] tiles = new Tile[h,w];
		for (int i = 0; i < h; i++) {
			string[] temp = lines [i].Split (',');

			for (int j = 0; j < w; j++) {

				char type = 'o';
				if (temp [j].Length > 0) {
					type = temp [j] [0];
				}
					
				char orientation = '0';

				if (temp[j].Length > 1) {
					orientation = temp [j] [1];
				}


				/*
				 * s: source
				 * d: destination
				 * x: black box
				 * o: no box
				 * 
				 * 
				 * 0: rotate 0 degrees
				 * 1: rotate 90 degrees
				 * 2: rotate 180 degrees
				 * 3: rotate 270 degress
				 */

				GameObject ground = GameObject.Find ("Ground");

				if (type == 'h') {
					GameObject hero = Instantiate (heroPrefab);
					hero.name = "Hero";
					hero.transform.position = new Vector3 (j, -i, 0);

				} else if (type == 's') {
					GameObject saw = Instantiate (sawPrefab);
					saw.transform.parent = ground.transform;
					saw.transform.position = new Vector3 (j, -i, 0);

				} else if (type == 'S') {
					GameObject livingSaw = Instantiate (livingSawPrefab);
					livingSaw.transform.parent = ground.transform;
					livingSaw.transform.position = new Vector3 (j, -i, 0);

				} else if (type != 'o') {
					GameObject go = Instantiate (tilePrefab);
					Tile t = go.GetComponent<Tile> ();
					t.Initialize (type, orientation, j, - i);
					tiles [i, j] = t;
				}
			}
		}

//		GameObject gobg = Instantiate(backgroundPrefab);
//		Background b = gobg.GetComponent<Background> ();
//		b.Initialize(h, w);
	}
}
