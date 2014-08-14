using UnityEngine;
using System.Collections;

public class EnemyControls : MonoBehaviour {

	public int hp;
	public int maxHP;


	void Start () {
		maxHP = 500;
		hp = maxHP;
	}
	
	void FixedUpdate () {
		
	}

	public void DamageEnemy(int damage){
		hp -= damage; 
	}
}
