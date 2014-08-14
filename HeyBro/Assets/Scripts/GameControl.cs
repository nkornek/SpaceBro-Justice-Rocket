using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	//public SequenceControls player;	// player script
	public SequenceControls player; 
	public EnemyControls enemy; 	// enemy script
	public GameObject playerLeft, playerRight;

	public int turn; 
	public bool charging;
	public bool playersTurn;

	public bool seqGenerated; 		// true if a sequence has been generated but not completed 
	public bool hasResetInput;		// Used to detect if we've reset input already

	public AudioSource srcSeqSound;
	public AudioClip clipMoveSuccess;
	public AudioClip clipWholeSeqSuccess;
	public AudioClip clipBlockSuccess;
	
	public AudioSource srcRobot;
	public AudioSource srcPlayersDie;

	public bool canTime;

	public bool tripleActive, paused;
	public tripleScript tripleScript;
	public Animator enemyAnimations;
	public Enemy_Particles enemyParticleParent;
	public Particle_Deactivate playerParticles;	
	
	public bool counterActive;
	public int counterNum;
	public bool pictogramsInRange, pictogramsFailed, canCounter;

	public cameraShake mainCamera;
	public Animator cutsceneAnim;
	public int attackNum;

	public Prompts prompts;

	void Start () {
		paused = false;
	}

	public void GameStart () {
		hasResetInput = false;
		canTime = true;
		startPlayerTurn();
	}

	void Update () {

		if (!paused) {
			if (playersTurn) playerTurn ();
			else enemyTurn ();

			
			if (enemy.hp <= 0){
				Invoke ("loadSplashScreen", 5.0f);
				paused = true;
				prompts.win ();
				enemyAnimations.SetTrigger("Laugh");

			}
			
			else if (player.hp <= 0){
				Invoke ("loadSplashScreen", 5.0f);
				paused = true;
				prompts.lose ();
				enemyAnimations.SetTrigger("ZeroHealth");
			}
		}

	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * ~ PLAYER'S TURN
	 * (1)	Generate a sequence
	 * (2)	Check if player succeeds current move
	 * (3)	check if finished seq
	 * (4)	if finished, check #correct moves = compare with number of moves in the sequence => deal damage to enemy
	 * (5)	check enemy HP (if 0 => win game)
	 * 
	 * ~ ENEMY'S TURN 
	 * (6)	randomly generate an attack	
	 * (7)	if player counters within counter window => counter minigame
	 * (8)	else if player blocks within block window => nothing happens
	 * (9)	else if player fails => damage player
	 * 
	 * ~ GENERAL CHECKS
	 * (10)	check enemy HP (if 0 => win game)
	 * (11)	check player HP (if 0 => lose game)
	 * PLAYER'S TURN AGAIN
	 * -------------------------------------------------------------------------------------------------------------------------- */



	private void startPlayerTurn () {
		if (!paused)
		{
			playersTurn = true;
			seqGenerated = false;
			canTime = true;
		}
		else
		{
			Invoke ("startPlayerTurn", 0.2f);
		}
	}
	
	private void startEnemyTurn () {
		if (!paused)
		{
			playersTurn = false;
			Invoke ("createBlockSequence", 1.3f);
			prompts.showPrompt(2);
		}
		else
		{
			Invoke ("startEnemyTurn", 0.2f);
		}
	}
	
	private void createBlockSequence () {
		player.generateBlockSequence ();

		//////put variables to choose which attack 
		attackNum = Random.Range (0, 3);
		switch (attackNum) {
		case 0:
			enemyAnimations.SetTrigger ("StartCharge");
			break;
		case 1:
			enemyAnimations.SetTrigger ("Siphon");
			break;
		case 2:
			enemyAnimations.SetTrigger ("PunchCharge");
			break;
		}
	}
		
	private void checkBlocked () {
		player.defending = false;
		enemyAnimations.SetTrigger ("Attack");

	}

	private void playerTurn(){
		// (1) generate a sequence
		if (!seqGenerated){
			player.generateSequence();
			seqGenerated = true; 
			turn++;
		}
		
		else if (!paused) {
			if (!hasResetInput) {
				if (pictogramsInRange) {
					player.detectedA = -1;
					player.detectedB = -2;
					canTime = true;
					hasResetInput = true;
				}
			}

			//check pass/fail for regular inputs

			if (pictogramsFailed) {
				pictogramsFailed = false;

			}
			else if (player.checkBothEvents() & pictogramsInRange){
				hasResetInput = false;
				player.onSuccess();
				pictogramsInRange = false;		
				player.correctMoves++;

				if (player.correctMoves < player.seqMoves & !tripleActive) {
					srcSeqSound.clip = clipMoveSuccess;
					srcSeqSound.Play ();	
				}
				else if (player.correctMoves == player.seqMoves)
				{					
					srcSeqSound.clip = clipWholeSeqSuccess;
					srcSeqSound.Play ();
					prompts.sequenceSuccess();
				}
			}
		}
	}
	public void StartPlayerAttack () {
			cutsceneAnim.SetTrigger("Cutscene 1");
	}
	public void PlayerAttack () {			
			playerLeft.GetComponent<PlayerAnimations>().SetAnim (3);
			playerRight.GetComponent<PlayerAnimations>().SetAnim (3);
			playerParticles.partVisible();
			enemyAnimations.SetTrigger("Hurt");
			enemy.DamageEnemy (player.seqMoves * 20);
			if (enemy.hp <= 0) {
				playersTurn = false;
				srcRobot.Play ();
			}
			else {
				Invoke ("startEnemyTurn", 2);
			}
		}
	public void enemyTurn(){
		if (!paused)
		{
		if (player.defending) {
			if (player.checkBothEvents() && pictogramsInRange) {
					playerLeft.GetComponent<PlayerAnimations>().characterAnims.SetBool ("Blocking", false);
					playerRight.GetComponent<PlayerAnimations>().characterAnims.SetBool ("Blocking", false);
					player.onSuccess();
					pictogramsInRange = false;
				if (canCounter)
				{
					enemyParticleParent.attackAudio.Stop ();
					prompts.showPrompt(3);
					canCounter = false;
					player.defending = false;
					//counterNum = Random.Range (1, 4);
					counterNum = attackNum + 1;
					if (GameObject.Find ("Counters"))
					{
						GameObject.Find ("Counters").GetComponent<CounterControl>().Invoke ("StartCounter", 0.3f);
					}
					else
					{
						GameObject.Find ("Counters(Clone)").GetComponent<CounterControl>().Invoke ("StartCounter", 0.3f);
					}

					paused = true;
					enemyAnimations.SetTrigger("FailCharge");
					enemyParticleParent.chargeVisible = false;
					player.contactA = 0;
					player.contactB = 0;
					player.defending = false;
				}
				else
				{
					player.blocked = true;
					checkBlocked ();
					prompts.showPrompt(4);
				}
			}
			if (pictogramsFailed) {
				playerLeft.GetComponent<PlayerAnimations>().characterAnims.SetBool ("Blocking", false);
				playerRight.GetComponent<PlayerAnimations>().characterAnims.SetBool ("Blocking", false);
				pictogramsFailed = false;
				checkBlocked ();
				
			}
		}
		}
	}

	public void LaserDamage() {
		if (!player.blocked) {
			player.hp -= 20;
			mainCamera.Shake();
			playerLeft.GetComponent<PlayerAnimations>().SetAnim(5);
			playerRight.GetComponent<PlayerAnimations>().SetAnim(5);
		}
		if (player.hp <= 0) {
			srcPlayersDie.Play ();
			Invoke ("loadSplashScreen", 5.0f);
			paused = true;
		}
		else {
			Invoke ("startPlayerTurn", 5.0f);
			enemyParticleParent.Invoke("EndField", 1);
		}
	}

	public void SiphonAttack () {
		if (!player.blocked) {
			enemyParticleParent.startSiphonParticles();
			player.hp -= 20;
			enemy.hp += 40;
			mainCamera.Shake();
			playerLeft.GetComponent<PlayerAnimations>().SetAnim(5);
			playerRight.GetComponent<PlayerAnimations>().SetAnim(5);
		}
		if (player.hp <= 0) {
			srcPlayersDie.Play ();
			Invoke ("loadSplashScreen", 5.0f);
			paused = true;
		}
		else {
			Invoke ("startPlayerTurn", 5.0f);
			enemyParticleParent.Invoke("EndField", 1);
		}
	}

	public void PunchAttack () {
		if (!player.blocked) {
			player.hp -= 30;
			mainCamera.Shake();
			playerLeft.GetComponent<PlayerAnimations>().SetAnim(5);
			playerRight.GetComponent<PlayerAnimations>().SetAnim(5);
		}
		if (player.hp <= 0) {
			srcPlayersDie.Play ();
			Invoke ("loadSplashScreen", 5.0f);
			paused = true;
		}
		else {
			Invoke ("startPlayerTurn", 5.0f);
			enemyParticleParent.Invoke("EndField", 1);
		}
	}

	private void loadSplashScreen () {
		Application.LoadLevel (0);
	}

}
