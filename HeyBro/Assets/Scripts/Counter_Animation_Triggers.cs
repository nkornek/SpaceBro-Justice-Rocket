using UnityEngine;
using System.Collections;

public class Counter_Animation_Triggers : MonoBehaviour {
	public CounterAnimations masterCounterSystem;
	public CounterControl CounterControl;
	public SmoothCamera2D camera;
	public Transform beamEnemyCamera, beamPlayerCamera, ballPointTarget, gunPointTarget, fistPointTarget;
	public Animator backgroundAnimator, enemyAnimator;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setAngle() {
		float randomAngle = Random.Range (86, 94);
		transform.localRotation = Quaternion.Euler (0, 0, randomAngle);
		}

	void FireBallTrigger() {
		masterCounterSystem.FireBall();
	}
	
	public void TriggerReset () {
		CounterControl.Reset ();
	}

	void BeamEnemyCameraTarget() {
		camera.target = beamEnemyCamera;
		camera.cangrow = false;

	}
	void BeamPlayerCameraTarget() {
		camera.target = beamPlayerCamera;
		camera.cangrow = false;
	}

	void showRoulette () {
		CounterControl.showRoulette ();
	}
	void decideRoulette () {
		CounterControl.slowing = true;
	}
	void failedRoulette () {
		if (!CounterControl.failed)
		{
		CounterControl.failed = true;
		}
	}
	void energyBallCameraTarget () {
		camera.target = ballPointTarget;
	}
	void gunCameraTarget () {
		camera.target = gunPointTarget;
	}

	void fistCameraTarget () {
		camera.target = fistPointTarget;
	}
	void reflectBall () {
		if (CounterControl.ballReflected < 3)
		{
			enemyAnimator.SetTrigger("ReflectBall");
		}
		else
		{
			enemyAnimator.SetTrigger("Hit");
		}
	}
	void damagePlayer () {
		CounterControl.damagePlayer ();
	}
	void damageEnemy () {
		CounterControl.damageEnemy ();
	}

	public void randomRPE () {
		int whichRPE = Random.Range (0, 3);
		//pick player req input
		CounterControl.PlayerControl.GetComponent<SequenceControls>().contactA = whichRPE;
		CounterControl.PlayerControl.GetComponent<SequenceControls>().contactB = whichRPE;
		//set enemy to the opposite
		switch (whichRPE) {
		case 0: 
			enemyAnimator.SetTrigger("Rock");
			break;
		case 1: 
			enemyAnimator.SetTrigger("Elbow");
			break;
		case 2: 
			enemyAnimator.SetTrigger("Paper");
			break;
		}
	}
	public void inputRPE () {
		CounterControl.canRPE = true;
	}
	public void failRPE () {
		CounterControl.failed = true;
		CounterControl.canRPE = true;
	}
}
