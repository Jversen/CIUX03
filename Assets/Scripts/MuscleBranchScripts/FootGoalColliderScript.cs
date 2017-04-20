using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootGoalColliderScript : MonoBehaviour {

	private bool isReached;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsReached(){
		print ("isReached? " + isReached);
		return isReached;
	}

	public void CreateJoint(Rigidbody rb){
		print ("Creating springjoint to " + rb.tag);
		isReached = false;
		SpringJoint joint = gameObject.AddComponent<SpringJoint> ();
		joint.enableCollision = true;
		joint.spring = 1200f;
		gameObject.GetComponent<SpringJoint> ().connectedBody = rb;
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "leftFoot" && coll.gameObject.tag == "rightFoot") {
			print ("Collided with " + coll.gameObject.tag + "Destroying springjoint");
			isReached = true;
			Destroy (GetComponent<SpringJoint> ());
		}

	}

	void OnTriggerExit(Collider coll){

	}


}
