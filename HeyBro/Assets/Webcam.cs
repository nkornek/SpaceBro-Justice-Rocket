using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour {
	WebCamTexture _CamTex;
	private string _SavePath = Application.dataPath + "/Snaps/";
	int _CaptureCounter = 0;
	public Material defaultMaterial; //prefab material set already

	void Start() {
		_CamTex = new WebCamTexture();
		_CamTex.Play ();
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.A))
		{
			TakeSnapshot();
		}
	
	}

	void TakeSnapshot()
	{
		Texture2D snap = new Texture2D(_CamTex.width, _CamTex.height);
		snap.SetPixels(_CamTex.GetPixels());
		snap.Apply();
		
		System.IO.File.WriteAllBytes(_SavePath + "highscore.png", snap.EncodeToPNG());
		++_CaptureCounter;
	}
	
}
