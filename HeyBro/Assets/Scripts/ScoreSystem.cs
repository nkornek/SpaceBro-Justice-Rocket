using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	public int hiScore, score;
	public Texture2D texTmp;
	public Material mat;

	void Awake() {
		hiScore = PlayerPrefs.GetInt ("HighScore");
		System.IO.Directory.CreateDirectory (Application.dataPath + "/Snaps/");
	}

	void Update() {

	}

}
