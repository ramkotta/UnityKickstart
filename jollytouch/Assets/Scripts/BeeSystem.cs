
using UnityEngine;
using System.Collections;

public class BeeSystem : MonoBehaviour {
	
	public GameObject[] houses;
	public Texture2D menu;

	bool showNotification = false;
	bool showingWindow = false;

	string message = "";

	int totalBees;
	int attackingBees;
	int housesConquered;
	int difficulty;
	int timeNotification = 2;
	float timeElapsed = 0;

	int menu_width = 662;
	int menu_height = 371;
	// Use this for initialization
	void Start () {
		totalBees = 30;
		attackingBees = 0;
		housesConquered = 0;
		difficulty = 0;
	}
	
	public bool setAttackingBees(int attackers) {
		if (totalBees >= attackers) {
			attackingBees = attackers;
			return true;
		}
		Debug.Log("Not enough bees");
		return false;
	}

	public void addZombees(int defendingBees, int numZombees, int id){
		int beeDiff = 0;
		if (numZombees == attackingBees) {
			beeDiff = 0;
		} else if (numZombees > attackingBees) {
			//totalBees-=attackingBees;
			beeDiff = numZombees - attackingBees;
		} else if (numZombees < 0) {
			beeDiff = numZombees;
		} else {
			beeDiff = numZombees - (attackingBees/3)*2;
		}
		totalBees += beeDiff;
		message = "You sent in " + attackingBees + " zom-bees";
		if (beeDiff > 0) {
			message += ". There were " + defendingBees + " defending bees! You sacrificed " + (attackingBees - attackingBees/3) + " zom-bees for a net " + beeDiff + " zom-bees!";
			houses [id].GetComponent<HouseScript> ().createHouse (difficulty);
		} else if (beeDiff == 0) {
			totalBees -= attackingBees;
			message += " but none came back.";
		} else if (beeDiff < 0){
			message += ". There were " + defendingBees + " defending bees! You lost your " + attackingBees + " zom-bees.";
		}
		Debug.Log(totalBees + ": " + numZombees + ": " + message);
		timeElapsed = 0;
		showNotification = true;
		if (numZombees >= 0) {
			housesConquered++;
			if (housesConquered % 3 == 0){
				difficulty++;
				for (int i = 0; i < houses.Length; i++){
					houses[i].GetComponent<HouseScript>().createHouse(difficulty);
				}
				Debug.Log(difficulty);
			}
		}
	}

	public bool showMenu(bool mouseOver, bool attackSetup){
		bool result = !showingWindow && (mouseOver || attackSetup);
		if (showNotification) showNotification = !(showNotification || result);
		showingWindow = result;
		Debug.Log(result);
		return result;
	}
	
	void OnGUI(){
		if (showNotification){
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= 8){
				showNotification = false;
			}
			GUI.BeginGroup(new Rect(10, 10, menu_width / 3, menu_height / 4));
			GUI.DrawTexture(new Rect(0, 0, menu_width / 3, menu_height / 4), menu);
			GUI.color = Color.black;
			GUI.Label(new Rect(10, 10, 200, 100), message);
			GUI.color = Color.white;
			GUI.EndGroup();
		}
		int screenW = Screen.width;
		int screenH = Screen.height;
		GUI.BeginGroup(new Rect(screenW * 5/7, screenH * 5/6, menu_width/ 4, menu_height / 5));
		GUI.DrawTexture(new Rect(0, 0, menu_width / 4, menu_height / 5), menu);
		GUI.color = Color.black;
		GUI.Label(new Rect(15, 10, 200, 30), "Zom-bees you control");
		GUI.Label(new Rect(15, 30, 50, 30), "" + totalBees);
		GUI.color = Color.white;
		GUI.EndGroup();
	}

	// Update is called once per frame
	void Update () {
		
	}
}

