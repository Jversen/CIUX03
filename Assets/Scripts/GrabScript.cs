using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

	public GameObject sword;
	public Rigidbody rb_sword;

	public GameObject cube_body;

	private int k;

	public Vector3 swing;

	// Use this for initialization
	void Start () {
		k = 0;
	}

	// Update is called once per frame
	void Update () {
		swing = cube_body.transform.forward;
		if ((Input.GetKeyDown ("space") && (k == 1))) {
			Destroy (GetComponent<FixedJoint> ());
			k = 0;
		} else if (Input.GetKeyDown ("j") && (k == 1)) {
			rb_sword.AddForce (swing * 1000);
			//rb_sword.AddTorque (0, 0, 0);
		} else if (k == 1) {
			rb_sword.AddForce (Vector3.up * 20);
		}
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

			rb_sword.useGravity = false;
			k = 1;
		}
	}
}