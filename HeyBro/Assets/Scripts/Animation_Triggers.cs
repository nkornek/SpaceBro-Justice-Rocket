using UnityEngine;
using System.Collections;

public class Animation_Triggers : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndIntro(){
		GameObject.Find ("Game").GetComponent<GameControl> ().Invoke("GameStart", 1);
	}
}
