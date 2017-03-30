using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour {

	bool collide = true; //Only collide once to avoid too many fast slices

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (collide && col.rigidbody != null) {

			//TODO: Cut is not currently in the right angle or position
			Vector3 contactPoint = new Vector3(0, 0, 0);
			int nbrOfContacts = col.contacts.Length;
			for (int i = 0; i < nbrOfContacts; i++) {
				contactPoint = contactPoint + col.contacts [i].point;
			}
			contactPoint = new Vector3 (contactPoint.x / nbrOfContacts, contactPoint.y / nbrOfContacts, contactPoint.z / nbrOfContacts);

			//Make the cut
			//col.rigidbody.transform.up = perpendicular to sword => cuts the plane parallel to the sword
			Material material = gameObject.GetComponent<Renderer> ().material;
			GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (gameObject, contactPoint, col.gameObject.transform.up, material);

			//pieces[0] is the original object, pieces[1] is the new, sliced of piece
			Destroy (pieces [0].GetComponent < BoxCollider> ());
			pieces [0].AddComponent<MeshCollider> ();
			pieces [0].GetComponent<MeshCollider> ().convex = true;
			pieces [1].transform.localScale = gameObject.transform.localScale;
			pieces [1].AddComponent<MeshCollider> ();
			pieces [1].GetComponent<MeshCollider> ().convex = true;
			pieces [1].AddComponent<Rigidbody> ();
			collide = false;
		}
	}
}
