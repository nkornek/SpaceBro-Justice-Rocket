using UnityEngine;
using System.Collections;

public class menu_anim_triggers : MonoBehaviour {
	public Animator infoPrompts;
	public MenuControl menu;

	// Use this for initialization
	void Start () {
	
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
}
