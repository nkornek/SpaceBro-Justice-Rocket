using UnityEngine;
using System.Collections;
using System.IO.Ports; 

// NB: STILL HAVEN'T IMPLEMENTED THE DELAY BETWEEN MOVES IN SEQUENCE

public class SequenceControls : MonoBehaviour {

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
	public int contactA;
	public int contactB; 
	public int detectedA;
	public int detectedB; 
	public int minEnum = 0; 	// first index of the enum
	public int maxEnum = 3;	// number of elements in the enum	
	public bool correctA, correctB;
	public int currentSeq;

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

	public bool hi5; 				// begin and end a battle with a hi5
	public bool seqGenerated; 		// true if a sequence has been generated but not completed 

	// TO KEEP TRACK OF CURRENT MOVE
	public int currentMove, seqMoves; 		// the move we're at in the current sequence, used as index for contactA/contactB arrays to get the move we want 
	public int correctMoves; 		// number of correctly done moves in the current sequence
	public int counterInputA, counterInputB;
	public int minSeqLength, maxSeqLength;
	
	// PLAYER STUFF
	public int hp;
	public int maxHP;		
	public bool defending; 
	public bool blocked; //lol

	public GameControl game;
	public PlayerAnimations playerLeft, playerRight;
	public Prompts prompts;

	// ENEMY STUFF
	public EnemyControls enemy; 


	void Start(){
		minSeqLength = 3;
		maxSeqLength = 7;
		touchDetectedA = false; 
		touchDetectedB = false;

		hi5 = false; 

		maxHP = 100;
		hp = maxHP;
	}

	void Update(){
		detectedA = arduino.in1; 
		detectedB = arduino.in2;
	}




	/*-----------------------------------
	 * 
	 * reset sequence
	 * set new length
	 * set contact A and B through prev function
	 * 
	 ----------------------------*/
	
	public void generateSequence(){
		defending = false;
		currentMove = 0; 
		correctMoves = 0;
		seqMoves = Random.Range (minSeqLength, maxSeqLength);
		generateMove();
	}
	
	public void generateBlockSequence () {
		defending = true;
		blocked = false;
		playerLeft.characterAnims.SetBool ("Blocking", true);
		playerRight.characterAnims.SetBool ("Blocking", true);
		currentMove = 0;
		correctMoves = 0;
		seqMoves = 1;
		contactA = 1;
		contactB = 1;	
		setWindup ();
	}
	
	public void generateMove(){
		if (!game.counterActive)
		{
			contactA = Random.Range (minEnum, maxEnum);
			contactB = Random.Range (minEnum, maxEnum);
			setWindup ();
		}
	}

	public void setWindup() {
		playerLeft.GetComponent<PlayerAnimations>().SetAnim(contactA);
		playerRight.GetComponent<PlayerAnimations>().SetAnim(contactB);
		prompts.moveCallout ();
		}
	
	public void onSuccess() {
		playerLeft.moveSuccess ();
		playerRight.moveSuccess ();
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. 
	 * RETURN: 
	 * - true if both players did the move that was asked
	 * - false otherwise 
	 * -------------------------------------------------------------------------------------------------------------------------- */

	 public bool checkBothEvents(){
			correctA = checkTouchA(contactA);
			correctB = checkTouchB(contactB);
		if (correctA && correctB){			
			//Hax
			if (defending) {
				blocked = true;
				GameObject.Find("Forcefield").GetComponent<Display_Forcefield>().showField = true;
				game.srcSeqSound.clip = game.clipBlockSuccess;
				game.srcSeqSound.Play ();
			}
	 		return true;
	 	}
		return false; 
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


	public void DamagePlayers(int damage){
		hp -= damage; 
	}
	//test change


}