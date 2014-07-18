using UnityEngine;
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


	//specifict counter sequence variables
	public GameObject energyBallObject;
	public int ballReflected;
	public SplineNode ballFirstSpline, ballLastSpline;
	public ParticleSystem fireParticles;
	public Sprite[] p1Ball;
	public Sprite[] p2Ball;

	public bool canMoveContactPoint;
	public Transform contactSphere;
	public ParticleSystem[] enemyBeam;
	public ParticleSystem[] playerBeam;
	public int fivesLaser;
	public Animator laserAnimator;
	public Sprite[] p1laser;
	public Sprite[] p2laser;

	// Use this for initialization
	void Start () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		damage = 50;
		PlayerControl = GameObject.Find ("Players");
		EnemyControls = GameObject.Find ("Enemy");
		GameManager = GameObject.Find ("Game");
		fireParticles.enableEmission = false;
		fivesLaser = 10;
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
			if (energyBallObject.transform.localPosition.x > 12.0f & !blocked & !failed)
			{		
				promptLeft.GetComponent<SpriteRenderer> ().enabled = true;
				promptRight.GetComponent<SpriteRenderer> ().enabled = true;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1Ball[0];		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2Ball[0];
				promptLeft.transform.localPosition = new Vector3 (-1 - (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptLeft.transform.localPosition.y, 1f);
				promptRight.transform.localPosition = new Vector3 (1 + (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptRight.transform.localPosition.y, 1f);
			}
			if (PlayerControl.GetComponent<SequenceControls>().checkBothEvents() && pictogramsInRangeBall() & !blocked & !failed){
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
						foreach (SplineNode s in CounterAnimations.ballSplines)
						{
							s.speed += 3;
						}
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
		case 2:
			//lasers counter
			if (canMoveContactPoint)
			{


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

	public void ShowBeamPrompts () {
		promptLeft.GetComponent<SpriteRenderer>().enabled = true;
		promptRight.GetComponent<SpriteRenderer>().enabled = true;
		}

	public void StartCounter () {
		GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = false;
		bgAnimator.SetTrigger ("In");
		if (GameManager.GetComponent<GameControl>().counterNum == 1)
		{
			fireParticles.enableEmission = true;
			enemyAnimator.SetTrigger ("StartIntro");		
			GameManager.GetComponent<GameControl>().counterActive = true;
		}
		else if (GameManager.GetComponent<GameControl>().counterNum == 2)
		{
			laserAnimator.SetTrigger("StartCounter");
			promptLeft.GetComponent<SpriteRenderer>().sprite = p1laser[0];		
			promptRight.GetComponent<SpriteRenderer>().sprite = p2laser[0];
			promptLeft.transform.localScale = new Vector3 (4, 4, 1);		
			promptRight.transform.localScale = new Vector3 (4, 4, 1);
			promptLeft.transform.localPosition = new Vector3 (-9.5f, 0, promptLeft.transform.localPosition.z);
			promptRight.transform.localPosition = new Vector3 (3.5f, 7.5f, promptLeft.transform.localPosition.z);
			promptLeft.transform.localRotation = Quaternion.Euler (0, 0, 30);			
			promptRight.transform.localRotation = Quaternion.Euler (0, 0, 30);
		}
	}

	private bool pictogramsInRangeBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 4);
	}
	private bool pictogramsFailedBall () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 2.5);
	}

}
