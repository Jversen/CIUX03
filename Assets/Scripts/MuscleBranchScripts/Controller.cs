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
		//GetComponent<Renderer> ().bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) {
			head.AddForce (transform.forward * force);
			//head.AddTorque(0,-force,0);
		} else if (Input.GetKey (right)) {
			head.AddForce (transform.forward * -force);
			//head.AddTorque(0, force,0);
		} else if (Input.GetKey (up)) {
			head.AddForce (transform.right * force); //TODO: Change this if forward is no longer left.
		} else if (Input.GetKey (down)) {
			head.AddForce (transform.right * -force);
		} 
	}
}
