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



	public SpriteRenderer[] allMenu;
	public SpriteRenderer[] fivesChange;
	public SpriteRenderer[] fistChange;
	public SpriteRenderer[] elbowChange;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
