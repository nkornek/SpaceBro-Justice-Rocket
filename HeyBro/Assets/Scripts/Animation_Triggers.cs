using UnityEngine;
using System.Collections;

public class Animation_Triggers : MonoBehaviour {
	public GameControl game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndIntro(){
		game.GameStart ();
	}
}
