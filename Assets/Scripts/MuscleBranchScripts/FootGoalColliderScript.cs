using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootGoalColliderScript : MonoBehaviour {

	private bool isSetup, isReached;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(bool setupStatus){
		this.isSetup = setupStatus;
	}

	public bool IsSetup(){
		//print ("Is spring joint set up? " + isSetup);
		return isSetup;
	}

	public bool IsReached(){
		//print ("isReached? " + isReached);
		return isReached;
	}

	public void CreateJoint(Rigidbody rb){
		//print ("Creating springjoint to " + rb.tag);
		isReached = false;
		SpringJoint joint = gameObject.AddComponent<SpringJoint> ();
		joint.enableCollision = true;
		joint.spring = 225f;
		gameObject.GetComponent<SpringJoint> ().connectedBody = rb;
	}

	void OnTriggerEnter(Collider coll){
		//print ("Some collision, object with tag " + coll.gameObject.tag + " collided with footGoalTrigger");
		if (isSetup && (coll.gameObject.tag == "LeftFoot" || coll.gameObject.tag == "RightFoot")) {
			//print ("Collided with " + coll.gameObject.tag + "Destroying springjoint");
			isReached = true;
			Destroy (GetComponent<SpringJoint> ());
		}

	}

	void OnTriggerExit(Collider coll){

	}


}
