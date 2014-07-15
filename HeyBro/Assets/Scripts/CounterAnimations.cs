using UnityEngine;
using System.Collections;

public class CounterAnimations : MonoBehaviour {
	public GameObject energyBall, energyBallObject;
	public Animator ballCounterAnimator;
	public SplineNode[] ballSplines;
	public SmoothCamera2D counterCamera;
	public Transform playerTransform, enemyTransform;
	public bool toPlayer;
	public ParticleSystem toPlayerPart, fromPlayerPart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (energyBallObject.transform.localPosition.x > -12.0f & energyBallObject.transform.localPosition.x < 4.0f)
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
		else if (energyBallObject.transform.localPosition.x > 4.0f)
		{
			counterCamera.target = playerTransform;
			}
			else
			{
				counterCamera.target = enemyTransform;
			}
	}

	public void FireBall (){
		energyBallObject.GetComponent<SplineController> ().enabled = true;
		energyBallObject.GetComponent<SpriteRenderer> ().enabled = true;
		ballCounterAnimator.SetBool ("hasFired", true);
		toPlayer = true;
	}

	public void ReflectedBall () {
		toPlayer = false;
		foreach (SplineNode s in ballSplines)
		{
			s.speed += 5;
		}
	}
}
