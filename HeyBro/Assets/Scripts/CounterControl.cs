using UnityEngine;
using System.Collections;

public class CounterControl : MonoBehaviour {
	public SequenceControls player; 
	public GameObject promptLeft, promptRight;
	public bool hasResetInput;
	public CounterAnimations CounterAnimations;
	public AudioSource srcSeqSound;
	public AudioClip clipMoveSuccess;
	public AudioClip clipWholeSeqSuccess;
	public bool counterActive;
	public int counterNum;
	public bool blocked, failed;
	public Sprite p1FiveNorm, p1FistNorm, p1ElbowNorm, p2FiveNorm, p2FistNorm, p2ElbowNorm;
	public Sprite p1FiveSuccess, p1FistSuccess, p1ElbowSuccess, p2FiveSuccess, p2FistSuccess, p2ElbowSuccess;
	public Sprite p1FiveFail, p1FistFail, p1ElbowFail, p2FiveFail, p2FistFail, p2ElbowFail;

	//specifict counter sequence variables
	public GameObject energyBallObject;
	public int ballReflected;
	public SplineNode ballFirstSpline, ballLastSpline;

	// Use this for initialization
	void Start () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		counterNum = 1;
		counterActive = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (counterActive)
		{
			if (!hasResetInput) {
				if (pictogramsInRange ()) {
					player.detectedA = -1;
					player.detectedB = -2;
					hasResetInput = true;
				}
			}
			counterSequence(counterNum);
		}
		else
		{
			ballReflected = 0;
			failed = false;
		}
	}
	public void counterSequence (int whichSequence) {
		switch (whichSequence) {
			case 1:
			player.counterInputA = 0;
			player.counterInputB = 0;
			//energy ball counter
			if (energyBallObject.transform.localPosition.x > 12.0f & !blocked & !failed)
			{		
				promptLeft.GetComponent<SpriteRenderer> ().enabled = true;
				promptRight.GetComponent<SpriteRenderer> ().enabled = true;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveNorm;		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveNorm;
				promptLeft.transform.localPosition = new Vector3 (-1 - (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptLeft.transform.localPosition.y, 1f);
				promptRight.transform.localPosition = new Vector3 (1 + (Mathf.Abs(energyBallObject.transform.localPosition.x - 27))/3, promptRight.transform.localPosition.y, 1f);
			}
			if (player.checkBothEvents() && pictogramsInRange() & !blocked & !failed){
				blocked = true;
				ballReflected ++;
				CounterAnimations.toPlayer = false;
				promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveSuccess;		
				promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveSuccess;
				Invoke ("hidePrompts", 0.2f);
			}
			//player fail
			if (pictogramsFailed() & !failed)
			{
				if (blocked)
				{
					foreach (SplineNode s in CounterAnimations.ballSplines)
					{
						s.speed += 3;
					}
				}
				else
				{
					failed = true;
					promptLeft.GetComponent<SpriteRenderer>().sprite = p1FiveFail;		
					promptRight.GetComponent<SpriteRenderer>().sprite = p2FiveFail;
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;				
					Invoke ("hidePrompts", 0.2f);
					ballLastSpline.type = SplineNode.Type.STOP;
				}
			}
			//hit enemy
			if (energyBallObject.transform.localPosition.x <= -18f & blocked)
			{
				blocked = false;
				if (ballReflected == 3)
				{
					energyBallObject.GetComponent<ParticleSystem>().Emit(800);
					energyBallObject.GetComponent<ParticleSystem>().enableEmission = false;
					energyBallObject.GetComponent<SpriteRenderer>().enabled = false;
					ballFirstSpline.type = SplineNode.Type.STOP;
				}
			}
			break;
		}
	}

	public void hidePrompts () {
		promptLeft.GetComponent<SpriteRenderer> ().enabled = false;
		promptRight.GetComponent<SpriteRenderer> ().enabled = false;
		}
	public void endCounter () {
		}

	private bool pictogramsInRange () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 4);
	}
	private bool pictogramsFailed () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 2.5);
	}
}
