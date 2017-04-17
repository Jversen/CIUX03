using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceIntelligence : MonoBehaviour {
	/*public Rigidbody leftLowerHamstring, leftUpperHamstring, rightLowerHamstring, rightUpperHamstring;
	public Rigidbody leftLowerGluteus, leftUpperGluteus, rightLowerGluteus, rightUpperGluteus;
	public Rigidbody leftLowerHip, leftUpperHip, rightLowerHip, rightUpperHip;
	public Rigidbody lowerAbs, upperAbs;
	public Rigidbody lowerBack, upperBack;
	public Muscle leftHamstring, rightHamstring;
	public Muscle leftGluteus, rightGluteus;
	public Muscle leftHip, rightHip;
	public Muscle abs;
	public Muscle back;*/
	public Rigidbody leftFoot, rightFoot, bodyCore;
	public float betweenFeetVectorToBodyCoGEpsilon;
	//public float forceHamstring, forceGluteus, forceHip, forceAbs, forceBack;
	private bool leftFootContact = false; 
	private bool rightFootContact = false;
	private Vector3 leftFootCoG, rightFootCoG, bodyCoG, betweenFeetVector, leftFootCoGToBodyCoG, rightFootCoGToBodyCoG, leftFootGoalCoG, rightFootGoalCoG,
	bodyCoGToFeetVector;
	private float feetDistance, balanceLineDistance, bodyCoGToFeetDistance;

	// Use this for initialization
	void Start () {
		/*leftHamstring = new Muscle (leftUpperHamstring, leftLowerHamstring);
		rightHamstring = new Muscle (rightUpperHamstring, rightLowerHamstring);
		leftGluteus = new Muscle (leftUpperGluteus, leftLowerGluteus);
		rightGluteus = new Muscle (rightUpperGluteus, rightLowerGluteus);
		leftHip = new Muscle (leftUpperHip, leftLowerHip);
		rightHip = new Muscle (rightUpperHip, rightLowerHip);
		abs = new Muscle (upperAbs, lowerAbs);
		back = new Muscle (upperBack, lowerBack);*/
	}

	// Update is called once per frame
	void FixedUpdate () {

		betweenFeetVector = getBetweenFeetVector (leftFoot, rightFoot);

		leftFootCoG = getCenterOfGravity (leftFoot);
		rightFootCoG = getCenterOfGravity (rightFoot);
		betweenFeetVector = leftFootCoG - rightFootCoG;

		bodyCoG = getCenterOfGravity (bodyCore);
		bodyCoGToFeetVector = bodyCoG - getOrthogonalProjection(bodyCoG, betweenFeetVector); //distance y - ŷ, bodyCoG vector minus its projection on betweenFeetVector
		leftFootCoGToBodyCoG = leftFootCoG - bodyCoG;
		rightFootCoGToBodyCoG = rightFootCoG - bodyCoG;

		//betweenFeetVector = getBetweenFeetVector(leftFoot, rightFoot);
		feetDistance = betweenFeetVector.magnitude;

		//leftFootCollider = leftFoot.GetComponent<Collider>();
		print ("Left foot's CoG: " + leftFootCoG);
		print ("Right foot's CoG: " + rightFootCoG);
		print ("Vector between feet: " + betweenFeetVector);
		print ("Distance between feet: " + feetDistance);

		print ("Torso's CoG: " + bodyCoG);
		print ("Vector between feetVector and bodyCoG: " + bodyCoGToFeetVector);
		print ("Distance between feetVector and bodyCoG: " + bodyCoGToFeetVector.magnitude);

		print ("Is body's CoG inside the feet's support polygon?: " + isBodySupported());



		/*leftHamstring.MoveMuscle (forceHamstring);
		rightHamstring.MoveMuscle (forceHamstring);
		leftGluteus.MoveMuscle (forceGluteus);
		rightGluteus.MoveMuscle (forceGluteus);
		leftHip.MoveMuscle (forceHip);
		rightHip.MoveMuscle (forceHip);
		abs.MoveMuscle (forceAbs);
		back.MoveMuscle (forceBack);*/
	}

	Vector3 getBetweenFeetVector(Rigidbody foot1, Rigidbody foot2){
		Vector3 feetVector = getCenterOfGravity(foot1) - getCenterOfGravity(foot2);
		return feetVector;
	}

	Vector3 getCenterOfGravity(Rigidbody rb){
		Vector3 tempCoG = rb.transform.TransformPoint (rb.centerOfMass);
		//Vector3 tempCoG = rb.centerOfMass;
		print ("CoG of " + rb + " is " + tempCoG);
		return new Vector3 (tempCoG[0], 0, tempCoG[2]); //Get twodimensional vector (X and Z coordinates)
	}

	Vector3 getOrthogonalProjection(Vector3 y, Vector3 u){
		Vector3 projection = (Vector3.Dot(y, u)/Vector3.Dot(u, u))*u; //Equation for y's projection on u
		return projection;
	}

	/* Is the body's center of gravity inside the feet's support polygon?*/
	bool isBodySupported(){
		
		if (Mathf.Abs(bodyCoGToFeetVector.magnitude) <= betweenFeetVectorToBodyCoGEpsilon){
			return true;
		} else {
			return false;
		}
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
