using UnityEngine;
using System.Collections;

public class CounterControl : MonoBehaviour {
	public GameObject promptLeft, promptRight, PlayerControl, EnemyControls, GameManager;
	public bool hasResetInput;
	public CounterAnimations CounterAnimations;
	public Camera counterCamera;
	public Transform closeOutCamera;
	public Animator bgAnimator;
	public int damage;
	public bool blocked, failed, speedup;
	public Animator counterAnimatorEnemy;
	public GameObject[] counterSpritesPlayers;
	public GameObject counterSpritesEnemy;
	public cameraShake counterCameraShake;
	public Animator playerLeft, playerRight;
	public Display_Forcefield forcefield, mainForcefield;


	//specifict counter sequence variables
	//ball
	public GameObject energyBallObject;
	public int ballReflected;
	public Sprite[] p1Ball;
	public Sprite[] p2Ball;



	//roulette
	public int roulettePrompt;
	public Sprite[] rouletteLeft;
	public Sprite[] rouletteRight;
	public bool spinning, canSwitchRoulette, RouletteSpeed, slowing;
	public float rouletteChangeTime;
	public AudioClip rouletteDing;
	public bool canRoulette;

	//RPE
	public bool canRPE;

	// Use this for initialization
	void Awake () {
		promptLeft = GameObject.Find ("P1_Prompt_back");
		promptRight = GameObject.Find ("P2_Prompt_back");
		promptLeft.transform.localPosition = new Vector3 (-4, 0, 0);
		promptRight.transform.localPosition = new Vector3 (4, 0, 0);
		damage = 50;
		PlayerControl = GameObject.Find ("Players");
		EnemyControls = GameObject.Find ("Enemy");
		GameManager = GameObject.Find ("Game");
		rouletteChangeTime = 0.1f;
		forcefield.showField = false;
		mainForcefield = GameObject.FindWithTag("mainField").GetComponent<Display_Forcefield>();

		foreach (GameObject g in counterSpritesPlayers)
		{
			foreach (SpriteRenderer r in g.GetComponentsInChildren<SpriteRenderer>())
			{
				r.enabled = false;
			}
			foreach (ParticleSystem p in g.GetComponentsInChildren<ParticleSystem>())
			{
				p.enableEmission = false;
			}
		}
		foreach (SpriteRenderer g in counterSpritesEnemy.GetComponentsInChildren<SpriteRenderer>())
		{
			g.enabled = false;
		}
		energyBallObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GetComponent<GameControl>().counterActive)
		{
			if (!hasResetInput) {
				if (pictogramsInRangeBall ()) {
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
			if (energyBallObject.transform.localPosition.y < 0 & !blocked & !failed)
			{		
				promptLeft.GetComponent<SpriteRenderer> ().enabled = true;
				promptRight.GetComponent<SpriteRenderer> ().enabled = true;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[0];		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[0];
				promptLeft.transform.localPosition = new Vector3 (-1 - (Mathf.Abs(energyBallObject.transform.localPosition.y + 18))/4, promptLeft.transform.localPosition.y, 1f);
				promptRight.transform.localPosition = new Vector3 (1 + (Mathf.Abs(energyBallObject.transform.localPosition.y + 18))/4, promptRight.transform.localPosition.y, 1f);
			}
			if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() && pictogramsInRangeBall() & !blocked & !failed & energyBallObject.transform.localPosition.y < 0){
				blocked = true;
				speedup = true;
				ballReflected ++;
				energyBallObject.GetComponent<Animator>().SetInteger("ReturnedNum", ballReflected);
				energyBallObject.GetComponent<Animator>().SetTrigger("Return");
				CounterAnimations.toPlayer = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[1];		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[1];
				Invoke ("hidePrompts", 0.2f);
				gameObject.GetComponent<AudioSource>().Play();
				playerLeft.SetTrigger("five");
				playerRight.SetTrigger("five");
				forcefield.showField = true;
			}
			//player fail
			if (pictogramsFailedBall() & !failed)
			{
				if (blocked)
				{
					if (speedup)
					{
						speedup = false;
					}
				}
				else
				{
					failed = true;
					energyBallObject.GetComponent<Animator>().SetTrigger("Failed");
					promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[2];		
					promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[2];
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;				
					Invoke ("hidePrompts", 0.2f);	
					damagePlayer();
				}
			}
			//hit enemy
			if (energyBallObject.transform.localPosition.y >= 60 & blocked)
			{
				blocked = false;
				forcefield.showField = false;
				//CounterAnimations.enemyHit();
				CounterAnimations.toPlayer = true;
				if (ballReflected == 3)
				{
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;
					damageEnemy();
					Invoke ("endCounter", 1);
				}
			}
			break;
		case 2:
			if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() & pictogramsInRangeRPE() & !failed)
			{
				canRPE = false;
				counterAnimatorEnemy.SetTrigger("Won");
				int whichRPE = PlayerControl.GetComponent<SequenceControls>().contactA;
				forcefield.showField = true;
				switch (whichRPE) {
				case 0:
					playerLeft.SetTrigger("five");
					playerRight.SetTrigger("five");
					break;
				case 1:
					playerLeft.SetTrigger("fist");
					playerRight.SetTrigger("fist");
					break;
				case 2:
					playerLeft.SetTrigger("elbow");
					playerRight.SetTrigger("elbow");
					break;
				}
			}

			break;
		case 3:
			if (spinning)
			{
				if (canSwitchRoulette) 
				{
					Invoke ("spinRoulette", rouletteChangeTime);
					canSwitchRoulette = false;
				}
				if (slowing & spinning & rouletteChangeTime < 0.6f)
				{
					rouletteChangeTime += 0.05f;
				}
			}
			else
			{				
				PlayerControl.GetComponent<SequenceControls>().contactA = roulettePrompt;
				PlayerControl.GetComponent<SequenceControls>().contactB = roulettePrompt;
			}
			if (failed & canRoulette)
			{
				canRoulette = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = rouletteLeft[roulettePrompt + 6];
				promptRight.GetComponent<SpriteRenderer>().sprite = rouletteRight[roulettePrompt + 6];
				Invoke ("hidePrompts", 0.2f);
			}
			else if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() & pictogramsInRangeRoulette() & !failed & canRoulette)
			{
				canRoulette = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = rouletteLeft[roulettePrompt + 3];
				promptRight.GetComponent<SpriteRenderer>().sprite = rouletteRight[roulettePrompt + 3];
				forcefield.showField = true;
				counterAnimatorEnemy.SetTrigger("Won");
				Invoke ("hidePrompts", 0.2f);
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
		counterCameraShake.Shake();
		Invoke ("endCounter", 1);
		}
	public void damagePlayer() {
		PlayerControl.GetComponent<SequenceControls>().hp -= 20;
		counterCameraShake.Shake();
		playerLeft.SetTrigger ("gethit");
		playerRight.SetTrigger ("gethit");
		Invoke ("endCounter", 1);
		}

