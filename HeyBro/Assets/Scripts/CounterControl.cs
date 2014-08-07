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
	public GameObject[] counterSpritesEnemy;


	//specifict counter sequence variables
	//ball
	public GameObject energyBallObject;
	public int ballReflected;
	public Sprite[] p1Ball;
	public Sprite[] p2Ball;

	//beam
	public bool canMoveContactPoint;
	public Transform contactSphere;
	public ParticleSystem[] enemyBeam;
	public ParticleSystem[] playerBeam;
	public int fivesLaser;
	public Sprite[] p1laser;
	public Sprite[] p2laser;
	public float enemyBeamPush;

	//roulette
	public int roulettePrompt;
	public Sprite[] rouletteLeft;
	public Sprite[] rouletteRight;
	public bool spinning;

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
		fivesLaser = 0;
		enemyBeamPush = 0.5f;

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
		foreach (GameObject g in counterSpritesEnemy)
		{
			g.GetComponent<SpriteRenderer>().enabled = false;
		}
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
				CounterAnimations.toPlayer = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[1];		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[1];
				Invoke ("hidePrompts", 0.2f);
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
					promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[2];		
					promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[2];
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;				
					Invoke ("hidePrompts", 0.2f);			
					Invoke ("endCounter", 1);
					damagePlayer();
				}
			}
			//hit enemy
			if (energyBallObject.transform.localPosition.y >= 60 & blocked)
			{
				blocked = false;
				CounterAnimations.enemyHit();
				CounterAnimations.toPlayer = true;
				energyBallObject.GetComponent<SplineController>().Duration -= 1;
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
			//lasers counter
			if (canMoveContactPoint)
			{
				enemyBeamPush -= Time.deltaTime;
				if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() & pictogramsInRangeLaser() & !failed)
				{
					fivesLaser += 1;
					promptLeft.GetComponent<SpriteRenderer>().sprite = p1laser[1];
					promptRight.GetComponent<SpriteRenderer>().sprite = p2laser[1];
					Invoke ("ResetLaserPrompts", 0.1f);
					contactSphere.Translate (Vector3.forward * 2, Space.Self);
					foreach (ParticleSystem pp in playerBeam)
					{
						pp.startLifetime += 0.06f;
						pp.startSpeed += 1;
					}
					foreach (ParticleSystem pe in enemyBeam)
					{
						pe.startLifetime -= 0.06f;
						pe.startSpeed -= 1;
					}
				}
				if (enemyBeamPush <= 0)
				{
					fivesLaser -= 1;
					enemyBeamPush = 0.5f;
					contactSphere.Translate (Vector3.back * 2, Space.Self);
					foreach (ParticleSystem pp in playerBeam)
					{
						pp.startLifetime -= 0.06f;
						pp.startSpeed -= 1;
					}
					foreach (ParticleSystem pe in enemyBeam)
					{
						pe.startLifetime += 0.06f;
						pe.startSpeed += 1;
					}
				}
				if (pictogramsWonLaser() || pictogramsFailedLaser() )
				{
					canMoveContactPoint = false;
					promptLeft.GetComponentInParent<Animator>().SetTrigger("BeamcounterEnd");
					counterAnimatorEnemy.SetTrigger("EndCounter");
					hidePrompts();
					if (pictogramsWonLaser() )
					{
						counterAnimatorEnemy.SetTrigger("PlayerWon");
						Invoke ("damageEnemy", 1);
						Invoke ("endCounter", 3);
						foreach (ParticleSystem pe in enemyBeam)
						{
							pe.enableEmission = false;
						}
					}
					else if (pictogramsFailedLaser() )
					{
						counterAnimatorEnemy.SetTrigger("EnemyWon");
						Invoke ("damagePlayer", 1);
						Invoke ("endCounter", 3);
						foreach (ParticleSystem pp in playerBeam)
						{
							pp.enableEmission = false;
						}
					}
				}
			}
			break;
		case 3:
			if (spinning)
			{
				roulettePrompt ++;
				if (roulettePrompt > 2)	{roulettePrompt = 0;}
				promptLeft.GetComponent<SpriteRenderer>().sprite = rouletteLeft[roulettePrompt];
				promptRight.GetComponent<SpriteRenderer>().sprite = rouletteRight[roulettePrompt];
			}
			else
			{				
				PlayerControl.GetComponent<SequenceControls>().contactA = roulettePrompt;
				PlayerControl.GetComponent<SequenceControls>().contactB = roulettePrompt;
			}
			if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() & pictogramsInRangeRoulette() & !failed)
			{
				promptLeft.GetComponent<SpriteRenderer>().sprite = rouletteLeft[roulettePrompt + 3];
				promptRight.GetComponent<SpriteRenderer>().sprite = rouletteRight[roulettePrompt + 3];
			}
			break;
		}
	}

	public void ResetLaserPrompts () {
		promptLeft.GetComponent<SpriteRenderer>().sprite = p1laser[0];
		promptRight.GetComponent<SpriteRenderer>().sprite = p2laser[0];
		}

	public void hidePrompts () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		}
	public void endCounter () {
		GameManager.GetComponent<GameControl>().counterActive = false;
		counterCamera.GetComponent<SmoothCamera2D> ().target = closeOutCamera;
		CounterAnimations.IntroOutro (2);
		foreach (ParticleSystem pe in enemyBeam)
		{
			pe.enableEmission = false;
		}
		foreach (ParticleSystem pp in playerBeam)
		{
			pp.enableEmission = false;
		}
		}
	public void damageEnemy() {
		EnemyControls.GetComponent<EnemyControls>().DamageEnemy (damage);
		}
	public void damagePlayer() {
		PlayerControl.GetComponent<SequenceControls>().hp -= 20;
		}

	public void Reset () {
		GameObject.Find ("Game").GetComponent<GameControl> ().paused = false;
		GameObject.Find ("Game").GetComponent<GameControl> ().Invoke ("startPlayerTurn", 1.0f);
		Destroy (gameObject);
		GameObject go = Instantiate(Resources.Load("Counters")) as GameObject;
		}

	public void ShowBeamPrompts () {
		promptLeft.GetComponent<SpriteRenderer>().enabled = true;
		promptRight.GetComponent<SpriteRenderer>().enabled = true;
		foreach (ParticleSystem pp in playerBeam)
		{
			pp.startLifetime = 1;
		}
		foreach (ParticleSystem pe in enemyBeam)
		{
			pe.startLifetime = 1;
		}
	}
	public void showRoulette() {
		promptLeft.GetComponent<SpriteRenderer>().enabled = true;
		promptRight.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void StartCounter () {
		GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = false;
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
			foreach (GameObject g in counterSpritesEnemy)
			{
				g.GetComponent<SpriteRenderer>().enabled = true;

			}
		}
		else if (GameManager.GetComponent<GameControl>().counterNum == 2)
		{
			counterAnimatorEnemy.SetTrigger("Start Laser");
			promptLeft.GetComponent<SpriteRenderer>().sprite = p1laser[0];		
			promptRight.GetComponent<SpriteRenderer>().sprite = p2laser[0];
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
			foreach (GameObject g in counterSpritesEnemy)
			{
				g.GetComponent<SpriteRenderer>().enabled = true;

			}
		}
		else if (GameManager.GetComponent<GameControl>().counterNum == 3)
		{
			counterAnimatorEnemy.SetTrigger("Start Roulette");
			spinning = true;
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
			foreach (GameObject g in counterSpritesEnemy)
			{
				g.GetComponent<SpriteRenderer>().enabled = true;
				
			}
		}
	}

	private bool pictogramsInRangeBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 4);
	}
	private bool pictogramsFailedBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 2.5);
	}
	private bool pictogramsInRangeLaser () {
		//check left both are the same
		return (promptLeft.GetComponent<SpriteRenderer>().sprite == p1laser[0]);
	}
	private bool pictogramsFailedLaser () {
		return (fivesLaser <= -13);
	}
	private bool pictogramsWonLaser () {
		return (fivesLaser >= 13);
	}
	private bool pictogramsInRangeRoulette () {
		return (!spinning);
	}
}
