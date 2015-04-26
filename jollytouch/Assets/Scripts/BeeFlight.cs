using UnityEngine;
//using System.Collections;
using System;

public class BeeFlight : MonoBehaviour {

	public GameObject node;

	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {    

		System.Random rand = new System.Random ();

		float x = rand.Next (1, 5);
		float y = rand.Next (1, 5);
		Debug.Log ("This bee's x velocity is: " + x);
		Debug.Log ("This bee's y velocity is: " + y);
		transform.Translate (0.01f*x, 0.01f*y, 0f, Space.Self);
		x = rand.Next (1, 5)*-1;
		y = rand.Next (1, 5)*-1;
		Debug.Log ("This bee's second x velocity is: " + x);
		Debug.Log ("This bee's second y velocity is: " + y);
		transform.Translate (0.01f*x, 0.01f*y, 0f, Space.Self);

	}
}
