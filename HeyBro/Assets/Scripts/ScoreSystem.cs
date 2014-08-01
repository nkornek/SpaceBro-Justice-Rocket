using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	public int hiScore, score;
	public SpriteRenderer highscore;

	void Awake() {
		hiScore = PlayerPrefs.GetInt ("HighScore");
		System.IO.Directory.CreateDirectory (Application.dataPath + "/Snaps/");

	}

	void Update() {
		highscore.sprite = Resources.LoadAssetAtPath<Sprite> (Application.dataPath + "/Resources/highscore.png");
		}

}
