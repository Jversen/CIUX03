using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

	public GameObject sword;
	public Rigidbody rb_sword;
	private int k;

	// Use this for initialization
	void Start () {
		sword = gameObject;
		rb_sword = GetComponent<Rigidbody> ();

		//rb_sword.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(k == 1){
			rb_sword.AddForce (Vector3.up * 20);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Hand")){
			//rb_sword.useGravity = true;

			Destroy(GetComponent("Halo"));

			//rb_sword.isKinematic = false;
			k = 1;
		}
	}
}
