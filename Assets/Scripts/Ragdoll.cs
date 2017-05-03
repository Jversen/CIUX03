using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public static void MakeRagdoll(GameObject ragdollObject){

		var components = ragdollObject.GetComponentsInChildren<Component>();
		foreach (Component component in components) {
			if (component is MonoBehaviour) {
				((MonoBehaviour)component).enabled = false;
			}
		}
	}


}
