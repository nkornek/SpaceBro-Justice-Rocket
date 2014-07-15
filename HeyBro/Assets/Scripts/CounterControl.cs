using UnityEngine;
using System.Collections;

public class CounterControl : MonoBehaviour {
	public SequenceControls player; 
	public GameObject promptLeft, promptRight;
	public bool hasResetInput;

	public AudioSource srcSeqSound;
	public AudioClip clipMoveSuccess;
	public AudioClip clipWholeSeqSuccess;
	public bool counterActive;
	public int counterSequence;
	public bool blocked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (counterActive)
		{
		}
		if (!hasResetInput) {
			if (pictogramsInRange ()) {
				player.detectedA = -1;
				player.detectedB = -2;
				hasResetInput = true;
			}
		}
	}
	public void countersequence (int whichSequence) {
		switch (whichSequence) {
			case 1:
			break;
		}
	}

	private bool pictogramsInRange () {
		return (Mathf.Abs (promptLeft.transform.localPosition.x - promptRight.transform.localPosition.x) <= 1);
	}
}
