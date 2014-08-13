using UnityEngine;
using System.Collections;

public class Cutscene_Camera : MonoBehaviour {
	public Animator cutsceneOneMaster;
	public ParticleSystem[] cutsceneOneParticles;
	public int cutsceneNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (cutsceneOneMaster.transform.localScale.y <= 0.2)
		{
			foreach (ParticleSystem p in cutsceneOneParticles)
			{
				p.enableEmission = false;
			}
		}
		else
		{
			foreach (ParticleSystem p in cutsceneOneParticles)
			{
				p.enableEmission = true;
			}
		}
	
	}

	public void triggerScene (int whichScene) {
	switch (whichScene){
		case 1:
			cutsceneOneMaster.SetTrigger("Cutscene 1");
			break;
		}
	}
}
