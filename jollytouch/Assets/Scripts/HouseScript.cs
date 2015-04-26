using UnityEngine;
//using System.Collections;
using System;

public class HouseScript : MonoBehaviour {
	
	int defendingBees;
	int attackingBees;
	string strNumBees = "";

	System.Random rand;
	public int id;
	public Texture2D infoMenu;
	public GameObject parent;

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
		createHouse(0);
	}
	
	public void createHouse(int difficulty) {
		bugSprayStatus = null;
		flowerStatus = null;
		honeyStatus = null;
		hiveStatus = null;
		double amountIncr = 0;
		double amountDecr = 0;

		defendingBees = rand.Next (10, 20);
		attackingBees = 0;

		if (defendingBees != 0) {
			int chanceBugSpray = rand.Next(40 + difficulty);
			if (chanceBugSpray <= 10) {
				amountDecr += 0.5;
				bugSprayStatus = "Bug Spray";
			}
			int chanceFlowers = rand.Next(40 - difficulty);
			if (chanceFlowers <= 10) {
				amountIncr += 0.1;
				flowerStatus = "Flowers";
			}
			int chanceHoney = rand.Next(50 - difficulty);
			if (chanceHoney <= 10) {
				chanceHoney = rand.Next(40 - difficulty);
				if (chanceHoney <= 10) {
					amountIncr += 0.2;
					honeyStatus = "Slight scent of honey";
				} else if (chanceHoney <= 20) {
					amountIncr += 0.3;
					honeyStatus = "Distinct scent of honey";
				} else if (chanceHoney <= 30) {
					amountIncr += 0.4;
					honeyStatus = "Strong scent of honey";
				}
			}
			int chanceHive = rand.Next(100 - difficulty);
			if (chanceHive <= 10) {
				amountIncr += 1;
				hiveStatus = "Bee hive present";
			}
			if (amountIncr != 0) {
				defendingBees = (int) Math.Round(defendingBees - amountDecr*defendingBees + amountIncr*defendingBees);
			}
		}
		Debug.Log (id + " bees: " + defendingBees);
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
			int col = id % 3 + 1;
			int row = id / 3 + 1;
			float pivot_x = col * Screen.width / 5f;
			float pivot_y = row * Screen.height / 5f;
			//GUI.BeginGroup(new Rect(400 + (int)this.transform.position.x * 10, 100 + (int)this.transform.position.y * 10, menu_width, menu_height));
			GUI.BeginGroup(new Rect((int)pivot_x, (int)pivot_y, menu_width, menu_height));
			GUI.DrawTexture(new Rect(0, 0, menu_width / 4, menu_height / 2), infoMenu);
			GUI.color = Color.black;
			int text_y = 10;
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
				text_y += 20;
			}
			if (text_y == 10){
				GUI.Label(new Rect(20, text_y, 150, 20), "No notable info");
			}
			if (attackSetup){
				GUI.Label(new Rect(20, 110, 150, 20), "Number of beees to send in:");
				strNumBees = GUI.TextField(new Rect(20, 130, 80, 20), strNumBees);
				if (GUI.Button(new Rect(20, 150, 50, 20), "Attack!")){			
					if (Int32.TryParse(strNumBees, out attackingBees)){
						if (parent.GetComponent<BeeSystem>().setAttackingBees(attackingBees)){
							if (attackingBees > defendingBees){
								attackingBees /= 3;
								attackingBees += defendingBees;
							} else if (attackingBees == defendingBees){
								//stuff
							} else {
								attackingBees *= -1;
							}
							parent.GetComponent<BeeSystem>().addZombees(defendingBees, attackingBees, id);
						}
					} else {
						Debug.Log("Das it mane");
					}
					attackSetup = false;
					mouseOver = false;
				}
				if (GUI.Button(new Rect(100, 150, 50, 20), "Cancel")){
					attackSetup = false;
					mouseOver = false;
				}
			} else {
				GUI.Label(new Rect(20, 140, 150, 20), "Click to attack now");
			}
			GUI.EndGroup();
		}
	}

}
