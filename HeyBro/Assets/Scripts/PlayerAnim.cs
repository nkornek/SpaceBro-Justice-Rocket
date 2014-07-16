using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

	public Sprite attack;
	public Sprite elbow;	
	public Sprite handsUp;
	public Sprite highfive;		
	public Sprite punch;	
	public Sprite sad;

	public Cutscene_Camera cutscene;
	public GameControl game;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().sprite = handsUp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetSprite (int sprite) {
		switch (sprite) {
		case -1: 
			gameObject.GetComponent<SpriteRenderer>().sprite = handsUp;
			break;
		case 0: 
			gameObject.GetComponent<SpriteRenderer>().sprite = highfive;
			break;
		case 1:
			gameObject.GetComponent<SpriteRenderer>().sprite = punch;
			break;
		case 2:
			gameObject.GetComponent<SpriteRenderer>().sprite = elbow;
			break;
		case 3:
			gameObject.GetComponent<SpriteRenderer>().sprite = attack;
			cutscene.triggerScene(1);
			break;
		case 4: 
			gameObject.GetComponent<SpriteRenderer>().sprite = handsUp;
			break;
		}
	}
}
