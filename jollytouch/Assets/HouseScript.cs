using UnityEngine;
//using System.Collections;
using System;

public class HouseScript : MonoBehaviour {

	int initNumBees;
	int numBees;

	System.Random rand;

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
			}
			int chanceFlowers = rand.Next(1,4);
			Debug.Log ("Flower number is: " + chanceFlowers);
			if (chanceFlowers == 1) {
				amountIncr += 0.1;
			}
			int chanceHoney = rand.Next(1,5);
			Debug.Log ("Honey number is: " + chanceHoney);
			if (chanceHoney == 1) {
				chanceHoney = rand.Next(1,4);
				Debug.Log ("Second Honey number is: " + chanceHoney);
				if (chanceHoney == 1) {
					amountIncr += 0.2;
				} else if (chanceHoney == 2) {
					amountIncr += 0.4;
				} else if (chanceHoney == 3) {
					amountIncr += 0.6;
				}
			}
			int chanceHive = rand.Next(1,11);
			Debug.Log ("Hive number is: " + chanceHive);
			if (chanceHive == 1) {
				amountIncr += 2;
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
	}

	void OnMousDown() {
		//this is the start of the attack phase
		//add a textfield of some sort
	}
}
