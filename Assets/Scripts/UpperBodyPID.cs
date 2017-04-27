using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBodyPID : MonoBehaviour {
	public bool isQuadruped;
	private Rigidbody body;
	private Vector3 goal;
	// Use this for initialization
	void Start () {
		print (isQuadruped);
		body = GetComponent<Rigidbody> ();
		goal = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 error = new Vector3 (angleDifference (goal.x, body.transform.rotation.eulerAngles.x), 0, angleDifference (goal.y, body.transform.rotation.eulerAngles.z));
	}

	float angleDifference(float a1, float a2){
		float largest;
		float smallest;
		if (a1 > a2) {
			largest = a1;
			smallest = a2;
		} else {
			largest = a2;
			smallest = a1;
		}
		float diff1 = largest - smallest;
		float diff2 = Mathf.Abs (smallest - largest);
		if (diff1 < diff2) {
			return diff1;
		} else {
			return diff2;
		}
	}
}

