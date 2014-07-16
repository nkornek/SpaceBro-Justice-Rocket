using UnityEngine;
using System.Collections;

public class scrollingmaterial : MonoBehaviour {
	
	public float Speed = 0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		renderer.material.mainTextureOffset = new Vector2 ((Time.time * Speed)%1, 0f);
	}
}
