using UnityEngine;
using System.Collections;
using System.IO.Ports; 

public class MenuControl : MonoBehaviour {
	//arduino stuff
	// TRUE IF USING KEYBOARD, FALSE IF ARDUINO
	public bool keyControl; 
	
	// GETTING FROM ARDUINO
	public ArduinoRead arduino; 
	
	// ARDUINO STUFF ("PORT" is not right)
	public SerialPort sp = new SerialPort("COM3", 9600);
	public byte[] byteBuffer; 
	public int byteOffset;
	public int byteCount; 
	public int current;

	// TO CREATE THE EVENTS THAT WILL BE CHECKED
	public enum touch { palm, fist, elbow }; 
	public int detectedA;
	public int detectedB; 
	public int minEnum = 0; 	// first index of the enum
	public int maxEnum = 3;	// number of elements in the enum	
	
	// CONTACT INPUTS (person A and person B)
	public bool palmA; 		// these will correspond to specific button inputs 
	public bool fistA;
	public bool elbowA;
	
	public bool palmB;
	public bool fistB;
	public bool elbowB;
	
	// TO CHECK THAT THE RIGHT CONTACT WAS MADE
	public bool touchDetectedA;
	public bool touchDetectedB; 

	public bool canMenu, mainMenu;
	//public bool hasResetInput;
	public bool canSpeedUp;
	public ParticleSystem starParticles;

	public Animator cameraMovement, whichMenu, playerLeft, playerRight;
	public Sound_Triggers_Players sounds;
	public AudioSource menuMusic;

	//check moves
	bool checkfivesA, checkfivesB, checkfistA, checkfistB, checkelbowA, checkelbowB;



	// Use this for initialization
	void Start () {
		touchDetectedA = false; 
		touchDetectedB = false;
		canMenu = true;
		mainMenu = true;
		menuMusic = GameObject.Find ("MenuMusic").GetComponent<AudioSource> ();

	
	}
	
	// Update is called once per frame
	void Update () {
		detectedA = arduino.in1; 
		detectedB = arduino.in2;
		/*
		if (!hasResetInput)
		{
			detectedA = -1;
			detectedB = -2;
			hasResetInput = true;
		}
		else
		{
			hasResetInput = false;
			*/
			if (canMenu) {
				if (checkFives())
				{
					playerLeft.SetTrigger("five");
					playerRight.SetTrigger("five");
					menuSelect(0);
					canMenu = false;
					mainMenu = false;
				}
			/*
				else if (checkFist())
				{
					playerLeft.SetTrigger("punch");
					playerRight.SetTrigger("punch");
					canMenu = false;
					mainMenu = false;
					menuSelect(1);
				}
				*/
				else if (checkElbow())
				{
					playerLeft.SetTrigger("elbow");
					playerRight.SetTrigger("elbow");
					canMenu = false;
					mainMenu = false;
					menuSelect(1);
				}
			//}

		}
		if (canSpeedUp & starParticles.startSpeed < 50.5)
		{
			starParticles.startSpeed = Mathf.Lerp (starParticles.startSpeed, 50.5f, 1);
		}
		if (canSpeedUp & menuMusic.volume > 0)
		{
			menuMusic.volume -= 0.002f;
		}


	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * ARG: the demanded contact input for player A
	 * (1) touchDetectedA bool set to TRUE;
	 * (2) if the player did a move within the window of time 
	 * (3) if hit the right contact then return true. 
	 * (4) Otherwise return false
	 * -------------------------------------------------------------------------------------------------------------------------- */
	
	public bool checkTouchA(int touchA){
		
		if(!keyControl){
			palmA 	= (detectedA == 1);
			fistA 	= (detectedA == 2);
			elbowA 	= (detectedA == 3);
		}
		
		else {
			palmA 	= Input.GetKey(KeyCode.Alpha3); 		// these will correspond to specific button inputs 
			fistA 	= Input.GetKey(KeyCode.Alpha2);
			elbowA	= Input.GetKey(KeyCode.Alpha1);
		}
		// (1) touch detected from player A
		touchDetectedA = true;
		
		// (2) check that hit within window
		//		if (currentSeqTime < seqWindow){
		// (3) if right input, return true
		switch (touchA){ 
		case (int) touch.palm:
			if (palmA){
				return true; 
			}
			break;
			
		case (int) touch.fist:
			if (fistA){
				return true; 
			}
			break;
			
		case (int) touch.elbow:
			if (elbowA){
				return true; 
			}
			break;
			
		default:
			break;
			
		}
		//		}
		// (4) if haven't returned true = wrong input (CHECK ANY KEY?)
		return false; 
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * ARG: the demanded contact input for player B
	 * (1) touchDetectedB bool set to TRUE;
	 * (2) if the player did a move within the window of time 
	 * (3) if hit the right contact then return true. 
	 * (4) Otherwise return false
	 * -------------------------------------------------------------------------------------------------------------------------- */
	
	public bool checkTouchB(int touchB){
		
		if(!keyControl){
			palmB 	= (detectedB == 4);
			fistB 	= (detectedB == 5);
			elbowB 	= (detectedB == 6);
		}
		
		else {
			palmB	= Input.GetKey(KeyCode.Alpha8); 
			fistB	= Input.GetKey(KeyCode.Alpha9);
			elbowB	= Input.GetKey(KeyCode.Alpha0);
		}
		
		// (1) touch detected from player B
		touchDetectedB = true; 
		
		// (2) if the players hit within the window of time 
		//if (currentSeqTime < seqWindow){
		// (3) if right input, return true
		switch (touchB){
		case (int) touch.palm:
			if (palmB){
				return true; 
			}
			break;
			
		case (int) touch.fist:
			if (fistB){
				return true; 
			}
			break;
			
		case (int) touch.elbow:
			if (elbowB){
				return true; 
			}
			break;
			
		default:
			break;
			
		}
		//}
		// (3) if haven't returned true = wrong input (CHECK ANY KEY?)
		return false; 
	}

	bool checkFives () {
		checkfivesA = checkTouchA (0);
		checkfivesB = checkTouchB (0);
		if (checkfivesA && checkfivesB)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	bool checkFist () {
		checkfistA = checkTouchA (1);
		checkfistB = checkTouchB (1);
		if (checkfistA && checkfistB)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	bool checkElbow () {
		checkelbowA = checkTouchA (2);
		checkelbowB = checkTouchB (2);
		if (checkelbowA && checkelbowB)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void menuSelect (int i) {
		switch (i) {
		case 0:
			if (mainMenu)
			{
				cameraMovement.SetTrigger("toinfo");
				Invoke ("loadLevel", 3f);
				canSpeedUp = true;
				sounds.ClipLetsDoThis();
			}
			else
			{
				cameraMovement.SetTrigger("return");
				whichMenu.SetTrigger("return");
				sounds.ClipCelebrate();
			}
			break;
		case 1:
			cameraMovement.SetTrigger("toinfo");
			whichMenu.SetTrigger("howto");
			sounds.ClipGatorShort();
			break;
		case 2:
			cameraMovement.SetTrigger("toinfo");
			whichMenu.SetTrigger("scores");
			break;
		}

	}

	void loadLevel ()
	{
		Application.LoadLevel (2);
	}
}
