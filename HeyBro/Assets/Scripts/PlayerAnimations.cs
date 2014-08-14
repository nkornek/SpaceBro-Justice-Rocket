using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	public Cutscene_Camera cutscene;
	public GameControl game;
	public Animator characterAnims;
	public SequenceControls player;
	public HealthBarEnemy enHP;
	public Prompts prompts;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 1)
		{
			characterAnims.SetTrigger("success");
		}
		
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
			characterAnims.SetTrigger("Attack");
			break;
		case 5:
			characterAnims.SetTrigger("gethit");
			break;
		}
	}
	//the following are only checked on left player since both are the same
	void inRange () {
		if (game) {
		game.pictogramsInRange = true;
		}
	}

	void inRangeDef () {
		game.pictogramsInRange = true;
		game.canCounter = true;

	}

	void failCounter () {
		game.canCounter = false;
	}

	void failure () {
		if (game) {
		game.pictogramsInRange = false;
		game.pictogramsFailed = true;
		prompts.sequenceFail();
		}
	}

	void success () {
		resetSpeed ();
		if (game) {
			if (game.playersTurn)
			{
				if (player.correctMoves < player.seqMoves)
				{
					player.generateMove ();
				}
				else 
				{
					game.StartPlayerAttack();
				}
			}
		}
	}

	public void nextTurn () {
		if (game && game.playersTurn)
		{
			game.Invoke ("startEnemyTurn", 1f);
		}
	}

	public void moveSuccess () {
		characterAnims.SetTrigger ("success");
		resetSpeed ();
	}

	void setSpeed () {
		if (enHP) {
		characterAnims.speed = 1 + Mathf.Abs (enHP.curPerc - 1);
		}
	}

	void resetSpeed () {
		characterAnims.speed = 1;
	}
}
