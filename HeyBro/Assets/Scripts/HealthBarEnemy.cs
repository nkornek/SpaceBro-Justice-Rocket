using UnityEngine;
using System.Collections;

public class HealthBarEnemy : MonoBehaviour {

	public float max_XScale;
	public float yScale;
	public EnemyControls enemy;
	
	public float curPerc;
	public float targetPerc;
	public GameObject barLeft, healthBar;

	// Use this for initialization
	void Start () {
		enemy = GameObject.Find ("Enemy").GetComponent<EnemyControls>();
		curPerc = 0.1f;
		targetPerc = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		//float percHealth = (float) enemy.hp / (float) enemy.maxHP
		//transform.localScale = new Vector3 (max_XScale * percHealth, yScale, 1f);
		targetPerc = (float) enemy.hp / (float) enemy.maxHP;
		
		if (targetPerc < 0f) {
			targetPerc = 0f;
		}
		
		if (targetPerc != curPerc) {
		
			if (Mathf.Abs (curPerc - targetPerc) <= 0.001f) {
				curPerc = targetPerc;
			}
			else {
				curPerc = Mathf.Lerp (curPerc, targetPerc, 0.03f);
			}			
			healthBar.transform.localScale = new Vector3 (max_XScale * curPerc, yScale, 1f);
			barLeft.transform.localPosition = new Vector3 ( -1.84f - (max_XScale * curPerc * 1.13333333333333333f), -0.87f, 0);
		}		
	}
}
