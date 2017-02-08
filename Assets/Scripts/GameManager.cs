using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static int currLevel = -1;
	public static int totalLevel = 3;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		SceneManager.sceneLoaded += SceneLoaded;

		NextLevel ();
	}

	static void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {

		if (scene.name == "Scene_0") {
			print (scene.name + " " + "Level " + currLevel + " loaded...");

			LevelParser.Load (currLevel);
		} else {
			print (scene.name + " loaded...");
		}
	}
		
	public void NextLevel () {
		currLevel = (currLevel + 1) % 3;
		SceneManager.LoadScene ("Scene_0");
	}
}
