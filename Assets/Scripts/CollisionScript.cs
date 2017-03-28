using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
	
	Rigidbody body;
	public int splitXIn;
	public int splitYIn;
	public int splitZIn;
	int nbrOfCubes;
	float cubeSideLengthX;
	float cubeSideLengthY;
	float cubeSideLengthZ;
	Vector3 shrapnelScale;

	// Use this for initialization
	void Start () {
		body = gameObject.GetComponent<Rigidbody> ();
		if (splitXIn == 0) {
			splitXIn = 1;
		}
		if (splitYIn == 0) {
			splitYIn = 1;
		}
		if (splitZIn == 0) {
			splitZIn = 1;
		}
		nbrOfCubes = splitXIn * splitYIn * splitZIn; //splitWidthIn^3
		cubeSideLengthX = transform.localScale.x / splitXIn;
		cubeSideLengthY = transform.localScale.y / splitYIn;
		cubeSideLengthZ = transform.localScale.z / splitZIn;
		shrapnelScale = new Vector3 (cubeSideLengthX, cubeSideLengthY, cubeSideLengthZ);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		Vector3 parentPosition = transform.position;
		float parentMass = body.GetComponent<Rigidbody> ().mass;
		float shrapnelMass = parentMass / nbrOfCubes;
		Vector3 parentScale = transform.localScale;
		Destroy (gameObject);
		System.Random random = new System.Random ();
		Vector3 g = Physics.gravity;

		//Switching to (parentScale.x /2f) - 0.5f * cubeSideLengthX; doesn't work for some reason
		float distToCenterX	= (float)splitXIn /2*cubeSideLengthX - 0.5f * cubeSideLengthX; 
		float distToCenterY = (float)splitYIn /2*cubeSideLengthY - 0.5f * cubeSideLengthY;
		float distToCenterZ = (float)splitZIn /2*cubeSideLengthZ - 0.5f * cubeSideLengthZ;

		for (float x = -distToCenterX; x <= distToCenterX; x += cubeSideLengthX) {
			for (float y = -distToCenterY; y <= distToCenterY; y += cubeSideLengthY) {
				for (float z = -distToCenterZ; z <= distToCenterZ; z += cubeSideLengthZ) {
					GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
					cube.transform.localScale = shrapnelScale;
					cube.transform.position = parentPosition + new Vector3 (x, y, z);
					cube.AddComponent<Rigidbody>();
					cube.GetComponent<Rigidbody> ().mass = shrapnelMass;
					float forceRandom = random.Next (1,10)/5; //Arbitrary number used for shatering shrapnel
					Vector3 forceVector = new Vector3 (x * forceRandom, g.y * shrapnelMass, z * forceRandom);
					cube.GetComponent<Rigidbody>().AddForce(forceVector);
				}
			}
		}
	}
}
