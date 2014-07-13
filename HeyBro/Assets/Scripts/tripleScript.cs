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
		triple1Left.enabled = true;
		triple2Left.enabled = true;
		triple3Left.enabled = true;
		triple1Right.enabled = true;
		triple2Right.enabled = true;
		triple3Right.enabled = true;
	switch (makeTriple){
		case 0:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFiveLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFiveRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 1:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFistRight;
			triple2Right.sprite = tripleFiveRight;
			triple3Right.sprite = tripleFistRight;
			break;
		case 2:			
			triple1Left.sprite = tripleFistLeft;
			triple2Left.sprite = tripleFiveLeft;
			triple3Left.sprite = tripleFistLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 3:			
			triple1Left.sprite = tripleFistLeft;
			triple2Left.sprite = tripleElbowLeft;
			triple3Left.sprite = tripleFistLeft;
			triple1Right.sprite = tripleFistRight;
			triple2Right.sprite = tripleElbowRight;
			triple3Right.sprite = tripleFistRight;
			break;
		case 4:			
			triple1Left.sprite = tripleFiveLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleElbowLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleElbowRight;
			break;
		case 5:			
			triple1Left.sprite = tripleElbowLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleElbowRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleFiveRight;
			break;
		case 6:			
			triple1Left.sprite = tripleElbowLeft;
			triple2Left.sprite = tripleFistLeft;
			triple3Left.sprite = tripleFiveLeft;
			triple1Right.sprite = tripleFiveRight;
			triple2Right.sprite = tripleFistRight;
			triple3Right.sprite = tripleElbowRight;
			break;
		}
	}
}
