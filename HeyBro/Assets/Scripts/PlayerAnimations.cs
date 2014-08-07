using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	public Cutscene_Camera cutscene;
	public GameControl game;
	public Animator characterAnims;
	public SequenceControls player;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetAnim (int anim) {
		switch (anim) {
		case 0: 
			characterAnims.SetTrigger("five");
			break;
		case 1:
			characterAnims.SetTrigger("punch");
			break;
		case 2:
			characterAnims.SetTrigger("elbow");	
			break;
		case 3:
			break;
			/*
		case 4: 
			characterAnims.SetTrigger("sad");
			break;
			*/
		case 5:
			characterAnims.SetTrigger("gethit");
			break;
		}
	}
	//the following are only checked on left player since both are the same
	void inRange () {
		game.pictogramsInRange = true;
	}

	void inRangeDef () {
		game.pictogramsInRange = true;
		game.canCounter = true;

	}

	void failCounter () {
		game.canCounter = false;
	}

	void failure () {
		game.pictogramsInRange = false;
		game.pictogramsFailed = true;
	}

	void success () {
		if (player.correctMoves < player.seqMoves)
		{
			player.generateMove ();
		}
		else 
		{
			game.StartPlayerAttack();
		}
	}

	public void nextTurn () {
		if (game.playersTurn == true)
		{
			game.Invoke ("startEnemyTurn", 1f);
		}
	}

	public void moveSuccess () {
		characterAnims.SetTrigger ("success");
	}
}
