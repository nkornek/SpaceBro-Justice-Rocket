﻿using UnityEngine;
using System.Collections;

public class Enemy_Sounds : MonoBehaviour {
	public AudioSource enemyAudio;
	public AudioSource rocketAudio;
	public AudioSource enemyEffects;
	public AudioClip[] grunt;
	public AudioClip[] laugh;
	public AudioClip[] roar;
	public AudioClip longRoar;
	public AudioClip[] servo;
	public AudioClip[] rocket;
	public AudioClip[] explosion;
	public AudioClip siphon;
	public AudioClip laserCharge;
	public AudioClip laserFire;
	public AudioClip wubwub;
	public AudioClip slowmo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void gruntClip () {
		enemyAudio.loop = false;
		int randomInt = Random.Range (0, 9);
		enemyAudio.clip = grunt[randomInt];
		enemyAudio.Play ();
	}

	public void laughClip () {
		enemyAudio.loop = true;
		//int randomInt = Random.Range (0, 2);
		int randomInt = 1;
		enemyAudio.clip = laugh[randomInt];
		enemyAudio.Play ();
	}

	public void ShortRoarClip () {
		enemyAudio.loop = false;
		int randomInt = Random.Range (0, 3);
		enemyAudio.clip = roar[randomInt];
		enemyAudio.Play ();
	}

	public void servoClip () {
		enemyAudio.loop = false;
		int randomInt = Random.Range (0, 5);
		enemyAudio.clip = servo[randomInt];
		enemyAudio.Play ();
	}

	public void servo2Clip () {
		enemyAudio.loop = false;
		enemyAudio.clip = servo[2];
		enemyAudio.Play ();
	}

	public void servo3Clip () {
		enemyAudio.loop = false;
		enemyAudio.clip = servo[3];
		enemyAudio.Play ();
	}

	public void LongRoarClip () {
		enemyAudio.loop = false;
		enemyAudio.clip = longRoar;
		enemyAudio.Play ();
	}

	public void RocketAudio () {
		int randomInt = Random.Range (0, 2);
		rocketAudio.clip = rocket[randomInt];
		rocketAudio.Play ();
	}

	public void explosionAudio () {
		enemyEffects.loop = false;
		int randomInt = Random.Range (0, 2);
		enemyEffects.clip = explosion[randomInt];
		enemyEffects.Play ();
	}

	public void siphonAudio () {
		enemyEffects.loop = false;
		enemyEffects.clip = siphon;
		enemyEffects.Play ();
	}

	public void laserChargeAudio () {
		enemyEffects.loop = false;
		enemyEffects.clip = laserCharge;
		enemyEffects.Play ();
	}

	public void laserFireAudio () {
		enemyEffects.loop = false;
		enemyEffects.clip = laserFire;
		enemyEffects.Play ();
	}

	public void wubwubAudio () {
		enemyEffects.loop = true;
		enemyEffects.clip = wubwub;
		enemyEffects.Play ();
	}

	public void slowmoAudio () {
		enemyEffects.loop = false;
		enemyEffects.clip = slowmo;
		enemyEffects.Play ();
	}

	public void stopEnemyVoiceAudio () {
		enemyAudio.Stop ();
	}

	public void stopEnemyRocketAudio () {
		rocketAudio.Stop ();
	}

	public void stopEnemyEffectAudio () {
		enemyEffects.Stop ();
	}

}