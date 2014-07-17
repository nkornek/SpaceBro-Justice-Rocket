﻿using UnityEngine;
using System.Collections;

public class CounterControl : MonoBehaviour {
	public GameObject promptLeft, promptRight, PlayerControl, EnemyControls, GameManager;
	public bool hasResetInput;
	public CounterAnimations CounterAnimations;
	public Camera counterCamera;
	public Transform closeOutCamera;
	public Animator bgAnimator, enemyAnimator;
	public int damage;
	public bool blocked, failed, speedup;
	public Sprite p1FiveNorm, p1FistNorm, p1ElbowNorm, p2FiveNorm, p2FistNorm, p2ElbowNorm;
	public Sprite p1FiveSuccess, p1FistSuccess, p1ElbowSuccess, p2FiveSuccess, p2FistSuccess, p2ElbowSuccess;
	public Sprite p1FiveFail, p1FistFail, p1ElbowFail, p2FiveFail, p2FistFail, p2ElbowFail;


	//specifict counter sequence variables
	public GameObject energyBallObject;
	public int ballReflected;
	public SplineNode ballFirstSpline, ballLastSpline;
	public ParticleSystem fireParticles;

	// Use this for initialization
	void Start () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		damage = 50;
		PlayerControl = GameObject.Find ("Players");
		EnemyControls = GameObject.Find ("Enemy");
		GameManager = GameObject.Find ("Game");
		fireParticles.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GetComponent<GameControl>().counterActive)
		{
			if (!hasResetInput) {
				if (pictogramsInRange ()) {
					PlayerControl.GetComponent<SequenceControls>().detectedA = -1;
					PlayerControl.GetComponent<SequenceControls>().detectedB = -2;
					hasResetInput = true;
				}
			}
			counterSequence(GameManager.GetComponent<GameControl>().counterNum);
		}
		else
		{
			ballReflected = 0;
			failed = false;
		}
	}
	public void counterSequence (int whichSequence) {
		switch (whichSequence) {
			case 1:
			//energy ball counter
			if (energyBallObject.transform.localPosition.x > 12.0f & !blocked & !failed)
			{		
				promptLeft.GetComponent<SpriteRenderer> ().enabled = true;
				promptRight.GetComponent<SpriteRenderer> ().enabled = true;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveNorm;		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveNorm;
				promptLeft.transform.localPosition = new Vector3 (-1 - (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptLeft.transform.localPosition.y, 1f);
				promptRight.transform.localPosition = new Vector3 (1 + (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptRight.transform.localPosition.y, 1f);
			}
			if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() && pictogramsInRange() & !blocked & !failed){
				blocked = true;
				speedup = true;
				ballReflected ++;
				CounterAnimations.toPlayer = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveSuccess;		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveSuccess;
				Invoke ("hidePrompts", 0.2f);
			}
			//player fail
			if (pictogramsFailed() & !failed)
			{
				if (blocked)
				{
					if (speedup)
					{
						speedup = false;
						foreach (SplineNode s in CounterAnimations.ballSplines)
						{
							s.speed += 3;
						}
					}
				}
				else
				{
					failed = true;
					promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveFail;		
					promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveFail;
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;				
					Invoke ("hidePrompts", 0.2f);
					ballLastSpline.type = SplineNode.Type.STOP;					
					Invoke ("endCounter", 1);
					PlayerControl.GetComponent<SequenceControls>().hp -= 20;
				}
			}
			//hit enemy
			if (energyBallObject.transform.localPosition.x <= -18f & blocked)
			{
				blocked = false;
				CounterAnimations.enemyHit();
				CounterAnimations.toPlayer = true;
				if (ballReflected == 3)
				{
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;
					ballFirstSpline.type = SplineNode.Type.STOP;
					damageEnemy();
					Invoke ("endCounter", 1);
				}
			}
			break;
		}
	}

	public void hidePrompts () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		}
	public void endCounter () {
		GameManager.GetComponent<GameControl>().counterActive = false;
		counterCamera.GetComponent<SmoothCamera2D> ().target = closeOutCamera;
		CounterAnimations.IntroOutro (2);
		}
	public void damageEnemy() {
		EnemyControls.GetComponent<EnemyControls>().DamageEnemy (damage);
		}

	public void Reset () {
		GameObject.Find ("Game").GetComponent<GameControl> ().paused = false;
		GameObject.Find ("Game").GetComponent<GameControl> ().Invoke ("startPlayerTurn", 1.0f);
		Destroy (gameObject);
		GameObject go = Instantiate(Resources.Load("Counters")) as GameObject;
		}

	public void StartCounter () {
		GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = false;
		fireParticles.enableEmission = true;
		bgAnimator.SetTrigger ("In");
		enemyAnimator.SetTrigger ("StartIntro");		
		GameManager.GetComponent<GameControl>().counterActive = true;

	}

	private bool pictogramsInRange () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 4);
	}
	private bool pictogramsFailed () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 2.5);
	}

}
