﻿using UnityEngine;
using System.Collections;

public class Animation_Triggers : MonoBehaviour {
	public GameControl game;
	public Prompts prompts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndIntro(){
		game.GameStart ();

	}

	void readyPrompt () {
		prompts.showPrompt (0);
	}

	void goPrompts () {
		prompts.showPrompt (1);
	}
}
