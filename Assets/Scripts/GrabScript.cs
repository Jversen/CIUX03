using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

	public GameObject hand;
	public Rigidbody rb_hand;
	public GameObject player_body;

	private int k;

	public Vector3 swing;

	// Use this for initialization
	void Start () {
		hand = this.gameObject;
		rb_hand = this.GetComponent<Rigidbody> ();
		player_body = transform.parent.gameObject.transform.parent.gameObject;
		k = 0;

		//Debug.Log (hand);
		//Debug.Log (rb_hand);
		//Debug.Log (player_body);
		Debug.Log (transform.parent.gameObject);
		Debug.Log (transform.parent.gameObject.transform.parent.gameObject);
	}

	// Update is called once per frame
	void Update () {
		//hand måste vara här då den är beroende av koordinater
		hand = this.gameObject;
		if ((Input.GetKeyDown ("space")) && (k == 1)) {
			Destroy (GetComponent<FixedJoint> ());
			k = 0;
		} else if ((Input.GetKeyDown ("j")) && (k == 1)) {
			swing = player_body.transform.forward;
			rb_hand.AddForce (swing * 1000);
			//rb_sword.AddTorque (0, 0, 0);
		} /*else if (k == 1) {
			rb_hand.AddForce (Vector3.up * 20);
		}*/
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Weapon")){
			// Flyttar svärdet så att handtaget är i handen. Kommer behöva justeras beroende på karaktär om detta format behålls.
			// Går ej att flytta handle till positionen, då svärdet går sönder.

			col.gameObject.transform.position = new Vector3(hand.transform.position[0], hand.transform.position[1] 
															+ (0.85f), hand.transform.position[2]);
			gameObject.AddComponent<FixedJoint> ();
			GetComponent<FixedJoint> ().connectedBody = col.rigidbody;

			k = 1;
		}
	}
}