using UnityEngine;
//using System.Collections;
using System;

public class HouseScript : MonoBehaviour {

	int initNumBees;
	int numBees;
	string strNumBees = "";

	System.Random rand;
	public int id;
	public Texture2D infoMenu;

	bool mouseOver = false;
	bool attackSetup = false;

	string bugSprayStatus;
	string flowerStatus;
	string honeyStatus;
	string hiveStatus;

	int menu_width = 662;
	int menu_height = 371;

	// Use this for initialization
	void Start () {
		//this is where we generate info for the house.
		rand = new System.Random ();

		double amountIncr = 0;
		double amountDecr = 0;

		numBees = rand.Next (10, 20);
		Debug.Log ("Initial number of bees is: " + numBees);

		if (numBees != 0) {
			int chanceBugSpray = rand.Next(1,4);
			Debug.Log ("Bug spray number is: " + chanceBugSpray);
			if (chanceBugSpray == 1) {
				amountDecr += 0.5;
				bugSprayStatus = "Bug Spray";
			}
			int chanceFlowers = rand.Next(1,4);
			Debug.Log ("Flower number is: " + chanceFlowers);
			if (chanceFlowers == 1) {
				amountIncr += 0.1;
				flowerStatus = "Flowers";
			}
			int chanceHoney = rand.Next(1,5);
			Debug.Log ("Honey number is: " + chanceHoney);
			if (chanceHoney == 1) {
				chanceHoney = rand.Next(1,4);
				Debug.Log ("Second Honey number is: " + chanceHoney);
				if (chanceHoney == 1) {
					amountIncr += 0.2;
					honeyStatus = "Slight scent of honey";
				} else if (chanceHoney == 2) {
					amountIncr += 0.4;
					honeyStatus = "Distinct scent of honey";
				} else if (chanceHoney == 3) {
					amountIncr += 0.6;
					honeyStatus = "Strong scent of honey";
				}
			}
			int chanceHive = rand.Next(1,11);
			Debug.Log ("Hive number is: " + chanceHive);
			if (chanceHive == 1) {
				amountIncr += 2;
				hiveStatus = "Bee hive present";
			}
			if (amountIncr != 0) {
				numBees = (int) Math.Round(numBees - amountDecr*numBees + amountIncr*numBees);
			}
		}
		Debug.Log ("Final Number of bees is: " + numBees);

	}
	
	// Update is called once per frame
	void Update () {
		//may not use?
	}

	void OnMouseEnter() {
		//this is when we display info about the house
		//interpret the numbers we have into words
		if (!attackSetup) mouseOver = true;
	}

	void OnMouseExit() {
		if (!attackSetup) mouseOver = false;
	}

	void OnMouseDown() {
		//this is the start of the attack phase
		//add a textfield of some sort
		attackSetup = true;
	}

	void OnGUI(){
		if (mouseOver || attackSetup){
			int col = id % 3;
			int row = id / 3;
			float pivot_x = col * Screen.width / 3f;
			float pivot_y = row * Screen.height / 3f;
			//GUI.BeginGroup(new Rect(400 + (int)this.transform.position.x * 10, 100 + (int)this.transform.position.y * 10, menu_width, menu_height));
			GUI.BeginGroup(new Rect((int)pivot_x, (int)pivot_y, menu_width, menu_height));
			GUI.DrawTexture(new Rect(0, 0, menu_width / 4, menu_height / 2), infoMenu);
			GUI.color = Color.black;
			GUI.Label(new Rect(20, 10, 100, 20), "House Info:");
			int text_y = 30;
			if (bugSprayStatus != null) {
				GUI.Label(new Rect(20, text_y, 150, 20), bugSprayStatus);
				text_y += 20;
			}
			if (flowerStatus != null) {
				GUI.Label(new Rect(20, text_y, 150, 20), flowerStatus);
				text_y += 20;
			}
			if (honeyStatus != null) {
				GUI.Label(new Rect(20, text_y, 150, 20), honeyStatus);
				text_y += 20;
			}
			if (hiveStatus != null) {
				GUI.Label(new Rect(20, text_y, 150, 20), hiveStatus);
			}
			if (attackSetup){
				GUI.Label(new Rect(20, 110, 150, 20), "Number of beees to send in:");
				strNumBees = GUI.TextField(new Rect(20, 130, 80, 20), strNumBees);
				if (GUI.Button(new Rect(20, 150, 100, 20), "Attack!")){			
					int numBees;
					if (Int32.TryParse(strNumBees, out numBees)){
						Debug.Log(numBees);
					} else {
						Debug.Log("Das it mane");
					}
					attackSetup = false;
				}
			} else {
				GUI.Label(new Rect(20, 140, 150, 20), "Click to attack now");
			}
			GUI.EndGroup();
		}
	}

}
