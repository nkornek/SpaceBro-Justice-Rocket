using UnityEngine;
using System.Collections;

public class Enemy_Particles : MonoBehaviour {

	public bool chargeVisible;
	public ParticleSystem laser;
	public ParticleSystem laser2;
	public GameObject chargeParticles;
	public AudioClip laserSound, laserCharge;
	public AudioSource attackAudio;
	public float attackTime;
	public SphereCollider forcefield;
	public float rotation;
	public Animator enemyAnimations;
	public ParticleSystem siphonParticles;

	// Use this for initialization
	void Start () {
		chargeVisible = false;
		laser.enableEmission = false;
		laser2.enableEmission = false;
		attackTime = 3.0f;
		siphonParticles.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		rotation += 1.5f;
		chargeParticles.transform.localRotation = Quaternion.Euler (333, 1.2f, rotation);
		if (chargeVisible)
		{
			chargeParticles.GetComponent<ParticleSystem>().Play();
			chargeParticles.GetComponent<ParticleSystem>().enableEmission = true;
			if (attackAudio.isPlaying == false)
			{
				attackAudio.clip = laserCharge;
				attackAudio.Play ();
				attackAudio.loop = true;
			}			
		}
		else
		{
			chargeParticles.GetComponent<ParticleSystem>().enableEmission = false;
		}
	
	}

	void FireLasers () {
		attackAudio.loop = false;
		attackAudio.Stop ();
		laser.Play ();
		laser2.Play ();
		laser.enableEmission = true;
		laser2.enableEmission = true;
		if (attackAudio.isPlaying == false) 
		{
			attackAudio.clip = laserSound;
			attackAudio.Play ();
		}
		enemyAnimations.SetTrigger("Fire Lasers");
		if (GameObject.Find ("Players").GetComponent<SequenceControls> ().blocked) 
		{
			forcefield.enabled = true;
		} 
		else 
		{
			forcefield.enabled = false;
		}
	}

	public void showCharge () {
		chargeVisible = true;
	}
	public void endCharge () {
		chargeVisible = false;
		}

	void EndLasers () {
		laser.enableEmission = false;
		laser2.enableEmission = false;
		Invoke ("EndField", 2);
		enemyAnimations.SetTrigger("LaserEnd");
	}
	void EndField () {	
		GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = false;
	}

	public void startSiphonParticles () {
		siphonParticles.enableEmission = true;
	}

	public void endSiphonParticles () {
		siphonParticles.enableEmission = false;
	}
}
