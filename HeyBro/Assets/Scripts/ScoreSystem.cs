using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	public int hiScore, score;

	public Material defaultMaterial; //prefab material set already

	void Awake() {
		hiScore = PlayerPrefs.GetInt ("HighScore");
		System.IO.Directory.CreateDirectory (Application.dataPath + "/Snaps/");
	}

	IEnumerator Start()
	{
		string path = Application.dataPath + "/Snaps/.highscore.png";
		WWW www = new WWW(path);
		yield return www; 
		
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		Sprite sprite = new Sprite();
		sprite = Sprite.Create(www.texture, new Rect(0, 0, 170, 170),new Vector2(0, 0),100.0f);
		
		renderer.sprite = sprite;
		renderer.material = defaultMaterial;
	}



	void Update() {

	
	
	}
}
