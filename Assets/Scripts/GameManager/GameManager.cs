// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class GameManager : MonoBehaviour {

	public static int currLevel = -1;
	public static int totalLevel = 10;
	public static KeyCode jumpKey = KeyCode.Space;
	public static KeyCode dashKey = KeyCode.LeftShift;

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

	void Start() {
		if (SceneManager.GetActiveScene ().name == "SceneInitial") {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	void Update() {
		inputDevice = InputManager.ActiveDevice;

		if (Input.GetKey (KeyCode.F1)) {
			NextLevel ();
		}

		if (inputDevice.Action4.IsPressed) {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	static public void LoadLevel(int level) {
		currLevel = level;
		SceneManager.LoadScene ("SceneGamePlay");
	}
		
	static public void NextLevel () {
		if (++currLevel < totalLevel) {
			SceneManager.LoadScene ("SceneGamePlay");
		} else {
			SceneManager.LoadScene ("SceneLevelSelection");
		}
	}

	static public void Reload() {
		SceneManager.LoadScene ("SceneGamePlay");
	}
}
