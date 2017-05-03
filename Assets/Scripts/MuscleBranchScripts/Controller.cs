using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public Rigidbody head;
	public float force;

	public string up;
	public string down;
	public string left;
	public string right;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) {
			head.AddForce (0, 0,force);
		} else if (Input.GetKey (right)) {
				head.AddForce (0, 0, -force);
		} else if (Input.GetKey (up)) {
			head.AddForce (force, 0, 0);
		} else if (Input.GetKey (down)) {
				head.AddForce (-force, 0, 0);
		} 
			

	}
}
