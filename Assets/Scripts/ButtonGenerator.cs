﻿// Attached to Canvas in SceneLevelSelection

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonGenerator : MonoBehaviour {
	float screenW;
	float screenH;
	float spaceW = 10;
	float spaceH = 10;
	float w = 160;
	float h = 40;
	GUIStyle style;

	void OnGUI() {
		style = new GUIStyle ("button");
		style.fontSize = 24;
		screenW = Screen.width;
		screenH = Screen.height;


		int i = 1;
		while (true) {
			float y = screenH * 0.5f + (h + spaceH) * i / 3;
			if (GUI.Button(new Rect (screenW * 0.5f - spaceW - w * 1.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}

			if (++ i >= GameManager.totalLevel) {
				break;
			}

			if (GUI.Button(new Rect (screenW * 0.5f - w * 0.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}

			if (++ i >= GameManager.totalLevel) {
				break;
			}

			if (GUI.Button(new Rect (screenW * 0.5f + spaceW + w * 0.5f, y, w, h), "Level " + i, style)) {
				GameManager.LoadLevel (i);
			}

			if (++ i >= GameManager.totalLevel) {
				break;
			}
		}

		if (GUI.Button(new Rect(screenW * 0.5f - w * 0.5f, screenH * 0.5f + (h + spaceH) * (Mathf.Floor(i / 3) + 1),
			w, h), "Tutorial", style )) {
			GameManager.LoadLevel (0);
		}
	}
}
