﻿using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour {

	WebCamTexture _CamTex;
	private string _SavePath = "C:/WebcamSnaps/";
	int _CaptureCounter = 0;

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
		
		System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
		++_CaptureCounter;
	}
	
}
