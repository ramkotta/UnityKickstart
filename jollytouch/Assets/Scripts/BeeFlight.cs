using UnityEngine;
//using System.Collections;
using System;

public class BeeFlight : MonoBehaviour {

	public GameObject node;

	// Use this for initialization
	void Start () {
	
	}

	void Orbit() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {    

		System.Random rand = new System.Random ();

		float x = rand.Next (200, 300);
		float y = rand.Next (5);
		float z = rand.Next (5);

		transform.RotateAround (node.transform.position, Vector3.forward, x * Time.deltaTime);
		node.transform.RotateAround (Vector3.right, Vector3.back, x / 2 * Time.deltaTime);

		node.transform.Translate (Vector3.left * Time.deltaTime);
		node.transform.RotateAround (Vector3.right, Vector3.forward, x/3 * Time.deltaTime);
		node.transform.Translate (Vector3.right * Time.deltaTime);
		//node.transform.RotateAround (Vector3.zero, Vector3.down, 20 * Time.deltaTime);
		//node.transform.RotateAround (Vector3.zero, Vector3.left, 20 * Time.deltaTime);
		//node.transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);
		//node.transform.RotateAround (Vector3.zero, Vector3.right, 20 * Time.deltaTime);
	}
}
