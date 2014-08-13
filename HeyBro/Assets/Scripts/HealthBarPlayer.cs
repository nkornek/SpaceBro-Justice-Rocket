using UnityEngine;
using System.Collections;

public class HealthBarPlayer : MonoBehaviour {

	public float max_XScale;
	public float yScale;
	public SequenceControls player;
	
	public float curPerc;
	public float targetPerc;
	public GameObject barRight, healthBar;
	public bool CanFadeIn, fadeSwitch;
	public float alpha;
	
	public SpriteRenderer[] healthPips;
	public Sprite pipDead, pipFull;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Players").GetComponent<SequenceControls>();
		curPerc = 0f;
		targetPerc = 1f;
		CanFadeIn = false;
		alpha = 0f;
		fadeSwitch = false;
	}
	
	// Update is called once per frame
	void Update () {
		//float percHealth = (float) enemy.hp / (float) enemy.maxHP
		//transform.localScale = new Vector3 (max_XScale * percHealth, yScale, 1f);
		targetPerc = (float) player.hp / (float) player.maxHP;
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
					//Invoke ("FadeOutStart", 0.7f);
				}
			}
			else {
				if (alpha > 0.9f)
				{
					curPerc = Mathf.Lerp (curPerc, targetPerc, 0.05f);
				}
			}
			//health pips
			for (int i = 0; i < 20; i++)
			{
				if (curPerc > 0.05f * i)
				{
					healthPips[i].sprite = pipFull;
				}
				else
				{
					healthPips[i].sprite = pipDead;
				}
			}
			//healthBar.transform.localScale = new Vector3 (max_XScale * curPerc, yScale, 1f);
			//barRight.transform.localPosition = new Vector3 ( 3.88f + (max_XScale * curPerc * 1.13f), barRight.transform.localPosition.y, 0);
		}		
	}
public void FadeOutStart(){
		CanFadeIn = false;
		fadeSwitch = true;
	}
}
