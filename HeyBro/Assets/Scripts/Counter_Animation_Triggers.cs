using UnityEngine;
using System.Collections;

public class Counter_Animation_Triggers : MonoBehaviour {
	public CounterAnimations masterCounterSystem;
	public CounterControl CounterControl;

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
}
