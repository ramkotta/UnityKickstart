
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

	public void addZombees(int numZombees, int id){
		int beeDiff = numZombees - attackingBees;
		totalBees += beeDiff;
		message = "You sent in " + attackingBees + " zombees";
		if (beeDiff > 0){
			message = " and brought back " + beeDiff + " new zom-bees!";
			houses[id].GetComponent<HouseScript>().createHouse(difficulty);
		} else if (beeDiff < 0){
			message = " but only  " + -1 * beeDiff + " came back.";
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
			if (timeElapsed >= 4){
				showNotification = false;
			}
			GUI.BeginGroup(new Rect(10, 10, menu_width / 3, menu_height / 4));
			GUI.DrawTexture(new Rect(0, 0, menu_width / 3, menu_height / 4), menu);
			GUI.color = Color.black;
			GUI.Label(new Rect(10, 10, 200, 30), message);
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

