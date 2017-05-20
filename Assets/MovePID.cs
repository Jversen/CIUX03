using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePID : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left")){
			gameObject.GetComponent<Rigidbody> ().AddForce (0, 0, -50);
		}else if(Input.GetKey("right")){
			gameObject.GetComponent<Rigidbody> ().AddForce (0, 0, 50);
		}else if(Input.GetKey("up")){
			gameObject.GetComponent<Rigidbody> ().AddForce (-50, 0, 0);
		}else if(Input.GetKey("down")){
			gameObject.GetComponent<Rigidbody> ().AddForce (50, 0, 0);
		}
	}
}
