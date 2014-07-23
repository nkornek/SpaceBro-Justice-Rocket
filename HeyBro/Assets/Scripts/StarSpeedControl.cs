using UnityEngine;
using System.Collections;

public class StarSpeedControl : MonoBehaviour {
	public ParticleSystem starParticles;
	public HealthBarEnemy enemyHealth;
	public float starspeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		starspeed = 50.5f - (50 * enemyHealth.curPerc);
		starParticles.startSpeed = starspeed;
	
	}
}
