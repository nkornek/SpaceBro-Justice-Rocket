using UnityEngine;
using System.Collections;

public class Sequence_Queue : MonoBehaviour {

	public GameObject[] sequenceObjects;
	private Texture[] sequenceSprites;
	
	public Texture pictogramHighfive, pictogramFist, pictogramElbow;
	public int playerNum;

	// delay stuff
	public SequenceControls player; 
	public float seqDelay;
	public float currentTime;
	public int zDistanceBetweenPictograms;
	
	public bool movingSpritesForward;
	
	// Use this for initialization
	void Start () {
		sequenceSprites = new Texture[3] {pictogramHighfive, pictogramFist, pictogramElbow};
		zDistanceBetweenPictograms = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
