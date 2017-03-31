using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMuscles : MonoBehaviour {
	public Rigidbody leftLowerHamstring, leftUpperHamstring, rightLowerHamstring, rightUpperHamstring;
	public Muscle leftHamstring, rightHamstring;
	public float force;
	// Use this for initialization
	void Start () {
		leftHamstring = new Muscle (leftUpperHamstring, leftLowerHamstring);
		rightHamstring = new Muscle (rightUpperHamstring, rightLowerHamstring);
	}
	
	// Update is called once per frame
	void Update () {
		leftHamstring.MoveMuscle (force);
		rightHamstring.MoveMuscle (force);
	}
	public class Muscle{
		private Rigidbody upper, lower;
		public Muscle(Rigidbody upper, Rigidbody lower)
		{
			this.upper = upper;
			this.lower = lower;
		}
		public void MoveMuscle(float force)
		{
			Vector3 direction1 = upper.transform.position - lower.transform.position;
			direction1.Normalize();
			direction1 = direction1 * force;
			Vector3 direction2 = -direction1;
			upper.AddForce(direction2 * Time.deltaTime);
			lower.AddForce(direction1 * Time.deltaTime);
		}
	}
}
