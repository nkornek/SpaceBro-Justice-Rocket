using UnityEngine;
using System.Collections;

public class menu_anim_triggers : MonoBehaviour {
	public Animator infoPrompts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pulseChange () {
		infoPrompts.SetTrigger ("pulsechange");
	}
}
