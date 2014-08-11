using UnityEngine;
using System.Collections;

public class menu_anim_triggers : MonoBehaviour {
	public Animator infoPrompts;
	public MenuControl menu;
	public GameObject Talker;

	// Use this for initialization
	void Start () {
		foreach (SpriteRenderer s in Talker.GetComponentsInChildren<SpriteRenderer>())
		{
			s.enabled = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pulseChange () {
		infoPrompts.SetTrigger ("pulsechange");
	}

	public void canMenu()
	{
		menu.canMenu = true;
	}
	public void mainMenu()
	{
		menu.mainMenu = true;
	}

	public void talkerIn () {
		foreach (SpriteRenderer s in Talker.GetComponentsInChildren<SpriteRenderer>())
		{
			s.enabled = true;
		}
		Talker.GetComponent<Animator> ().SetTrigger ("In");

	}
	public void talkerOut () {
		Talker.GetComponent<Animator> ().SetTrigger ("Out");
		Invoke ("talkerDisable", 0.25f);
	}

	public void talkerDisable () {
		foreach (SpriteRenderer s in Talker.GetComponentsInChildren<SpriteRenderer>())
		{
			s.enabled = false;
		}
	}
}
