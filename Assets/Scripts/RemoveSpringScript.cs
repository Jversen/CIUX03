using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSpringScript : MonoBehaviour {

	public GameObject connector;

	void OnCollisionEnter(Collision col){
		if(col.gameObject.Equals(connector)){
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			Destroy(connector.GetComponent<SpringJoint> ());
			Destroy(this);
		}
	}
}
