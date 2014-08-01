using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	public int hiScore, score;


	void Awake() {
		hiScore = PlayerPrefs.GetInt ("HighScore");
		System.IO.Directory.CreateDirectory (Application.dataPath + "/Snaps/");
		var url = "Application.dataPath"+"/Snaps/highscore.png";
	}

	void Update() {

	
	
	}
}
