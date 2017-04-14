// Attached to GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	Dictionary<string, AudioClip> soundEffects;
	Dictionary<string, AudioClip> bgms;
	AudioSource bgmSource;

	// Use this for initialization
	void Start () {
		soundEffects = new Dictionary<string, AudioClip> {
			
			{ "coin", Resources.Load<AudioClip> ("Audios/coin") },
			{ "death", Resources.Load<AudioClip> ("Audios/death") },
			{ "goal", Resources.Load<AudioClip> ("Audios/goal") },
			{ "pause", Resources.Load<AudioClip> ("Audios/pause") },
			{ "jump", Resources.Load<AudioClip> ("Audios/jump") } // done!
			//{ "rotate", Resources.Load<AudioClip> ("Audios/rotate") },
		};

		bgms = new Dictionary<string, AudioClip> {
			{"theme", Resources.Load<AudioClip>("BGMs/Intro Theme")}
		};

		bgmSource = GetComponent<AudioSource> ();
		bgmSource.loop = true;
		bgmSource.clip = bgms ["theme"];
		bgmSource.Play ();
	}

	public void Play(string soundEffectName) {
		if (!soundEffects.ContainsKey (soundEffectName)) {
			print ("Error in Audio.cs: sound effect not found!");
			return;
		}

		AudioSource newbie = gameObject.AddComponent<AudioSource> ();
		newbie.clip = soundEffects [soundEffectName];
		newbie.Play ();
		Destroy (newbie, newbie.clip.length);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
