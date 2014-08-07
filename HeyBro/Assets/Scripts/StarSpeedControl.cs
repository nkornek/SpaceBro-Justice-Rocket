using UnityEngine;
using System.Collections;

public class StarSpeedControl : MonoBehaviour {
	public ParticleSystem starParticles;
	public HealthBarEnemy enemyHealth;
	public float starspeed;
	public bool slowdown;

	// Use this for initialization
	void Start () {
		starspeed = 50.5f;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth) {
		starspeed = 50.5f - (50 * enemyHealth.curPerc);
		starParticles.startSpeed = starspeed;
		}
		else if (slowdown & starspeed > 0.5f)
		{
			starspeed -= 0.5f;
			starParticles.startSpeed = starspeed;
		}
	
	}
}
