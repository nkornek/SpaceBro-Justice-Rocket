using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	public Cutscene_Camera cutscene;
	public GameControl game;
	public Animator characterAnims;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetAnim (int anim) {
		switch (anim) {
		case -1: 
			break;
		case 0: 
			characterAnims.SetTrigger("five");
			break;
		case 1:
			characterAnims.SetTrigger("punch");
			break;
		case 2:
			characterAnims.SetTrigger("elbow");		
			break;
		case 3:
			cutscene.triggerScene(1);
			break;
		case 4: 
			break;
		}
	}
}
