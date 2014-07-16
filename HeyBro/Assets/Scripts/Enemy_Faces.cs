using UnityEngine;
using System.Collections;

public class Enemy_Faces : MonoBehaviour {
	public Sprite normal, angry, happy;
	public Sprite laserAttack, laserCharge;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		}

	public void SetSprite (int sprite) {
		switch (sprite) {
		case 0: 
			gameObject.GetComponent<SpriteRenderer>().sprite = normal;
			break;
		case 1:
			gameObject.GetComponent<SpriteRenderer>().sprite = angry;
			break;
		case 2:
			gameObject.GetComponent<SpriteRenderer>().sprite = happy;
			Invoke ("ResetFace", 2.0f);
			break;
		case 3:
			gameObject.GetComponent<SpriteRenderer>().sprite = laserCharge;
			break;
		case 4:
			gameObject.GetComponent<SpriteRenderer>().sprite = laserAttack;
			break;
		}
	}
	
	public void ResetFace(){
		gameObject.GetComponent<SpriteRenderer>().sprite = normal;
		}
		
}
