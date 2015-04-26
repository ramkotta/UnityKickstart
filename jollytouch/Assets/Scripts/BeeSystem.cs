
using UnityEngine;
using System.Collections;

public class BeeSystem : MonoBehaviour {
	
	int totalBees;
	int idleBees;
	int attackingBees;
	
	// Use this for initialization
	void Start () {
		totalBees = 20;
		idleBees = totalBees;
		attackingBees = 0;
	}
	
	void setAttackingBees(int attackers) {
		if (hasIdleBees ()) {
			attackingBees += attackers;
			idleBees = totalBees - attackingBees;
		}  else {
			Debug.Log("There are no bees left to attack with.");
		}
	}
	
	int getAttackingBees() {
		return attackingBees;
	}
	
	bool hasIdleBees() {
		return idleBees != 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

