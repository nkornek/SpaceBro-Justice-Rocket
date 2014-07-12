using UnityEngine;
using System.Collections;

public class Sequence_Queue : MonoBehaviour {

	public GameObject[] sequenceObjects;
	public GameObject gameManager;
	private Sprite[] sequenceSprites;
	
	public Sprite pictogramHighfive, pictogramFist, pictogramElbow, pictogramTriple;
	public Sprite highSuccess, fistSuccess, elbSuccess;
	public Sprite highFail, fistFail, elbFail;
	public int playerNum;

	// delay stuff
	public SequenceControls player; 
	public float seqDelay;
	public float currentTime;
	public int zDistanceBetweenPictograms;
	
	public bool movingSpritesForward;
	public bool movesCorrect, movesFail;
	public float timeBetweenMoves;
	
	// Use this for initialization
	void Start () {
		sequenceSprites = new Sprite[3] {pictogramHighfive, pictogramFist, pictogramElbow};
		zDistanceBetweenPictograms = 1;
		timeBetweenMoves = 0.2f;

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject o in sequenceObjects)
		{
			if (o.transform.localPosition.z == -1)
			{
				o.GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				o.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		if (movesCorrect)
		{
			movesCorrect = false;
			foreach (GameObject o in sequenceObjects)
			{
				if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramHighfive)
				{
					o.GetComponent<SpriteRenderer>().sprite = highSuccess;
				}
				else if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramFist)
				{
					o.GetComponent<SpriteRenderer>().sprite = fistSuccess;
				}
				else if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramElbow)
				{
					o.GetComponent<SpriteRenderer>().sprite = elbSuccess;
				}
			}
		}
		if (movesFail)
		{
			movesFail = false;
			foreach (GameObject o in sequenceObjects)
			{
				if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramHighfive)
				{
					o.GetComponent<SpriteRenderer>().sprite = highFail;
				}
				if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramFist)
				{
					o.GetComponent<SpriteRenderer>().sprite = fistFail;
				}
				if (o.transform.localPosition.z == -1 & o.GetComponent<SpriteRenderer>().sprite == pictogramElbow)
				{
					o.GetComponent<SpriteRenderer>().sprite = elbFail;
				}
				gameManager.GetComponent<GameControl>().enemyTurn();
			}
		}
	
	}
	/*
	void FixedUpdate () {
		currentTime += Time.deltaTime;
		MoveSpriteDown();
	}
	*/
	
	// Sets the appropriate pictogram sprites and visibility
	public void LoadSequence (int[] seq, float delay) {
		for (int i = 0; i < seq.Length; i++) {
			sequenceObjects[i].GetComponent<SpriteRenderer>().enabled = true;
			sequenceObjects[i].GetComponent<SpriteRenderer>().sprite = sequenceSprites[seq[i]];
			if (playerNum == 1)
			{			
				sequenceObjects[i].transform.localPosition = new Vector3 (sequenceObjects[i].transform.localPosition.x, sequenceObjects[i].transform.localPosition.y, - zDistanceBetweenPictograms*(i+1));
			}
			else
			{
				sequenceObjects[i].transform.localPosition = new Vector3 (sequenceObjects[i].transform.localPosition.x, sequenceObjects[i].transform.localPosition.y, - zDistanceBetweenPictograms*(i+1));
			}
		}
		
		for (int i = seq.Length; i < sequenceObjects.Length; i++) {
			sequenceObjects[i].GetComponent<SpriteRenderer>().enabled = false;
		}

	}
	public void MoveSpriteForward(){
		float zTranslation = zDistanceBetweenPictograms;
		foreach (GameObject o in sequenceObjects) {
			o.gameObject.transform.localPosition = new Vector3 (o.gameObject.transform.localPosition.x, o.gameObject.transform.localPosition.y, 
			o.gameObject.transform.localPosition.z + zTranslation);
			}
		}
	public void AfterFail(){
		float zTranslation = zDistanceBetweenPictograms;
		foreach (GameObject o in sequenceObjects)
		{
			o.gameObject.transform.localPosition = new Vector3 (o.gameObject.transform.localPosition.x, o.gameObject.transform.localPosition.y, 
			o.gameObject.transform.localPosition.z + zTranslation * 10);
		}
		gameManager.GetComponent<GameControl>().canEmit = false;

	}

}
