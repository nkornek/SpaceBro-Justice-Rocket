using UnityEngine;
using System.Collections;

public class Sequence_Queue : MonoBehaviour {

	public GameObject[] sequenceObjects;
	public GameObject gameManager;
	private Texture[] sequenceSprites;
	
	public Texture pictogramHighfive, pictogramFist, pictogramElbow;
	public Texture highSuccess, fistSuccess, elbSuccess;
	public Texture highFail, fistFail, elbFail;
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
		sequenceSprites = new Texture[3] {pictogramHighfive, pictogramFist, pictogramElbow};
		zDistanceBetweenPictograms = 1;
		timeBetweenMoves = 0.2f;

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject o in sequenceObjects)
		{
			if (o.transform.localPosition.z == -1)
			{
				o.gameObject.guiTexture.enabled = true;
			}
			else
			{
				o.gameObject.guiTexture.enabled = false;
			}
		}
		if (movesCorrect)
		{
			movesCorrect = false;
			foreach (GameObject o in sequenceObjects)
			{
				if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramHighfive)
				{
					o.guiTexture.texture = highSuccess;
				}
				else if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramFist)
				{
					o.guiTexture.texture = fistSuccess;
				}
				else if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramElbow)
				{
					o.guiTexture.texture = elbSuccess;
				}
			}
		}
		if (movesFail)
		{
			movesFail = false;
			foreach (GameObject o in sequenceObjects)
			{
				if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramHighfive)
				{
					o.guiTexture.texture = highFail;
				}
				if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramFist)
				{
					o.guiTexture.texture = fistFail;
				}
				if (o.transform.localPosition.z == -1 & o.guiTexture.texture == pictogramElbow)
				{
					o.guiTexture.texture = elbFail;
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
			sequenceObjects[i].guiTexture.enabled = true;
			sequenceObjects[i].guiTexture.texture = sequenceSprites[seq[i]];
			if (playerNum == 1)
			{			
				sequenceObjects[i].transform.localPosition = new Vector3 (0.15f, 0.72f, - zDistanceBetweenPictograms*(i+1));
			}
			else
			{
				sequenceObjects[i].transform.localPosition = new Vector3 (0.35f, 0.72f, - zDistanceBetweenPictograms*(i+1));
			}
		}
		
		for (int i = seq.Length; i < sequenceObjects.Length; i++) {
			sequenceObjects[i].guiTexture.enabled = false;
		}

	}
	public void MoveSpriteForward(){
		float zTranslation = zDistanceBetweenPictograms;
		foreach (GameObject o in sequenceObjects) {
			o.gameObject.transform.position = new Vector3 (o.gameObject.transform.position.x, o.gameObject.transform.position.y, 
			o.gameObject.transform.position.z + zTranslation);
			}
		}
	public void AfterFail(){
		float zTranslation = zDistanceBetweenPictograms;
		foreach (GameObject o in sequenceObjects)
		{
			if (o.transform.localPosition.z == -1)
			{
				o.gameObject.transform.position = new Vector3 (o.gameObject.transform.position.x, o.gameObject.transform.position.y, 
				                                               o.gameObject.transform.position.z + zTranslation);
			}
		}

	}

}
