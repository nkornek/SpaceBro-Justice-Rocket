using UnityEngine;
using System.Collections;

public class LoadingMessage : MonoBehaviour {


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if (level >= 0)
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

}
