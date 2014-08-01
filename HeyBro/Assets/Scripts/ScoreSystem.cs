using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	public int hiScore, score;


	void Start() {
		hiScore = PlayerPrefs.GetInt ("HighScore");
	}

	void Update() {
	
	
	}
}