	public void Reset () {
		GameObject.Find ("Game").GetComponent<GameControl> ().paused = false;
		GameObject.Find ("Game").GetComponent<GameControl> ().Invoke ("startPlayerTurn", 1.0f);
		Destroy (gameObject);
		GameObject go = Instantiate(Resources.Load("Counters")) as GameObject;
		}
	
	public void showRoulette() {
		promptLeft.GetComponent<SpriteRenderer>().enabled = true;
		promptRight.GetComponent<SpriteRenderer>().enabled = true;
		canRoulette = true;
	}

	public void spinRoulette () {
		roulettePrompt ++;
		if (roulettePrompt > 2)	{roulettePrompt = 0;}
		promptLeft.GetComponent<SpriteRenderer>().sprite = rouletteLeft[roulettePrompt];
		promptRight.GetComponent<SpriteRenderer>().sprite = rouletteRight[roulettePrompt];
		canSwitchRoulette = true;
		if (promptLeft.GetComponent<SpriteRenderer>().enabled == true)
		{
			gameObject.GetComponent<AudioSource> ().clip = rouletteDing;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
		if (rouletteChangeTime >= 0.5f)
		{
			spinning = false;
			canSwitchRoulette = false;
			promptLeft.GetComponent<ParticleSystem>().Emit(200);
			promptRight.GetComponent<ParticleSystem>().Emit(200);
		}
		}

	public void StartCounter () {
		mainForcefield.showField = false;
		bgAnimator.SetTrigger ("In");	
		GameManager.GetComponent<GameControl>().counterActive = true;
		if (GameManager.GetComponent<GameControl>().counterNum == 1)
		{
			counterAnimatorEnemy.SetTrigger ("Start Ball");
			foreach (GameObject g in counterSpritesPlayers)
			{
				foreach (SpriteRenderer r in g.GetComponentsInChildren<SpriteRenderer>())
				{
					r.enabled = true;
				}
				foreach (ParticleSystem p in g.GetComponentsInChildren<ParticleSystem>())
				{
					p.enableEmission = true;
				}
			}
			foreach (SpriteRenderer g in counterSpritesEnemy.GetComponentsInChildren<SpriteRenderer>())
			{
				g.enabled = true;
			}
		}
		else if (GameManager.GetComponent<GameControl>().counterNum == 2)
		{
			counterAnimatorEnemy.SetTrigger ("Start RPE");
			foreach (GameObject g in counterSpritesPlayers)
			{
				foreach (SpriteRenderer r in g.GetComponentsInChildren<SpriteRenderer>())
				{
					r.enabled = true;
				}
				foreach (ParticleSystem p in g.GetComponentsInChildren<ParticleSystem>())
				{
					p.enableEmission = true;
				}
			}
			foreach (SpriteRenderer g in counterSpritesEnemy.GetComponentsInChildren<SpriteRenderer>())
			{
				g.enabled = true;
			}
		}
		else if (GameManager.GetComponent<GameControl>().counterNum == 3)
		{
			counterAnimatorEnemy.SetTrigger("Start Roulette");
			spinning = true;
			canSwitchRoulette = true;
			roulettePrompt = Random.Range (0, 3);
			foreach (GameObject g in counterSpritesPlayers)
			{
				foreach (SpriteRenderer r in g.GetComponentsInChildren<SpriteRenderer>())
				{
					r.enabled = true;
				}
				foreach (ParticleSystem p in g.GetComponentsInChildren<ParticleSystem>())
				{
					p.enableEmission = true;
				}
			}
			foreach (SpriteRenderer g in counterSpritesEnemy.GetComponentsInChildren<SpriteRenderer>())
			{
				g.enabled = true;
			}
		}
	}

	private bool pictogramsInRangeBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 4);
	}
	private bool pictogramsFailedBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 2.5);
	}
	private bool pictogramsInRangeRoulette () {
		return (!spinning);
	}
	private bool pictogramsInRangeRPE () {
		return (canRPE);
	}

}
