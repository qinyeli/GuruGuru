// Attached to ButtonGenerator

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour {
	float screenW;
	float screenH;
	float spaceW = 10;
	float spaceH = 10;
	float w = 120;
	float h = 40;
	GUIStyle style;

	void Start() {
		style = new GUIStyle ("button");
		style.fontSize = 24;

		screenW = Screen.width;
		screenH = Screen.height;
	}

	void OnGUI() {
		int i = 0;
		while (i < GameManager.totalLevel) {
			float y = screenH * 0.5f + (h + spaceH) * i / 3;
			if (GUI.Button(new Rect (screenW * 0.5f - spaceW - w * 1.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}
			i++;

			if (GUI.Button(new Rect (screenW * 0.5f - w * 0.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}
			i++;

			if (GUI.Button(new Rect (screenW * 0.5f + spaceW + w * 0.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}
			i++;
		}
	}
}
