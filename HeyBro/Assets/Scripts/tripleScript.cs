using UnityEngine;
using System.Collections;

public class tripleScript : MonoBehaviour {
	public Sprite tripleFiveLeft, tripleFistLeft, tripleElbowLeft;
	public Sprite tripleFiveSuccessLeft, tripleFistSuccessLeft, tripleElbowSuccessLeft;
	public Sprite tripleFiveFailLeft, tripleFistFailLeft, tripleElbowFailLeft;
	public SpriteRenderer triple1Left, triple2Left, triple3Left;
	public Sprite tripleFiveRight, tripleFistRight, tripleElbowRight;
	public Sprite tripleFiveSuccessRight, tripleFistSuccessRight, tripleElbowSuccessRight;
	public Sprite tripleFiveFailRight, tripleFistFailRight, tripleElbowFailRight;
	public SpriteRenderer triple1Right, triple2Right, triple3Right;
	public GameObject gameManager, seqControl, seqQueueLeft, seqQueueRight;
	public int tripleSeqNum;

	// Use this for initialization
	void Start () {
		triple1Left.enabled = false;
		triple2Left.enabled = false;
		triple3Left.enabled = false;
		triple1Right.enabled = false;
		triple2Right.enabled = false;
		triple3Right.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.GetComponent<GameControl>().tripleActive)
		{
			if (gameManager.GetComponent<GameControl>().canTime == true)
			{
				SetAlphas(tripleSeqNum);
			}
			if (tripleSeqNum == 1)
			{
				if (triple1Left.sprite == tripleFiveLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 0;}
				else if (triple1Left.sprite == tripleFistLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 1;}
				else if (triple1Left.sprite == tripleElbowLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 2;}
				if (triple1Right.sprite == tripleFiveRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 0;}
				else if (triple1Right.sprite == tripleFistRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 1;}
				else if (triple1Right.sprite == tripleElbowRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 2;}
			}
			else if (tripleSeqNum == 2)
			{
				if (triple2Left.sprite == tripleFiveLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 0;}
				else if (triple2Left.sprite == tripleFistLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 1;}
				else if (triple2Left.sprite == tripleElbowLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 2;}
				if (triple2Right.sprite == tripleFiveRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 0;}
				else if (triple2Right.sprite == tripleFistRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 1;}
				else if (triple2Right.sprite == tripleElbowRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 2;}
			}
			else if (tripleSeqNum == 3)
			{
				if (triple3Left.sprite == tripleFiveLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 0;}
				else if (triple3Left.sprite == tripleFistLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 1;}
				else if (triple3Left.sprite == tripleElbowLeft)
				{seqControl.GetComponent<SequenceControls>().tripleInputA = 2;}
				if (triple3Right.sprite == tripleFiveRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 0;}
				else if (triple3Right.sprite == tripleFistRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 1;}
				else if (triple3Right.sprite == tripleElbowRight)
				{seqControl.GetComponent<SequenceControls>().tripleInputB = 2;}
			}
		}	
	}

	public void GenerateTriple (int makeTriple) {
		triple1Left.enabled = true;
		triple2Left.enabled = true;
		triple3Left.enabled = true;
		triple1Right.enabled = true;
		triple2Right.enabled = true;
		triple3Right.enabled = true;
		tripleSeqNum = 1;
		gameManager.GetComponent<GameControl> ().SetTimer (1);
	switch (makeTriple){
		case 0:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFiveLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFiveRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 1:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFistRight;
			triple2Right.sprite = tripleFiveRight;
			triple3Right.sprite = tripleFistRight;
			break;
		case 2:			
			triple1Left.sprite = tripleFistLeft;
			triple2Left.sprite = tripleFiveLeft;
			triple3Left.sprite = tripleFistLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 3:			
			triple1Left.sprite = tripleFistLeft;
			triple2Left.sprite = tripleElbowLeft;
			triple3Left.sprite = tripleFistLeft;
			triple1Right.sprite = tripleFistRight;
			triple2Right.sprite = tripleElbowRight;
			triple3Right.sprite = tripleFistRight;
			break;
		case 4:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleElbowLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleElbowRight;
			break;
		case 5:			
			triple1Left.sprite = tripleElbowLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleElbowRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 6:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		}
	}
	public void SetAlphas (int switchAlpha) {
		switch (switchAlpha) {
		case 1:
			triple1Left.color = new Color(1f, 1f, 1f, 1f);
			triple1Right.color = new Color(1f, 1f, 1f, 1f);
			triple2Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple2Right.color = new Color(1f, 1f, 1f, 0.35f);
			triple3Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple3Right.color = new Color(1f, 1f, 1f, 0.35f);
			break;
		case 2:
			triple1Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple1Right.color = new Color(1f, 1f, 1f, 0.35f);
			triple2Left.color = new Color(1f, 1f, 1f, 1f);
			triple2Right.color = new Color(1f, 1f, 1f, 1f);
			triple3Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple3Right.color = new Color(1f, 1f, 1f, 0.35f);
			break;
		case 3:
			triple1Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple1Right.color = new Color(1f, 1f, 1f, 0.35f);
			triple2Left.color = new Color(1f, 1f, 1f, 0.35f);
			triple2Right.color = new Color(1f, 1f, 1f, 0.35f);
			triple3Left.color = new Color(1f, 1f, 1f, 1f);
			triple3Right.color = new Color(1f, 1f, 1f, 1f);
			break;
		}
	}
	public void TripleSuccess1 (int SwitchSpritesSuccess1) {
		switch (SwitchSpritesSuccess1){
		case 0:
			triple1Left.sprite = tripleFiveSuccessLeft;
			break;
		case 1:
			triple1Left.sprite = tripleFistSuccessLeft;
			break;
		case 2:
			triple1Left.sprite = tripleElbowSuccessLeft;
			break;
		case 3:
			triple1Right.sprite = tripleFiveSuccessRight;
			break;
		case 4:
			triple1Right.sprite = tripleFistSuccessRight;
			break;
		case 5:
			triple1Right.sprite = tripleElbowSuccessRight;
			break;
		}
	}
	public void TripleSuccess2 (int SwitchSpritesSuccess2) {
		switch (SwitchSpritesSuccess2){
		case 0:
			triple2Left.sprite = tripleFiveSuccessLeft;
			break;
		case 1:
			triple2Left.sprite = tripleFistSuccessLeft;
			break;
		case 2:
			triple2Left.sprite = tripleElbowSuccessLeft;
			break;
		case 3:
			triple2Right.sprite = tripleFiveSuccessRight;
			break;
		case 4:
			triple2Right.sprite = tripleFistSuccessRight;
			break;
		case 5:
			triple2Right.sprite = tripleElbowSuccessRight;
			break;
		}
	}
	public void TripleSuccess3 (int SwitchSpritesSuccess3) {
		switch (SwitchSpritesSuccess3){
		case 0:
			triple3Left.sprite = tripleFiveSuccessLeft;
			break;
		case 1:
			triple3Left.sprite = tripleFistSuccessLeft;
			break;
		case 2:
			triple3Left.sprite = tripleElbowSuccessLeft;
			break;
		case 3:
			triple3Right.sprite = tripleFiveSuccessRight;
			break;
		case 4:
			triple3Right.sprite = tripleFistSuccessRight;
			break;
		case 5:
			triple3Right.sprite = tripleElbowSuccessRight;
			break;
		}
	}
	public void TripleFail1 (int SwitchSpritesFail1) {
		switch (SwitchSpritesFail1){
		case 0:
			triple1Left.sprite = tripleFiveFailLeft;
			break;
		case 1:
			triple1Left.sprite = tripleFistFailLeft;
			break;
		case 2:
			triple1Left.sprite = tripleElbowFailLeft;
			break;
		case 3:
			triple1Right.sprite = tripleFiveFailRight;
			break;
		case 4:
			triple1Right.sprite = tripleFistFailRight;
			break;
		case 5:
			triple1Right.sprite = tripleElbowFailRight;
			break;
		}
	}
	public void TripleFail2 (int SwitchSpritesFail2) {
		switch (SwitchSpritesFail2){
		case 0:
			triple2Left.sprite = tripleFiveFailLeft;
			break;
		case 1:
			triple2Left.sprite = tripleFistFailLeft;
			break;
		case 2:
			triple2Left.sprite = tripleElbowFailLeft;
			break;
		case 3:
			triple2Right.sprite = tripleFiveFailRight;
			break;
		case 4:
			triple2Right.sprite = tripleFistFailRight;
			break;
		case 5:
			triple2Right.sprite = tripleElbowFailRight;
			break;
		}
	}
	public void TripleFail3 (int SwitchSpritesFail3) {
		switch (SwitchSpritesFail3){
		case 0:
			triple3Left.sprite = tripleFiveFailLeft;
			break;
		case 1:
			triple3Left.sprite = tripleFistFailLeft;
			break;
		case 2:
			triple3Left.sprite = tripleElbowFailLeft;
			break;
		case 3:
			triple3Right.sprite = tripleFiveFailRight;
			break;
		case 4:
			triple3Right.sprite = tripleFistFailRight;
			break;
		case 5:
			triple3Right.sprite = tripleElbowFailRight;
			break;
		}
	}
	public void TripleEnd () {
		triple1Left.enabled = false;
		triple2Left.enabled = false;
		triple3Left.enabled = false;
		triple1Right.enabled = false;
		triple2Right.enabled = false;
		triple3Right.enabled = false;	
		gameManager.GetComponent<GameControl>().tripleActive = false;
		seqQueueLeft.GetComponent<Sequence_Queue>().tripleMade = false;
		seqQueueRight.GetComponent<Sequence_Queue>().tripleMade = false;
		}
}
