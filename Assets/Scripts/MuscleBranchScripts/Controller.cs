using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public Rigidbody head;
	public float force;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.U)) {
			head.AddForce (0, force, 0);
		} else if (Input.GetKey (KeyCode.J)) {
			head.AddForce (force, 0, 0);
		} else if (Input.GetKey (KeyCode.K)) {
			head.AddForce (0, 0, force);
		}
			

	}
}
