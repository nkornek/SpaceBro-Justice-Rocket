﻿using UnityEngine;
using System.Collections;

public class Sequence_Queue : MonoBehaviour {

	public GameObject gameManager;
	public Animator playerLeft, playerRight;

	// delay stuff
	public SequenceControls player;
	
	public bool tripleMade;
	public bool movesCorrect, movesFail;
	public float timeBetweenMoves;
	
	// Use this for initialization
	void Start () {
		timeBetweenMoves = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {

		if (movesCorrect)
		{
			movesCorrect = false;

		}
		if (movesFail)
		{
			movesFail = false;

		}
		
	
	}
	/*
	void FixedUpdate () {
		currentTime += Time.deltaTime;
		MoveSpriteDown();
	}
	*/
	
	// Sets the appropriate pictogram sprites and visibility
	public void LoadSequence (int[] seq, float delay) {
		for (int i = 0; i < seq.Length; i++) {
			gameManager.GetComponent<GameControl> ().SetTimer (0);
		}
		
		for (int i = seq.Length; i < sequenceObjects.Length; i++) {

		}

	}
	public void CreateTriples() {

	}


	public void AfterFail(){

	}

}
