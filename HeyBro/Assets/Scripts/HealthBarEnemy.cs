using UnityEngine;
using System.Collections;

public class HealthBarEnemy : MonoBehaviour {

	public float max_XScale;
	public float yScale;
	public EnemyControls enemy;
	
	public float curPerc;
	public float targetPerc;
	public GameObject barLeft; //healthBar;
	public bool CanFadeIn, fadeSwitch;
	public float alpha;

	public SpriteRenderer[] healthPips;
	public Sprite pipDead, pipRed, pipOrange;
	public ParticleSystem[] damageParticles;
	public bool canEmit;

	// Use this for initialization
	void Start () {
		enemy = GameObject.Find ("Enemy").GetComponent<EnemyControls>();
		curPerc = 0f;
		targetPerc = 1f;
		CanFadeIn = false;
		alpha = 0f;
		fadeSwitch = true;
		canEmit = true;
		foreach (ParticleSystem p in damageParticles)
		{
			p.enableEmission = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//float percHealth = (float) enemy.hp / (float) enemy.maxHP
		//transform.localScale = new Vector3 (max_XScale * percHealth, yScale, 1f);
		targetPerc = (float) enemy.hp / (float) enemy.maxHP;
		if (CanFadeIn & alpha < 1)
		{
			alpha += 0.05f;
		}
		else if (!CanFadeIn & alpha > 0)
		{
			alpha -= 0.05f;
		}

		foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
		{
			r.color = new Color (1.0f, 1.0f, 1.0f, alpha);
		}

		if (targetPerc < 0f) {
			targetPerc = 0f;
		}
		
		if (targetPerc != curPerc) {
		
			if (Mathf.Abs (curPerc - targetPerc) <= 0.001f) {
				curPerc = targetPerc;
				if (fadeSwitch & CanFadeIn)
				{
					fadeSwitch = false;
					//Invoke ("FadeOutStart", 1f);
				}
			}
			else {
				if (alpha > 0.9f)
				{
					curPerc = Mathf.Lerp (curPerc, targetPerc, 0.05f);
				}
			}
			//health pips
			for (int i = 0; i < 25; i++)
			{
				if (curPerc > (0.02f * i) + 0.5)
				{
					healthPips[i].sprite = pipOrange;
				}
				else if (curPerc > 0.02f * i)
				{
					healthPips[i].sprite = pipRed;
				}
				else
				{
					healthPips[i].sprite = pipDead;
				}
			}
			//damage particles
			if (curPerc > targetPerc & canEmit)
			{
				canEmit = false;
			    foreach (ParticleSystem p in damageParticles)
			    {
					p.Emit(4);
				}
			}
			else if (curPerc <= targetPerc)
			{
				canEmit = true;
			}
		}

	}
public void FadeOutStart(){
		CanFadeIn = false;
		fadeSwitch = true;
	}
}
