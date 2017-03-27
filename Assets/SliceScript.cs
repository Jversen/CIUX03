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
		if (collide) {

			//TODO: Cut is not currently in the right angle or position
			Renderer renderer = gameObject.GetComponent<Renderer> ();
			Vector3 contactPoint = col.contacts [0].normal;
			Vector3 contactDirection = transform.position - contactPoint;

			//Make the cut
			Material material = renderer.material;
			GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (gameObject, transform.position, contactDirection, material);

			//pieces[0] is the original object, pieces[0] is the new, sliced of piece
			GameObject newPiece = pieces [1];
			//Add rigidbody and collider to sliced of piece
			newPiece.AddComponent<Rigidbody> ();
			newPiece.AddComponent<BoxCollider> ();

			//Resize both box colliders to fit pieces
			for (int i = 0; i < 2; i++) {
				BoxCollider collider = (BoxCollider)pieces [i].GetComponent<Collider> ();
				Bounds bounds = pieces [i].GetComponent<Renderer> ().bounds;
				collider.center = bounds.center - pieces [i].transform.position;
				collider.size = bounds.size;
			}
			collide = false;
		}
	}
}
