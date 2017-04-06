using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footContact : MonoBehaviour {
	private bool hasGroundContact = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collisionInfo){
		print ("Entered OnCollisionEnter");
		if (collisionInfo.gameObject.CompareTag("Ground")){hasGroundContact = true;}
		print ("hasGroundContact = " + hasGroundContact);
	}
	void OnCollisionExit(Collision collisionInfo){
		print ("Entered OnCollisionExit");
		if (collisionInfo.gameObject.CompareTag("Ground")){hasGroundContact = false;}
		print ("hasGroundContact = " + hasGroundContact);
	}
}
