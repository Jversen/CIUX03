using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMuscles : MonoBehaviour {
	public Rigidbody leftLowerHamstring, leftUpperHamstring, rightLowerHamstring, rightUpperHamstring;
	public Rigidbody leftLowerGluteus, leftUpperGluteus, rightLowerGluteus, rightUpperGluteus;
	public Rigidbody leftLowerHip, leftUpperHip, rightLowerHip, rightUpperHip;
	public Rigidbody lowerAbs, upperAbs;
	public Rigidbody lowerBack, upperBack;
	public Muscle leftHamstring, rightHamstring;
	public Muscle leftGluteus, rightGluteus;
	public Muscle leftHip, rightHip;
	public Muscle abs;
	public Muscle back;
	public Rigidbody leftFoot, rightFoot ;
	public float forceHamstring, forceGluteus, forceHip, forceAbs, forceBack;
	private bool leftFootContact = false; 
	private bool rightFootContact = false;
	private Vector3 leftFootCoG, rightFootCoG, bodyCoG, betweenFeetVector, leftToBodyCoG, rightToBodyCoG, leftFootGoalCoG, rightFootGoalCoG;
	private float feetDistance, balanceLineDistance;
	// Use this for initialization
	void Start () {
		leftHamstring = new Muscle (leftUpperHamstring, leftLowerHamstring);
		rightHamstring = new Muscle (rightUpperHamstring, rightLowerHamstring);
		leftGluteus = new Muscle (leftUpperGluteus, leftLowerGluteus);
		rightGluteus = new Muscle (rightUpperGluteus, rightLowerGluteus);
		leftHip = new Muscle (leftUpperHip, leftLowerHip);
		rightHip = new Muscle (rightUpperHip, rightLowerHip);
		abs = new Muscle (upperAbs, lowerAbs);
		back = new Muscle (upperBack, lowerBack);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		leftHamstring.MoveMuscle (forceHamstring);
		rightHamstring.MoveMuscle (forceHamstring);
		leftGluteus.MoveMuscle (forceGluteus);
		rightGluteus.MoveMuscle (forceGluteus);
		leftHip.MoveMuscle (forceHip);
		rightHip.MoveMuscle (forceHip);
		abs.MoveMuscle (forceAbs);
		back.MoveMuscle (forceBack);
	}
	void OnCollisionEnter(Collision coll){
		print ("Entered OnCollisionEnter");
		if (coll.contacts[0].thisCollider.gameObject.CompareTag("Ground")){
			if (coll.contacts [1].thisCollider.gameObject.CompareTag ("LeftFoot")) {
				leftFootContact = true; 
			} else if (coll.contacts [1].thisCollider.gameObject.CompareTag ("RightFoot")) {
				rightFootContact = true;
			}
		} else if (coll.contacts[1].thisCollider.gameObject.CompareTag("Ground")){
			if (coll.contacts [0].thisCollider.gameObject.CompareTag ("LeftFoot")) {
				leftFootContact = true; 
			} else if (coll.contacts [0].thisCollider.gameObject.CompareTag ("RightFoot")) {
				rightFootContact = true;
			}
		}
		print ("right foot: " + rightFootContact);
		print ("left foot: " + leftFootContact);
	}
	void OnCollisionExit(Collision coll){
		print ("Entered OnCollisionExit");
		if (coll.contacts[0].thisCollider.gameObject.CompareTag("Ground")){
			if (coll.contacts [1].thisCollider.gameObject.CompareTag ("LeftFoot")) {
				leftFootContact = false; 
			} else if (coll.contacts [1].thisCollider.gameObject.CompareTag ("RightFoot")) {
				rightFootContact = false;
			}
		} else if (coll.contacts[1].thisCollider.gameObject.CompareTag("Ground")){
			if (coll.contacts [0].thisCollider.gameObject.CompareTag ("LeftFoot")) {
				leftFootContact = false; 
			} else if (coll.contacts [0].thisCollider.gameObject.CompareTag ("RightFoot")) {
				rightFootContact = false;
			}
		}
		print ("right foot: " + rightFootContact);
		print ("left foot: " + leftFootContact);
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
