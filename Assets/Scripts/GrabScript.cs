using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

	public GameObject sword;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Hand")){
			// Flyttar svärdet så att handtaget är i handen. Kommer behöva justeras beroende på karaktär om detta format behålls.
			// Går ej att flytta handle till positionen, då svärdet går sönder.
			sword.transform.position = new Vector3(col.gameObject.transform.position[0], col.gameObject.transform.position[1] + (0.85f), col.gameObject.transform.position[2]);

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
