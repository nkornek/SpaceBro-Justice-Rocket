using UnityEngine;
using System.Collections;

public class tripleScript : MonoBehaviour {
	public Sprite tripleFiveLeft, tripleFistLeft, tripleElbowLeft;
	public Sprite tripleFiveSuccessLeft, tripleFistSuccessLeft, tripleElbowSuccessLeft;
	public Sprite tripleFiveFailLeft, tripleFistFailLeft, tripleElbowFailLeft;
	public SpriteRenderer triple1Left, triple2Left, triple3Left;
	public Sprite tripleFiveRight, tripleFistRight, tripleElbowRight;
	public Sprite tripleFiveSuccessRight, tripleFistSuccessRight, tripleElbowSuccessRight;
	public Sprite tripleFiveFailRight, tripleFistFailRight, tripleElbowFailRight;
	public SpriteRenderer triple1Right, triple2Right, triple3Right;

	// Use this for initialization
	void Start () {
		triple1Left.enabled = false;
		triple2Left.enabled = false;
		triple3Left.enabled = false;
		triple1Right.enabled = false;
		triple2Right.enabled = false;
		triple3Right.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateTriple (int makeTriple) {
	switch (makeTriple){
		case 1:

			break;
		}
	}
}
