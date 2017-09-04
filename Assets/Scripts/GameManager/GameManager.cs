// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class GameManager : MonoBehaviour {

	public static int currLevel = -1;
	public static int totalLevel = 10;
//	public static KeyCode jumpKey = KeyCode.Space;
//	public static KeyCode dashKey = KeyCode.LeftShift;

	static GameManager _instance;

	InputDevice inputDevice = null;

	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
			return;
		} else {
			_instance = this;
		}
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		
		// Print the name of the scene on load
		if (scene.name == "SceneGamePlay") {
			print (scene.name + " " + "Level " + currLevel + " loaded...");
			LevelParser.Load (currLevel);
		}

		if (scene.name == "SceneLevelSelection") {
			print (scene.name + " loaded...");
		}
	}
		
	void Update() {
		inputDevice = InputManager.ActiveDevice;

		switch (SceneManager.GetActiveScene ().name) {
		case "SceneInitial":
			SceneManager.LoadScene ("SceneOpening");
			break;
		case "SceneOpening":
			if (inputDevice.AnyButton || Input.GetKey (KeyCode.Return)) {
				SceneManager.LoadScene ("SceneLevelSelection");
			}
			break;
		default:
			break;
		}

		if (inputDevice.Action4.IsPressed) {
			SceneManager.LoadScene ("SceneOpening");
		}
	}

	static public void LoadLevel(int level) {
		currLevel = level;

		if (currLevel == 0) {
			SceneManager.LoadScene ("SceneTutorial");
		} else {
			SceneManager.LoadScene ("SceneGamePlay");
		}
	}

	static public void LevelClear () {
		if (currLevel == 0) {
			currLevel = totalLevel;
		}
		_instance.StartCoroutine ("WaitAndNextLevel", 1.5f);

	}

	IEnumerator WaitAndNextLevel(float t) {
		yield return new WaitForSeconds (t);
		if (++currLevel < totalLevel) {
			SceneManager.LoadScene ("SceneGamePlay");
		} else {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	static public void ReloadLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
