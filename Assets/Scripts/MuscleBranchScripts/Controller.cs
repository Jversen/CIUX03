using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public Rigidbody body;
	public float force;

	public string up;
	public string down;
	public string left;
	public string right;

	private KinematicLegs kinematicLegs;

	// Use this for initialization
	void Start () {
		//GetComponent<Renderer> ().bounds.size;
		kinematicLegs = gameObject.GetComponent<KinematicLegs>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("left")) {
			body.AddTorque (transform.up * -(force/2f));
			//body.AddTorque(0,-force,0);
		} else if (Input.GetKey ("right")) {
			body.AddTorque (transform.up * (force/2f));
			//body.AddTorque(0, force,0);
		} else if (Input.GetKey ("up")) {
			//kinematicLegs.startRunning = true;
			body.AddForce (transform.right * force); //TODO: Change this if forward is no longer left.
		} else if (Input.GetKey ("down")) {
			//kinematicLegs.startRunning = true;
			body.AddForce (transform.right * -force);
		} 
	}
}
