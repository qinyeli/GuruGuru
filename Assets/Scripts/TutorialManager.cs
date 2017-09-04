// Attached to TutorialManager in SceneTutorial

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
	
	Text instruction;
	InputDevice inputDevice = null;

	string[] steps_keyboard = {
		"Hello",
		"Hit Space to Jump",
		"Hold Shift to Dash",
		"The Bright is the Goal."
	};
	string[] steps_controller = {
		"Hello",
		"Hit A to Jump",
		"Hold B to Dash",
		"The Bright is the Goal."
	};
	int currStep = 0;
	bool isTransferring = false;

	void Start () {
		LevelParser.Load (0);
		instruction = GameObject.Find ("Canvas").transform.Find ("Instruction").GetComponent<Text> ();
	}

	void Update () {
		inputDevice = InputManager.ActiveDevice;

		if (inputDevice == null) {
			instruction.text = steps_keyboard [currStep];
		} else {
			instruction.text = steps_controller [currStep];
		}

		if (isTransferring) {
			return;
		}

		switch (currStep) {
		case 0:
			StartCoroutine("WaitAndNextStep", 1f);
			break;
		case 1:
			if (inputDevice.Action1.IsPressed) {
				StartCoroutine("WaitAndNextStep", 1f);
			}
			break;
		case 2:
			if (inputDevice.Action3.IsPressed) {
				StartCoroutine("WaitAndNextStep", 1f);
			}
			break;
		case 3:

			break;
		default:
			break;
		}
	}

	IEnumerator WaitAndNextStep(float t) {
		isTransferring = true;
		yield return new WaitForSeconds (t);
		currStep++;
		isTransferring = false;
	}

}
