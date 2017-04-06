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
		Joint[] joints = gameObject.GetComponents<CharacterJoint> ();
		if (joints != null && joints.Length > 0) {
			foreach (Joint joint in joints) {
				Destroy (joint);
			}
		}
	}
}
