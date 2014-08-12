using UnityEngine;
using System.Collections;

public class Counter_Animation_Triggers : MonoBehaviour {
	public CounterAnimations masterCounterSystem;
	public CounterControl CounterControl;
	public SmoothCamera2D camera;
	public Transform beamEnemyCamera, beamPlayerCamera, beamPointTarget;
	public Animator backgroundAnimator;



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
	void BeamPointCameraTarget () {
		camera.target = beamPointTarget;
		camera.cangrow = true;
		Invoke ("CanMoveBeamContact", 1);		
		CounterControl.promptLeft.GetComponentInParent<Animator>().SetTrigger("BeamcounterStart");
	}
	void CanMoveBeamContact () {
		CounterControl.canMoveContactPoint = true;
		CounterControl.ShowBeamPrompts ();
	}

	void enemyFireBeams () {
		masterCounterSystem.FireLasersEnemy ();
	}
	void playerFireBeams () {
		masterCounterSystem.FireLasersPlayer ();
	}
	void sphereBeams () {
		masterCounterSystem.SphereParticleClash ();
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


}
