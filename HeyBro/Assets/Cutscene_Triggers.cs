using UnityEngine;
using System.Collections;

public class Cutscene_Triggers : MonoBehaviour {
	public Particle_Deactivate playerParticles;	
	public GameControl game;
	public Animator enemyAnimations;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FireStars(){
		playerParticles.partVisible = true;
		enemyAnimations.SetTrigger("Hurt");
		}
}
