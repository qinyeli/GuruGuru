// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static int currLevel = -1;
	public static int totalLevel = 4;

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
	}

	void Update() {
		if (Input.GetKey (KeyCode.F1)) {
			NextLevel ();
		}
	}

	static public void LoadLevel(int level) {
		currLevel = level;
		SceneManager.LoadScene ("Scene_0");
	}
		
	static public void NextLevel () {
		currLevel = (currLevel + 1) % totalLevel;
		SceneManager.LoadScene ("Scene_0");
	}

	static public void Reload() {
		SceneManager.LoadScene ("Scene_0");
	}
}
