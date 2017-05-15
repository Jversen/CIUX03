using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceIntelligence : MonoBehaviour {
	//public Rigidbody leftLowerHamstring, leftUpperHamstring, rightLowerHamstring, rightUpperHamstring;
	//public Rigidbody leftLowerGluteus, leftUpperGluteus, rightLowerGluteus, rightUpperGluteus;
	public Rigidbody leftLowerHip, leftUpperHip, rightLowerHip, rightUpperHip;
	//public Rigidbody lowerAbs, upperAbs;
	//public Rigidbody lowerBack, upperBack;
	//public Muscle leftHamstring, rightHamstring;
	//public Muscle leftGluteus, rightGluteus;
	public Muscle leftHip, rightHip;
	//public Muscle abs;
	//public Muscle back;
	public Rigidbody leftFoot, rightFoot, bodyCore, footGoalMarker;
	public float betweenFeetVectorToBodyCoGEpsilon, muscleForce;
	public float forceHamstring, forceGluteus, forceHip, forceAbs, forceBack;
	private bool leftFootContact = false;
	private bool rightFootContact = false;
	private Vector3 leftFootCoG, rightFootCoG, bodyCoG, betweenFeetVector, leftFootCoGToBodyCoG, rightFootCoGToBodyCoG, leftFootGoalCoG, rightFootGoalCoG,
	bodyCoGToFeetVector;
	private float feetDistance, balanceLineDistance, bodyCoGToFeetDistance;

	private Vector3 gizmoFromFoot, gizmoBodyCoG, gizmoToFoot; //For drawing gizmos for debug

	private bool isWalking;
	private FootGoalColliderScript fScript;

	// Use this for initialization
	void Start () {
		fScript = (FootGoalColliderScript) footGoalMarker.gameObject.GetComponent(typeof(FootGoalColliderScript));

		//leftHamstring = new Muscle (leftUpperHamstring, leftLowerHamstring);
		//rightHamstring = new Muscle (rightUpperHamstring, rightLowerHamstring);
		//leftGluteus = new Muscle (leftUpperGluteus, leftLowerGluteus);
		//rightGluteus = new Muscle (rightUpperGluteus, rightLowerGluteus);
		leftHip = new Muscle (leftUpperHip, leftLowerHip);
		rightHip = new Muscle (rightUpperHip, rightLowerHip);
		//abs = new Muscle (upperAbs, lowerAbs);
		//back = new Muscle (upperBack, lowerBack);
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.U)) {
			leftHip.MoveMuscle(muscleForce);
		} else if (Input.GetKey (KeyCode.O)) {
			rightHip.MoveMuscle(muscleForce);
		}

		betweenFeetVector = GetBetweenFeetVector (leftFoot, rightFoot);

		leftFootCoG = GetCenterOfGravity (leftFoot);
		rightFootCoG = GetCenterOfGravity (rightFoot);
		betweenFeetVector = leftFootCoG - rightFootCoG;

		bodyCoG = GetCenterOfGravity (bodyCore);
		bodyCoGToFeetVector = bodyCoG - GetOrthogonalProjection(bodyCoG, betweenFeetVector); //distance y - ŷ, bodyCoG vector minus its projection on betweenFeetVector. Maybe GetOrthogonal is what we need?
		leftFootCoGToBodyCoG = leftFootCoG - bodyCoG;
		rightFootCoGToBodyCoG = rightFootCoG - bodyCoG;

		//betweenFeetVector = getBetweenFeetVector(leftFoot, rightFoot);
		feetDistance = betweenFeetVector.magnitude;

		//leftFootCollider = leftFoot.GetComponent<Collider>();
		/*print ("Left foot's CoG: " + leftFootCoG);
        print ("Right foot's CoG: " + rightFootCoG);
        print ("Vector between feet: " + betweenFeetVector);
        print ("Distance between feet: " + feetDistance);
 
        print ("Torso's CoG: " + bodyCoG);
        print ("Vector between feetVector and bodyCoG: " + bodyCoGToFeetVector);
        print ("Distance between feetVector and bodyCoG: " + bodyCoGToFeetVector.magnitude);
 
        print ("Is body's CoG inside the feet's support polygon?: " + IsBodySupported());*/

		if (fScript.IsReached ()) {
			isWalking = false;
			print ("isWalking is false");
		} else {
			print ("isWalking is true");
		}

		if (IsBodySupported ()) {
			//use balancing script in PIDControl/skeletonMuscles
		} else {

			if (!isWalking) {
				//print ("Not supported, moving ");
				if (leftFootCoGToBodyCoG.magnitude > rightFootCoGToBodyCoG.magnitude) {
					//print ("leftFoot to ");
					TakeStep (leftFoot, rightFoot);
				} else {
					//print ("rightFoot to ");
					TakeStep (rightFoot, leftFoot);
				}
			}
		}

		/*leftHamstring.MoveMuscle (forceHamstring);
        rightHamstring.MoveMuscle (forceHamstring);
        leftGluteus.MoveMuscle (forceGluteus);
        rightGluteus.MoveMuscle (forceGluteus);
        leftHip.MoveMuscle (forceHip);
        rightHip.MoveMuscle (forceHip);
        abs.MoveMuscle (forceAbs);
        back.MoveMuscle (forceBack);*/
	}


	void OnCollisionStay(Collision collisionInfo) {
		foreach (ContactPoint contact in collisionInfo.contacts) {
			print (contact.point);
		}
	}


	Vector3 GetBetweenFeetVector(Rigidbody foot1, Rigidbody foot2){
		Vector3 feetVector = GetCenterOfGravity(foot1) - GetCenterOfGravity(foot2);
		return feetVector;
	}

	Vector3 GetCenterOfGravity(Rigidbody rb){
		Vector3 tempCoG = rb.transform.TransformPoint (rb.centerOfMass);
		//Vector3 tempCoG = rb.centerOfMass;
		//print ("CoG of " + rb + " is " + tempCoG);
		return new Vector3 (tempCoG[0], 0, tempCoG[2]); //Get twodimensional vector (X and Z coordinates)
	}

	Vector3 GetOrthogonalProjection(Vector3 y, Vector3 u){
		Vector3 projection = (Vector3.Dot(y, u)/Vector3.Dot(u, u))*u; //Equation for y's projection on u
		return projection;
	}

	/* Is the body's center of gravity inside the feet's support polygon?*/
	bool IsBodySupported(){

		//if (Mathf.Abs(bodyCoGToFeetVector.magnitude) <= betweenFeetVectorToBodyCoGEpsilon){
		if(UnityEditor.HandleUtility.DistancePointLine(bodyCoG, GetCenterOfGravity(leftFoot), GetCenterOfGravity(rightFoot)) <=
			betweenFeetVectorToBodyCoGEpsilon){
			return true;
		} else {
			return false;
		}
	}

	//If body's center of gravity is outside the support polygon, calculate line between bodyCoG and closest foot CoG. Then place the foot furthest away on this line.
	Vector3 TakeStep(Rigidbody moveFoot, Rigidbody stayFoot){

		isWalking = true;
		//Vector3 goalPos = GetOrthogonalProjection(GetCenterOfGravity(moveFoot), (GetCenterOfGravity(stayFoot) - bodyCoG));  
		Vector3 goalPos = bodyCoG + (bodyCoG - GetCenterOfGravity(stayFoot))/2;
		Vector3 initialFootPos = GetCenterOfGravity (moveFoot);

		//print ("goalPos: " + goalPos);
		gizmoFromFoot = stayFoot.transform.position;
		gizmoToFoot = goalPos;
		gizmoBodyCoG = bodyCoG;
		//moveFoot.transform.position = new Vector3(goalPos[0], 0f, goalPos[2]); //instantly move foot to just above goalPos, make more realistic later

		FootGoalColliderScript fScript = (FootGoalColliderScript) footGoalMarker.gameObject.GetComponent(typeof(FootGoalColliderScript));

		footGoalMarker.transform.position = new Vector3(goalPos[0], 0.5f, goalPos[2]);
		moveFoot.transform.position = new Vector3(goalPos[0], 0.5f, goalPos[2]);
		fScript.CreateJoint (moveFoot);
		moveFoot.transform.position = initialFootPos;
		fScript.Setup (true); //indicates that setup of joint is done
		print("Now foot goal setup is true");




		//moveFoot.gameObject.AddComponent<SpringJoint> ();
		//moveFoot.gameObject.GetComponent<SpringJoint> ().connectedBody = footGoalPos;

		if (fScript.IsReached()) {
			isWalking = false;
			print ("isWalking is false");
		}
		return goalPos;

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawLine (leftFootCoG, rightFootCoG);
		Gizmos.DrawSphere(bodyCoG, 0.1f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (Vector3.zero, bodyCoG);

		if (bodyCoGToFeetVector != null) {
			Gizmos.color = Color.cyan;
			if (leftFootCoGToBodyCoG.magnitude > rightFootCoGToBodyCoG.magnitude) {
				Gizmos.DrawLine (leftFootCoG, GetOrthogonalProjection (rightFootCoG, GetOrthogonalProjection (bodyCoG, betweenFeetVector)));
			} else {
				Gizmos.DrawLine (rightFootCoG, GetOrthogonalProjection (leftFootCoG, GetOrthogonalProjection (bodyCoG, betweenFeetVector)));
			}
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine (bodyCoG, GetOrthogonalProjection(bodyCoG, betweenFeetVector));

		}

		if (gizmoFromFoot != null && gizmoBodyCoG != null && gizmoToFoot != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (new Vector3(gizmoFromFoot[0],0,gizmoFromFoot[2]), gizmoBodyCoG);
			Gizmos.color = Color.white;
			Gizmos.DrawLine (gizmoBodyCoG, GetOrthogonalProjection(gizmoToFoot, 1*(gizmoFromFoot + gizmoBodyCoG)+bodyCoG));
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(gizmoToFoot, 0.1f);
			//Gizmos.DrawLine (gizmoBodyCoG, gizmoToFoot);
		}

	}

	void OnCollisionEnter(Collision coll){
		print ("Entered OnCollisionEnter");
		if (coll.contacts[0].thisCollider.gameObject.CompareTag("Plane")){
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