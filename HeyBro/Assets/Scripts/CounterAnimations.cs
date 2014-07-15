using UnityEngine;
using System.Collections;

public class CounterAnimations : MonoBehaviour {
	public GameObject energyBall, energyBallObject;
	public Animator ballCounterAnimator;
	public SplineNode[] ballSplines;
	public SmoothCamera2D counterCamera;
	public Transform playerTransform, enemyTransform;
	public bool canTrack;

	// Use this for initialization
	void Start () {
		canTrack = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (canTrack)
		{
			if (energyBallObject.transform.localPosition.x > -15.0f & energyBallObject.transform.localPosition.x < -2.0f)
			{
				counterCamera.target = energyBallObject.transform;
			}
			else if (energyBallObject.transform.localPosition.x > -2.0f)
			{
				counterCamera.target = playerTransform;
			}
			else
			{
				counterCamera.target = enemyTransform;
			}
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
		canTrack = true;
	}

	public void ReflectedBall () {
		foreach (SplineNode s in ballSplines)
		{
			s.speed += 5;
		}
	}
}
