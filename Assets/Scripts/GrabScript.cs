using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Hand")){
			/*Component handle = GetComponent ("Handle");
			if (handle != null) {
				col.gameObject.transform.position = handle.transform.position;
			}*/
			//gameObject.transform.parent = col.gameObject.transform;
			gameObject.AddComponent<FixedJoint> ();
			GetComponent<FixedJoint> ().connectedBody = col.rigidbody;
		}
	}
}
