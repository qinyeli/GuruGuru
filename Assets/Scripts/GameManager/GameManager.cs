﻿// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static int currLevel = -1;
	public static int totalLevel = 11;
	public static KeyCode jumpKey = KeyCode.Space;
	public static KeyCode dashKey = KeyCode.LeftShift;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {

		if (scene.name == "Scene_0") {
			print (scene.name + " " + "Level " + currLevel + " loaded...");
			LevelParser.Load (currLevel);
		} 

		if (scene.name == "SceneLevelSelection") {
			print (scene.name + " loaded...");
		}

		if (scene.name == "SceneOpening") {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	void Update() {
		if (Input.GetKey (KeyCode.F1)) {
			NextLevel ();
		}

		if (Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	static public void LoadLevel(int level) {
		currLevel = level;
		SceneManager.LoadScene ("Scene_0");
	}
		
	static public void NextLevel () {
		if (++currLevel < totalLevel) {
			SceneManager.LoadScene ("Scene_0");
		} else {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	static public void Reload() {
		SceneManager.LoadScene ("Scene_0");
	}
}