using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadImage;

	public void LoadLevel(int level) {
		loadImage.SetActive (true);
		GameManager.LoadLevel (level);
	}
}
