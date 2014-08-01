using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour {

	public WebCamTexture webcamTexture;
	public Color32[] data;
	void Start() {
		webcamTexture = new WebCamTexture();
		webcamTexture.Play();
		data = new Color32[webcamTexture.width * webcamTexture.height];
	}
	void Update() {
		webcamTexture.GetPixels32(data);
	}

}
