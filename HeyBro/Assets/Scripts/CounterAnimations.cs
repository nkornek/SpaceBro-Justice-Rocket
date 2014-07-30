using UnityEngine;
using System.Collections;

public class CounterAnimations : MonoBehaviour {
	public GameObject energyBallObject;
	public Animator ballCounterAnimator, BG;
	public SmoothCamera2D counterCamera;
	public Transform playerTransform, enemyTransform;
	public bool toPlayer;
	public ParticleSystem toPlayerPart, fromPlayerPart;
	public CounterControl CounterControl;
	public GameControl game;
	public ParticleSystem[] enemyBeam;
	public ParticleSystem[] playerBeam;
	public ParticleSystem[] sphere;

	// Use this for initialization
	void Start () {	
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = false;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = false;
		game = GameObject.Find ("Game").GetComponent<GameControl>();
		toPlayerPart.enableEmission = false;
		fromPlayerPart.enableEmission = false;
		foreach (ParticleSystem pe in enemyBeam)
		{
			pe.enableEmission = false;
		}
		foreach (ParticleSystem pe in playerBeam)
		{
			pe.enableEmission = false;
		}
		foreach (ParticleSystem pe in sphere)
		{
			pe.enableEmission = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (game.counterActive == true & game.counterNum == 1)
		{
			if (energyBallObject.transform.localPosition.y > -10 & energyBallObject.transform.localPosition.y < 45)
			{
				counterCamera.target = energyBallObject.transform;
				if (toPlayer)
				{
					toPlayerPart.enableEmission = true;
					fromPlayerPart.enableEmission = false;
				}
				else
				{
					toPlayerPart.enableEmission = false;
					fromPlayerPart.enableEmission = true;
				}
			}
			else if (energyBallObject.transform.localPosition.y < -10)
			{
				counterCamera.target = playerTransform;
				fromPlayerPart.enableEmission = false;
				toPlayerPart.enableEmission = false;
			}
			else
			{
				counterCamera.target = enemyTransform;
				fromPlayerPart.enableEmission = false;
				toPlayerPart.enableEmission = false;
			}
		}
	}

	public void FireBall (){
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = true;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = true;
		energyBallObject.GetComponent<SplineController> ().FollowSpline ();
		toPlayer = true;
	}
	public void enemyHit () {
		ballCounterAnimator.SetTrigger ("Hit");
	}

	public void IntroOutro(int bgTransition) {
		switch (bgTransition) {
		case 1:
			BG.SetTrigger("In");
			break;
		case 2:
			BG.SetTrigger("Out");
			break;
		}
	}

	public void FireLasersEnemy () {
		foreach (ParticleSystem pe in enemyBeam)
		{
			pe.Play();
			pe.enableEmission = true;
		}
	}
	public void FireLasersPlayer () {
		foreach (ParticleSystem pe in playerBeam)
		{
			pe.Play();
			pe.enableEmission = true;
		}
	}
	public void SphereParticleClash () {
		foreach (ParticleSystem pe in sphere)
		{
			pe.Play();
			pe.enableEmission = true;
		}
	}
}
