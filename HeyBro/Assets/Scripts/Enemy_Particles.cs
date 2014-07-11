﻿using UnityEngine;
using System.Collections;

public class Enemy_Particles : MonoBehaviour {

	public bool chargeVisible;
	public ParticleSystem laser;
	public ParticleSystem laser2;
	public GameObject chargeParticles;
	public AudioClip laserSound;
	public AudioSource attackAudio;
	public float attackTime;
	public BoxCollider forcefield;
	public float rotation;
	// Use this for initialization
	void Start () {
		chargeVisible = false;
		laser.enableEmission = false;
		laser2.enableEmission = false;
		attackTime = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (chargeVisible)
		{
			GetComponentInParent<SpriteRenderer> ().sprite = GetComponentInParent<Enemy_Faces> ().laserCharge;
			chargeParticles.GetComponent<ParticleSystem>().enableEmission = true;
			rotation += 1.5f;
			chargeParticles.transform.localRotation = Quaternion.Euler (0, 0, rotation);
		}
		else
		{
			chargeParticles.GetComponent<ParticleSystem>().enableEmission = false;
		}
	
	}

	void FireLasers () {
		GetComponentInParent<SpriteRenderer> ().sprite = GetComponentInParent<Enemy_Faces> ().laserAttack;
		laser.enableEmission = true;
		laser2.enableEmission = true;
		Invoke ("EndLasers", attackTime);
		GameObject.Find ("Game").GetComponent<GameControl> ().Invoke ("LaserDamage", attackTime / 3);
		if (attackAudio.isPlaying == false) 
		{
			attackAudio.clip = laserSound;
			attackAudio.Play ();
		}
		GameObject.Find ("Enemy_Face").GetComponent<Enemy_Faces> ().SetSprite (4);
		if (GameObject.Find ("Players").GetComponent<SequenceControls> ().blocked) 
		{
			forcefield.enabled = true;
		} 
		else 
		{
			forcefield.enabled = false;
		}
	}
	void EndLasers () {
		laser.enableEmission = false;
		laser2.enableEmission = false;
		GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = false;
		GameObject.Find ("Enemy_Face").GetComponent<Enemy_Faces>().SetSprite (0);
	}
}
