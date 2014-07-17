using UnityEngine;
using System.Collections;

public class CounterAnimations : MonoBehaviour {
	public GameObject energyBall, energyBallObject;
	public Animator ballCounterAnimator, BG;
	public SplineNode[] ballSplines;
	public SmoothCamera2D counterCamera;
	public Transform playerTransform, enemyTransform;
	public bool toPlayer;
	public ParticleSystem toPlayerPart, fromPlayerPart;
	public CounterControl CounterControl;
	public GameControl game;

	// Use this for initialization
	void Start () {	
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = false;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = false;
		game = GameObject.Find ("Game").GetComponent<GameControl>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (game.counterActive == true & game.counterNum == 1)
		{
			if (energyBallObject.transform.localPosition.x > -12.0f & energyBallObject.transform.localPosition.x < 12.0f)
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
			else if (energyBallObject.transform.localPosition.x > 12.0f)
			{
				counterCamera.target = playerTransform;
			}
			else
			{
				counterCamera.target = enemyTransform;
			}
		}
	}

	public void FireBall (){
		energyBallObject.GetComponent<SplineController> ().enabled = true;
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = true;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = true;
		ballCounterAnimator.SetBool ("hasFired", true);
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
}
