using UnityEngine;
using System.Collections;

public class Counter_Animation_Triggers : MonoBehaviour {
	public CounterAnimations masterCounterSystem;
	public CounterControl CounterControl;
	public SmoothCamera2D camera;
	public Transform beamEnemyCamera, beamPlayerCamera, beamPointTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
	}
	void CanMoveBeamContact () {
		CounterControl.canMoveContactPoint = true;
		CounterControl.ShowBeamPrompts ();
	}
}
