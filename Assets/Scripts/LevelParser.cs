// Initialized in GameManager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LevelParser : MonoBehaviour {

	static GameObject tilePrefab;
	static GameObject heroPrefab;
	static GameObject backgroundPrefab;
	//static Sprite graySprite;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		tilePrefab = Resources.Load<GameObject> ("Prefabs/Tile");
		heroPrefab = Resources.Load<GameObject> ("Prefabs/Hero");
		backgroundPrefab = Resources.Load<GameObject> ("Prefabs/Background");
	}

	static public void Load(int level) {
		// Load the map file and analyze basic info
		TextAsset map = Resources.Load<TextAsset> ("Levels/Level_" + level);
		string[] lines = map.text.Split('\n');
		int h = lines.Length;
		int w = lines[0].Split(',').Length;

		// Parse the map file and generate tiles, the background and the hero
		Tile[,] tiles = new Tile[w,h];
		for (int i = 0; i < w; i++) {
			string[] temp = lines [i].Split (',');

			for (int j = 0; j < h; j++) {
				//char c = char.Parse (temp [j]);
				char type = temp [j] [0]; // c defines the tile type

				/*
				 * s: source
				 * d: destination
				 * x: black box
				 * o: no box
				 */

				if (type == 's') {
					GameObject hero = Instantiate (heroPrefab);
					hero.name = "Hero";
					hero.transform.position = new Vector3 (j, - i, 0);
				} else if (type != 'o') {
					GameObject go = Instantiate (tilePrefab);
					Tile t = go.GetComponent<Tile> ();
					t.Initialize (type, j, - i);
					tiles [i, j] = t;
				}
			}
		}

		GameObject gobg = Instantiate(backgroundPrefab);
		Background b = gobg.GetComponent<Background> ();
		b.Initialize(h, w);
	}
}
