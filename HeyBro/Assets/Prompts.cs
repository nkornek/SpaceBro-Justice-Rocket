using UnityEngine;
using System.Collections;

public class Prompts : MonoBehaviour {
	public Sprite[] promptSprite;
	public AudioClip[] promptAudio;
	public AudioClip[] moveAudio;
	public SequenceControls player;
	public int moveNum;
	public AudioClip[] yeah;
	public AudioClip[] aww;
	public HealthBarPlayer playerhp;
	public HealthBarEnemy enemyhp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showPrompt (int sprite) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		gameObject.GetComponent<SpriteRenderer> ().sprite = promptSprite [sprite];
		gameObject.GetComponentInChildren<ParticleSystem> ().Emit (100);
		gameObject.GetComponent<AudioSource> ().clip = promptAudio [sprite];
		gameObject.GetComponent<AudioSource> ().Play();
		if (sprite < 5)
		{
			Invoke ("hidePrompt", 1);
		}
		if (sprite == 0)
		{
			playerhp.CanFadeIn = true;
			enemyhp.CanFadeIn = true;
		}
	}

	public void hidePrompt () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void moveCallout () {
		//determine which move to shout
		if (player.contactA == 0 & player.contactB == 0)
		{
			moveNum = 0;
		}
		else if (player.contactA == 0 & player.contactB == 1)
		{
			moveNum = 1;
		}
		else if (player.contactA == 0 & player.contactB == 2)
		{
			moveNum = 2;
		}
		else if (player.contactA == 1 & player.contactB == 1)
		{
			moveNum = 3;
		}
		else if (player.contactA == 1 & player.contactB == 0)
		{
			moveNum = 4;
		}
		else if (player.contactA == 1 & player.contactB == 2)
		{
			moveNum = 5;
		}
		else if (player.contactA == 2 & player.contactB == 2)
		{
			moveNum = 6;
		}
		else if (player.contactA == 2 & player.contactB == 0)
		{
			moveNum = 7;
		}
		else if (player.contactA == 2 & player.contactB == 1)
		{
			moveNum = 8;
		}
		gameObject.GetComponent<AudioSource> ().clip = moveAudio[moveNum];
		gameObject.GetComponent<AudioSource> ().Play();
	}

	public void sequenceSuccess () {
		int randomInt = Random.Range (0,6);
		gameObject.GetComponent<AudioSource> ().clip = yeah[randomInt];
		gameObject.GetComponent<AudioSource> ().Play();
	}
	public void sequenceFail () {
		int randomInt = Random.Range (0,5);
		gameObject.GetComponent<AudioSource> ().clip = aww[randomInt];
		gameObject.GetComponent<AudioSource> ().Play();
	}

	public void win () {
		showPrompt (5);
	}
	public void lose () {
		showPrompt (6);
	}
}
