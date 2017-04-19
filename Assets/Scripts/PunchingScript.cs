using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingScript : MonoBehaviour {

	public Rigidbody fist;
	public Rigidbody home;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("k")){
			Vector3 fistPos = fist.transform.position;
			fist.transform.position = transform.position;
			SpringJoint joint = fist.GetComponent<SpringJoint> ();
			joint.connectedBody = GetComponent<Rigidbody>();
			joint.spring = 100f;
			fist.transform.position = fistPos;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Rigidbody>().Equals(fist)){
			Vector3 fistPos = fist.transform.position;
			fist.transform.position = home.transform.position;
			SpringJoint joint = fist.GetComponent<SpringJoint> ();
			joint.connectedBody = home;
			joint.spring = 200f;
			fist.transform.position = fistPos;
		}
	}
}
