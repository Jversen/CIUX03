using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		Joint joint = gameObject.GetComponent<CharacterJoint> ();
		if (joint != null) {
			Destroy (joint);
		}
	}
}
