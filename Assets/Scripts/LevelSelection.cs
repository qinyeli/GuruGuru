// Attached to Canvas in SceneLevelSelection

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {
	
//	float screenW;
//	float screenH;
//	float spaceW = 10;
//	float spaceH = 10;
	float w = 160;
	float h = 40;

	string[] selections = new string[] {
		"Start",
		"Tutorial"
	};
	List<GameObject> selectionTexts = new List<GameObject> ();
	List<GameObject> selectionBoxes = new List<GameObject> ();
	int prevIdx = 0, currIdx = 0;

	InputDevice inputDevice = null;

	void Start() {
		GameObject textPrefab = Resources.Load<GameObject> ("Prefabs/SelectionText");
		GameObject boxPrefab = Resources.Load<GameObject> ("Prefabs/SelectionBox");

		// Generate texts and boxes
		for (int i = 0; i < selections.Length; i++) {
			
			/*---------------------------- Set the text ----------------------------*/

			GameObject go = Instantiate (textPrefab);
//			GameObject go = new GameObject (selectionTexts[i]);

			go.transform.SetParent (this.transform);
			go.transform.localScale = new Vector3 (1, 1, 1);
			selectionTexts.Add (go);

			// Set the text
			Text text = go.GetComponent<Text> ();
			text.text = selections[i];

			// Set the position of the text
			RectTransform tran = go.GetComponent<RectTransform>();
			tran.localPosition = new Vector3 (0, -60 * i, 0);
			tran.sizeDelta = new Vector2(w, h);

			// Set the size, font and alignment of the text
//			text.fontSize = 36;
//			Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
//			text.font = ArialFont;
//			text.alignment = TextAnchor.MiddleCenter;

			/*---------------------------- Set the box ----------------------------*/
			GameObject gob = Instantiate (boxPrefab);

			gob.transform.SetParent (this.transform);
			gob.transform.localScale = new Vector3 (1.5f, 1, 1);
			selectionBoxes.Add (gob);

			// Set the position of the text
			RectTransform tranb = gob.GetComponent<RectTransform>();
			tranb.localPosition = new Vector3 (0, -60 * i + 25, 0);

			if (currIdx != i) {
				gob.SetActive (false);
			}
		}
	}

	void Update () {
		inputDevice = InputManager.ActiveDevice;

		if (inputDevice.Action1.IsPressed || Input.GetKey (KeyCode.Return)) {
			if (selections [currIdx] == "Start") {
				GameManager.LoadLevel (1);
			}
			if (selections [currIdx] == "Tutorial") {
				GameManager.LoadLevel (0);
			}
		}

		if (inputDevice.Direction.Down) {
			prevIdx = currIdx;
			currIdx++;
			currIdx = Mathf.Min (currIdx, selections.Length - 1);
		} else if (inputDevice.Direction.Up) {
			prevIdx = currIdx;
			currIdx--;
			currIdx = Mathf.Max (currIdx, 0);
		}

		if (prevIdx != currIdx) {
			selectionBoxes [prevIdx].SetActive (false);
			selectionBoxes [currIdx].SetActive (true);
		}
	}
}