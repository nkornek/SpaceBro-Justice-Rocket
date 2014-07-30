using UnityEngine;
using System.Collections;

public class Particle_Deactivate : MonoBehaviour {

	public float attackTime = 2.0f;
	public ParticleSystem particle;
	public ParticleSystem particle2;
		
	public GameControl game;
	
	// Use this for initialization
	void Start () {
		particle.enableEmission = false;
		particle2.enableEmission = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void partVisible() {
		particle.Play();
		particle2.Play();
		particle.enableEmission = true;
		particle2.enableEmission = true;
		Invoke ("endParticles", attackTime);
	}

	public void endParticles() {
		particle.enableEmission = false;
		particle2.enableEmission = false;
		attackTime = 2.0f;	
		game.paused = false;
	}
	
	
}
