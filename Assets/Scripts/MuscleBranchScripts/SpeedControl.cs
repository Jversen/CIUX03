using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour {
	public Rigidbody head, lowerLeftLeg, upperLeftLeg, lowerRightLeg, upperRightLeg, leftFoot, rightFoot;
	public Rigidbody p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p17,p18,p19,p20,p21,p22,p23,p24,p25,p26,p27,p28,p29,p30,p31,p32,p33,p34,p35,p36;
	public Rigidbody q1,q2,q3,q4,q5,q6,q7,q8,q9,q10,q11,q12;
	public float m, a;
	private List<Rigidbody> rigidbodies;
	// Use this for initialization
	void Start () {
		rigidbodies = new List<Rigidbody> ();
		rigidbodies.Add (head);
		rigidbodies.Add (upperLeftLeg);
		rigidbodies.Add (upperRightLeg);
		rigidbodies.Add (lowerLeftLeg);
		rigidbodies.Add (lowerRightLeg);
		rigidbodies.Add (leftFoot);
		rigidbodies.Add (rightFoot);
		rigidbodies.Add (p1);
		rigidbodies.Add (p2);
		rigidbodies.Add (p3);
		rigidbodies.Add (p4);
		rigidbodies.Add (p5);
		rigidbodies.Add (p6);
		rigidbodies.Add (p7);
		rigidbodies.Add (p8);
		rigidbodies.Add (p9);
		rigidbodies.Add (p10);
		rigidbodies.Add (p11);
		rigidbodies.Add (p12);
		rigidbodies.Add (p13);
		rigidbodies.Add (p14);
		rigidbodies.Add (p15);
		rigidbodies.Add (p16);
		rigidbodies.Add (p17);
		rigidbodies.Add (p18);
		rigidbodies.Add (p19);
		rigidbodies.Add (p20);
		rigidbodies.Add (p21);
		rigidbodies.Add (p22);
		rigidbodies.Add (p23);
		rigidbodies.Add (p24);
		rigidbodies.Add (p25);
		rigidbodies.Add (p26);
		rigidbodies.Add (p27);
		rigidbodies.Add (p28);
		rigidbodies.Add (p29);
		rigidbodies.Add (p30);
		rigidbodies.Add (p31);
		rigidbodies.Add (p32);
		rigidbodies.Add (p33);
		rigidbodies.Add (p34);
		rigidbodies.Add (p35);
		rigidbodies.Add (p36);
		rigidbodies.Add (q1);
		rigidbodies.Add (q2);
		rigidbodies.Add (q3);
		rigidbodies.Add (q4);
		rigidbodies.Add (q5);
		rigidbodies.Add (q6);
		rigidbodies.Add (q7);
		rigidbodies.Add (q8);
		rigidbodies.Add (q9);
		rigidbodies.Add (q10);
		rigidbodies.Add (q11);
		rigidbodies.Add (q12);
		foreach (Rigidbody r in rigidbodies) {
			r.maxAngularVelocity = a;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach (Rigidbody r in rigidbodies) {
			SetMaxMagnitude (r, m);
		}
	}


	private void SetMaxMagnitude(Rigidbody r, float m){
		if (r.velocity.magnitude > m) {
			
			r.velocity = r.velocity.normalized * m;
		}
	}
}
