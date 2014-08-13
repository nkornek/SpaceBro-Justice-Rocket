using UnityEngine;
using System.Collections;

public class ExplosionSounds : MonoBehaviour {
	public AudioClip[] explosion;
	public AudioSource soundEffects;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void explosionAudio () {
		int randomInt = Random.Range (0, 2);
		soundEffects.clip = explosion[randomInt];
		soundEffects.Play ();
	}
}
