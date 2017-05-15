using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Chest" || col.gameObject.tag == "Head"){
			Ragdoll.MakeRagdoll (col.gameObject.transform.parent.parent.gameObject);
		}
	}
}
