using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	//public SequenceControls player;	// player script
	public SequenceControls player; 
	public EnemyControls enemy; 	// enemy script
	public Sequence_Queue seqQueueLeft;
	public Sequence_Queue seqQueueRight;

	public int turn; 
	public bool charging;
	public bool playersTurn;

	public bool hi5; 				// begin and end a battle with a hi5
	public bool seqGenerated; 		// true if a sequence has been generated but not completed 
	public bool hasResetInput;		// Used to detect if we've reset input already

	public enum reaction { block, counter, fail };

	public float responseTime; 

	public AudioSource srcSeqSound;
	public AudioClip clipMoveSuccess;
	public AudioClip clipWholeSeqSuccess;
	public AudioClip clipBlockSuccess;
	
	public AudioSource srcRobot;
	public AudioSource srcPlayersDie;
	public float moveTimeToFail, timeLeft, tripleTimeToFail, baseBlockTime, maxTime, timerPercentage;
	public bool canTime, canEmit;

	public Transform timerBar;
	public SpriteRenderer timerSprite, timerOutline;
	public Sprite timerYellow, timerGreen, timerRed;
	public GameObject timerParticles, passfailParticles;

	public bool sceneStarted, tripleActive, paused;
	public tripleScript tripleScript;
	public Animator enemyAnimations;
	public Enemy_Particles enemyParticleParent;
	
	public bool counterActive;
	public int counterNum;

	void Start () {
		moveTimeToFail = 4.0f;	
		tripleTimeToFail = 6.0f;
		baseBlockTime = 1.0f;
		maxTime = 4.0f;
		sceneStarted = true;
		paused = false;

	}

	public void GameStart () {
		hi5 = true;
		hasResetInput = false;
		canTime = true;
		canEmit = true;
		startPlayerTurn();
	}

	void Update () {
			timerPercentage = timeLeft / maxTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}

			//set particle speed & ebable
			if (canEmit & !paused) {
				timerSprite.enabled = true;			
				timerOutline.enabled = true;
				foreach (ParticleSystem p in timerParticles.GetComponentsInChildren<ParticleSystem>()) {
					p.enableEmission = true;
				}
			if (timerPercentage > 0.2f) {
				timerSprite.sprite = timerYellow;
				foreach (ParticleSystem p in timerParticles.GetComponentsInChildren<ParticleSystem>()) {
					p.startColor = Color.yellow;
					p.startSpeed = timerPercentage * 4;
					}
				} 
				else {
					timerSprite.sprite = timerRed;
					foreach (ParticleSystem p in timerParticles.GetComponentsInChildren<ParticleSystem>()) {
						p.startColor = Color.red;
						p.startSpeed = 4;
					}
				}
			} 
			else {
					timerSprite.enabled = false;
					timerOutline.enabled = false;
					foreach (ParticleSystem p in timerParticles.GetComponentsInChildren<ParticleSystem>()) {
						p.enableEmission = false;
					}
				}
			//set bar length
			timerBar.localScale = new Vector3 (1.0f, timerPercentage, 1.0f);
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
	 * (7)	if player counters within counter window => damage enemy
	 * (8)	else if player blocks within block window => nothing happens
	 * (9)	else if player fails => damage player
	 * 
	 * ~ GENERAL CHECKS
	 * (10)	check enemy HP (if 0 => win game)
	 * (11)	check player HP (if 0 => lose game)
	 * PLAYER'S TURN AGAIN
	 * -------------------------------------------------------------------------------------------------------------------------- */

	

	void FixedUpdate(){
	if (!paused) {
		responseTime += Time.deltaTime; 

		if (hi5){
			if (playersTurn) playerTurn ();
			else enemyTurn ();
		}

		//else if (player.detectedA == 1 && player.detectedB == 4){
		else if (player.palmA && player.palmB){
			hi5 = true; 
		}	

		if (enemy.hp <= 0){
			Debug.LogWarning ("Win");
			Invoke ("loadSplashScreen", 5.0f);
//			Application.LoadLevel("Win");
		}

		else if (player.hp <= 0){
			Debug.LogWarning ("Lose");
			player.sp.Close(); 
//			Application.LoadLevel("Lose");
		}
//		player.attacking = true; 
//		player.defending = false; 
	}
	}

	private void startPlayerTurn () {
		if (!paused)
		{
			playersTurn = true;
			seqGenerated = false;
			canTime = true;
			canEmit = true;
			GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (-1);
			GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (-1);
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
			Invoke ("createBlockSequence", 2);
			//seqQueueLeft.movingSpritesDown = false;
			//seqQueueRight.movingSpritesDown = false;
		}
		else
		{
			Invoke ("startEnemyTurn", 0.2f);
		}
	}
	
	private void createBlockSequence () {
		player.generateBlockSequence ();		
		canEmit = true;
		seqQueueLeft.LoadSequence (player.contactA, player.seqDelay);
		seqQueueRight.LoadSequence (player.contactB, player.seqDelay);
		enemyParticleParent.chargeVisible = true;
		enemyAnimations.SetTrigger ("StartCharge");
		if (!canTime)
		{
			canTime = true;
			SetTimer(2);
		}
		//Hax!
		//Invoke ("checkBlocked", 8.0f);
	}
		
	private void checkBlocked () {
		player.defending = false;
		enemyParticleParent.chargeVisible = false;
		enemyParticleParent.Invoke ("FireLasers", 0.7f);

	}

	private void playerTurn(){
		if (canTime)
		{
			timeLeft -= Time.deltaTime;
		}
		// (1) generate a sequence
		if (!seqGenerated){
			player.generateSeqParams(); 
			player.generateSequence(player.currentSeq);
			seqQueueLeft.LoadSequence (player.contactA, player.seqDelay);
			seqQueueRight.LoadSequence (player.contactB, player.seqDelay);
			seqGenerated = true; 
			turn++;
		}
		
		else if (!paused) {
			if (!hasResetInput) {
				if (pictogramsInRange ()) {
					player.detectedA = -1;
					player.detectedB = -2;
					canTime = true;
					hasResetInput = true;
				}
			}

			//check pass/fail for regular inputs

			if (pictogramsFailed ()) {
				if (!tripleActive)
				{
					seqQueueLeft.GetComponent<Sequence_Queue>().movesFail = true;
					seqQueueRight.GetComponent<Sequence_Queue>().movesFail = true;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("AfterFail", 1);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("AfterFail", 1);
					startEnemyTurn ();
					canTime = false;
					canEmit = false;
					enemyAnimations.SetTrigger("Laugh");
					passfailParticles.particleSystem.startColor = Color.red;
					passfailParticles.particleSystem.Emit(400);
				}
				else 
				{
					if (tripleScript.tripleSeqNum == 1)
					{
						tripleScript.TripleFail1(player.tripleInputA);
						tripleScript.TripleFail1(player.tripleInputB + 3);
					}
					else if (tripleScript.tripleSeqNum == 2)
					{
						tripleScript.TripleFail2(player.tripleInputA);
						tripleScript.TripleFail2(player.tripleInputB + 3);
					}
					else if (tripleScript.tripleSeqNum == 3)
					{
						tripleScript.TripleFail3(player.tripleInputA);
						tripleScript.TripleFail3(player.tripleInputB + 3);
					}
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("AfterFail", 1);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("AfterFail", 1);
					tripleScript.Invoke("TripleEnd", 1);
					startEnemyTurn ();
					canTime = false;
					canEmit = false;
					enemyAnimations.SetTrigger("Laugh");
					passfailParticles.particleSystem.startColor = Color.red;
					passfailParticles.particleSystem.Emit(400);
				}
			}
			else if (player.checkBothEvents() && pictogramsInRange()){
				if (!tripleActive)
				{
					hasResetInput = false;
					player.correctMoves++;
					seqQueueLeft.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueRight.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					canTime = false;
					passfailParticles.particleSystem.startColor = Color.green;
					passfailParticles.particleSystem.Emit(400);
					timerSprite.sprite = timerGreen; 
					//seqQueueLeft.GetComponent<Sequence_Queue>().MoveSpriteForward();
					//seqQueueRight.GetComponent<Sequence_Queue>().MoveSpriteForward();
					//player.generateNextMove();
				}
				else
				{
					hasResetInput = false;
					if (tripleScript.tripleSeqNum == 1)
					{
						tripleScript.TripleSuccess1(player.tripleInputA);
						tripleScript.TripleSuccess1(player.tripleInputB + 3);
						passfailParticles.particleSystem.Emit(200);
						srcSeqSound.clip = clipMoveSuccess;
						srcSeqSound.Play ();
						GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
						GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
					}
					if (tripleScript.tripleSeqNum == 2)
					{
						tripleScript.TripleSuccess2(player.tripleInputA);
						tripleScript.TripleSuccess2(player.tripleInputB + 3);
						passfailParticles.particleSystem.Emit(300);
						srcSeqSound.clip = clipMoveSuccess;
						srcSeqSound.Play ();
						GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
						GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
					}
					if (tripleScript.tripleSeqNum == 3)
					{
						tripleScript.TripleSuccess3(player.tripleInputA);
						tripleScript.TripleSuccess3(player.tripleInputB + 3);
						passfailParticles.particleSystem.Emit(400);
						srcSeqSound.clip = clipMoveSuccess;
						srcSeqSound.Play ();
						GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
						GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (player.tripleInputA);
					}
					tripleScript.tripleSeqNum ++;				
				}
				if (tripleScript.tripleSeqNum > 3)
				{
					tripleScript.tripleSeqNum = 1;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);		
					tripleScript.Invoke("TripleEnd", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					canTime = false;
					tripleActive = false;
					paused = true;
					Invoke ("PlayerAttack", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					player.generateNextMove ();
				}

				if (player.correctMoves < player.seqMoves & !tripleActive) {
					srcSeqSound.clip = clipMoveSuccess;
					srcSeqSound.Play ();
					GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (player.contactA[player.currentMove]);
					GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (player.contactB[player.currentMove]);
					player.generateNextMove ();
				}
				if (player.correctMoves >= player.seqMoves & !tripleActive & !paused) {
					int randomInt = Random.Range (0, 7);
					tripleScript.GenerateTriple(randomInt);
					tripleActive = true;
				}
			}

			/** Yeah fuck all this  
			player.seqMoves--; 
			// check if sequence is finished
			if (player.seqMoves <= 0){
				// check if all moves in sequence were correctly done  
				if (player.correctMoves >= player.seqMoves){ 
					//deal damage
				//	enemy.DamageEnemy(player.seqDamage);
				}
				player.seqGenerated = false; // to generate a new sequence 
				player.attacking = false;
				player.defending = true;
				//playersTurn = false;
			}
			*/			
		}
	}
	public void PlayerAttack () {			
			srcSeqSound.clip = clipWholeSeqSuccess;
			srcSeqSound.Play ();
			GameObject.Find ("Player_Left").GetComponent<PlayerAnim>().SetSprite (3);
			GameObject.Find ("Player_Right").GetComponent<PlayerAnim>().SetSprite (3);			
			canEmit = false;
			//					player.attacking = false;
			//					player.defending = true;
			if (enemy.hp <= 0) {
				playersTurn = false;
				srcRobot.Play ();
			}
			else {
				startEnemyTurn ();
			}
		}
	public void enemyTurn(){
		if (!paused)
		{
		//enemy.generateAttack(); 
		responseTime = 0;
		//playerResponse(); 
		if (canTime)
		{
			timeLeft -= Time.deltaTime;
		}
		if (player.defending) {
			if (player.checkBothEvents() && pictogramsInRange()) {
				if (timerPercentage >= 0.6)
				{
					player.defending = false;
					counterNum = Random.Range (1, 3);
					if (GameObject.Find ("Counters"))
					{
						GameObject.Find ("Counters").GetComponent<CounterControl>().Invoke ("StartCounter", 0.3f);
					}
					else
					{
						GameObject.Find ("Counters(Clone)").GetComponent<CounterControl>().Invoke ("StartCounter", 0.3f);
					}
					seqQueueLeft.sequenceObjects[0].GetComponent<SpriteRenderer>().enabled = false;
					seqQueueRight.sequenceObjects[0].GetComponent<SpriteRenderer>().enabled = false;
					seqQueueLeft.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueRight.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					passfailParticles.particleSystem.startColor = Color.green;
					passfailParticles.particleSystem.Emit(400);
					canEmit = false;
					canTime = false;
					timerSprite.sprite = timerGreen;
					paused = true;
					enemyAnimations.SetTrigger("FailCharge");
					enemyParticleParent.chargeVisible = false;
					player.counterInputA = 0;
					player.counterInputB = 0;
				}
				else
				{
					player.blocked = true;
					checkBlocked ();
					seqQueueLeft.sequenceObjects[0].GetComponent<SpriteRenderer>().enabled = false;
					seqQueueRight.sequenceObjects[0].GetComponent<SpriteRenderer>().enabled = false;
					seqQueueLeft.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueRight.GetComponent<Sequence_Queue>().movesCorrect = true;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("MoveSpriteForward", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					passfailParticles.particleSystem.startColor = Color.green;
					passfailParticles.particleSystem.Emit(400);
					canEmit = false;
					canTime = false;
					timerSprite.sprite = timerGreen;
				}
			}
			if (pictogramsFailed ()) {
				checkBlocked ();
				for (int i = 0; i < 6; i++) {
					seqQueueLeft.GetComponent<Sequence_Queue>().movesFail = true;
					seqQueueRight.GetComponent<Sequence_Queue>().movesFail = true;
					seqQueueLeft.GetComponent<Sequence_Queue>().Invoke ("AfterFail", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					seqQueueRight.GetComponent<Sequence_Queue>().Invoke ("AfterFail", seqQueueLeft.GetComponent<Sequence_Queue>().timeBetweenMoves);
					passfailParticles.particleSystem.startColor = Color.red;
					passfailParticles.particleSystem.Emit(400);
					canTime = false;
				}
				
			}
		}
		}
	}

	private void playerResponse(){
		int resp = player.enemyResponse(); 
		switch (resp){
			case (int) reaction.counter:
				if (responseTime <= enemy.attackParams[(int) enemy.currentAttack][3]){
					gameObject.SendMessage("DamageEnemy", player.counterDamage); 
					responseTime = 0; 
				}
				break;


			case (int) reaction.fail:
				if (responseTime <= enemy.attackParams[(int) enemy.currentAttack][2]){
				//	player.hp -= (int) enemy.attackParams[(int) enemy.currentAttack][0]; 
					responseTime = 0;
				}
				break; 

			case (int) reaction.block:
				break;

			default:
				break; 
		}
	}

	public void LaserDamage() {
		if (!player.blocked) {
			player.hp -= 20;
		}
		if (player.hp <= 0) {
			srcPlayersDie.Play ();
			Invoke ("loadSplashScreen", 5.0f);			
		}
		else {
			Invoke ("startPlayerTurn", 5.0f);
		}
	}
	
	private bool pictogramsInRange () {
		//Just check left, they're the same
		if (!tripleActive)
		{
			return (Mathf.Abs (seqQueueLeft.sequenceObjects[player.currentMove].transform.localPosition.z) == 1);
		}
		else
		{
			return (true);
		}
	}

	public bool pictogramsFailed () {
		return (timeLeft <= 0);
	}

	public void SetTimer (int timerSwitch) {
		switch (timerSwitch) {
		case 0:
			maxTime = moveTimeToFail - (0.25f * player.correctMoves) - (1 - GameObject.Find("Enemy Health Parent").GetComponent<HealthBarEnemy>().curPerc);
			break;
		case 1:
			maxTime = tripleTimeToFail - (1 - GameObject.Find("Enemy Health Parent").GetComponent<HealthBarEnemy>().curPerc);
			break;
		case 2:
			maxTime = baseBlockTime + (2.0f * GameObject.Find("Enemy Health Parent").GetComponent<HealthBarEnemy>().curPerc);
			break;
				}
		timeLeft = maxTime;

	}

	private void loadSplashScreen () {
		Application.LoadLevel ("SplashScreenScene");
	}

}
