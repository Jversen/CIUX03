using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

	public Rigidbody head;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float pushHorizontal = Input.GetAxis ("Horizontal");
		head.AddForce (new Vector3 (0, 0, pushHorizontal * 20));

		float pushVertical = Input.GetAxis ("Vertical");
		head.AddForce (new Vector3 (0, pushVertical * 50, 0));
		
	}
}
