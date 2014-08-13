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

	// Use this for initialization
	void Start () {	
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = false;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = false;
		game = GameObject.Find ("Game").GetComponent<GameControl>();
		toPlayerPart.enableEmission = false;
		fromPlayerPart.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (game.counterActive == true & game.counterNum == 1)
		{
			if (energyBallObject.transform.localPosition.y > -10 & energyBallObject.transform.localPosition.y < 45)
			{
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
				fromPlayerPart.enableEmission = false;
				toPlayerPart.enableEmission = false;
			}
			else
			{
				fromPlayerPart.enableEmission = false;
				toPlayerPart.enableEmission = false;
			}
		}
	}

	public void FireBall (){
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = true;
		energyBallObject.GetComponent<ParticleSystem> ().enableEmission = true;
		energyBallObject.GetComponent<Animator>().SetTrigger("Fire");
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
