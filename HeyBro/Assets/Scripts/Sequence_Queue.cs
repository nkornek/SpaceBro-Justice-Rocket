using UnityEngine;
using System.Collections;

public class Sequence_Queue : MonoBehaviour {

	public GameObject[] sequenceObjects;
	private Texture[] sequenceSprites;
	
	public Texture pictogramHighfive, pictogramFist, pictogramElbow;

	// delay stuff
	public SequenceControls player; 
	public float seqDelay;
	public float currentTime;
	public float zDistanceBetweenPictograms;
	
	public bool movingSpritesForward;
	
	// Use this for initialization
	void Start () {
		sequenceSprites = new Texture[3] {pictogramHighfive, pictogramFist, pictogramElbow};
		zDistanceBetweenPictograms = 0.1f;
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
			sequenceObjects[i].renderer.enabled = true;
			sequenceObjects[i].transform.localPosition = new Vector3 (0, 0, - zDistanceBetweenPictograms*(i+1));
			sequenceObjects[i].guiTexture.texture = sequenceSprites[seq[i]];
		}
		
		for (int i = seq.Length; i < sequenceObjects.Length; i++) {
			sequenceObjects[i].renderer.enabled = false;
		}

	}
	public void MoveSpriteForward(){
		float zTranslation = zDistanceBetweenPictograms;
		
		if (movingSpritesForward){
				foreach (GameObject o in sequenceObjects) {
					o.gameObject.transform.position = new Vector3 (o.gameObject.transform.position.x, o.gameObject.transform.position.y, 
					o.gameObject.transform.position.z - zTranslation);
				}			
			}
		}
}
