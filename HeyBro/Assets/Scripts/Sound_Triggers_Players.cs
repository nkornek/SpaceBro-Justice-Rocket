using UnityEngine;
using System.Collections;

public class Sound_Triggers_Players : MonoBehaviour {
	AudioSource audio;
	public AudioClip eyeguyShort, eyeguyLong, gatorShort, gatorLong, playersExcited, playersCelebrate, playersSad, playersAngry;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void ClipEyeguyShort () {
		audio.clip = eyeguyShort;
		audio.Play ();
	}
	public void ClipEyeguyLong () {
		audio.clip = eyeguyLong;
		audio.Play ();
	}
	public void ClipGatorShort () {
		audio.clip = gatorShort;
		audio.Play ();
	}
	public void ClipGatorLong () {
		audio.clip = gatorLong;
		audio.Play ();
	}
	public void ClipLetsDoThis () {
		audio.clip = playersExcited;
		audio.Play ();
	}
	public void ClipCelebrate () {
		audio.clip = playersCelebrate;
		audio.Play ();
	}
	public void ClipSad () {
		audio.clip = playersSad;
		audio.Play ();
	}
	public void ClipAngry () {
		audio.clip = playersAngry;
		audio.Play ();
	}
	public void PlayerSoundStop () {
		audio.Stop ();
	}
}
