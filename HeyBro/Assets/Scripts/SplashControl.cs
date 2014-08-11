using UnityEngine;
using System.Collections;

public class SplashControl : MonoBehaviour {
	public StarSpeedControl starparticles;
	public SpriteRenderer loader;

	// Use this for initialization
	void Start () {
		Invoke ("fadeOut", 2);
		loader = GameObject.Find ("loading").GetComponent<SpriteRenderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void NextLevel () {
		Application.LoadLevel (1);
	}

	void fadeOut () {
		gameObject.GetComponent<Animator> ().SetTrigger ("fadeout");
		Invoke ("slowstars", 0.3f);
	}
	void slowstars () {
		starparticles.slowdown = true;
	}
	void showLoading () {
		loader.enabled = true;
	}
}
