using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceIntelligenceNew : MonoBehaviour {


	public Rigidbody leftLowerHip, leftUpperHip, rightLowerHip, rightUpperHip;
	public Muscle leftHip, rightHip;
	public Rigidbody leftFoot, rightFoot, bodyCore;
	public Rigidbody footGoalMarker;
	private FootGoalColliderScript fScript;
	private Vector3 bodyCoGToFeetVector, leftFootCoGToBodyCoG, rightFootCoGToBodyCoG, bodyCoG, betweenFeetVector, leftFootCoG, rightFootCoG;
	private float feetDistance;
	private bool isWalking;
	public float allowedCogToBalanceLineDist; //Allowed difference betwen cog and balance line. Smal epsilon => small steps.
	//public float muscleForce;
	//public float forceHamstring, forceGluteus, forceHip, forceAbs, forceBack;
	//private bool leftFootContact = false; 
	//private bool rightFootContact = false;
	//private Vector3 leftFootGoalCoG, rightFootGoalCoG;
	//balanceLineDistance, bodyCoGToFeetDistance;

	// Use this for initialization
	void Start () {
		if (footGoalMarker != null) {
			fScript = footGoalMarker.gameObject.GetComponent<FootGoalColliderScript> ();
		} else {
			print ("footGoalMarker is null!");
		}
		leftHip = new Muscle (leftUpperHip, leftLowerHip);
		rightHip = new Muscle (rightUpperHip, rightLowerHip);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		betweenFeetVector = GetBetweenVector (leftFoot, rightFoot);

		leftFootCoG = GetCenterOfGravity (leftFoot);
		rightFootCoG = GetCenterOfGravity (rightFoot);
		betweenFeetVector = leftFootCoG - rightFootCoG;

		bodyCoG = GetCenterOfGravity (bodyCore);

		//distance y - ŷ, bodyCoG vector minus its projection on betweenFeetVector. Maybe GetOrthogonal is what we need?
		bodyCoGToFeetVector = bodyCoG - Vector3.Project(bodyCoG, betweenFeetVector); 
		leftFootCoGToBodyCoG = leftFootCoG - bodyCoG;
		rightFootCoGToBodyCoG = rightFootCoG - bodyCoG;

		feetDistance = betweenFeetVector.magnitude;

		if (fScript.IsReached ()) {
			isWalking = false;
		}

		if (IsBodySupported ()) {
			//use balancing script in PIDControl/skeletonMuscles
			//TODO: Insert eventual behaviour here
		} else if (!isWalking) {
			//Move foot furthest from center of gravity.
			if (leftFootCoGToBodyCoG.magnitude > rightFootCoGToBodyCoG.magnitude) {
				TakeStep (leftFoot, rightFoot);
			} else {
				TakeStep (rightFoot, leftFoot);
			}
		}
	}

	Vector3 GetBetweenVector(Rigidbody body1, Rigidbody body2){
		Vector3 feetVector = GetCenterOfGravity(body1) - GetCenterOfGravity(body2);
		return feetVector;
	}

	Vector3 GetCenterOfGravity(Rigidbody rb){
		Vector3 worldCoG = rb.transform.TransformPoint (rb.centerOfMass); //get world coordinates of local center of mass
		return new Vector3 (worldCoG.x, 0, worldCoG.z); //Get twodimensional vector (X and Z coordinates)
	}

	//If body's center of gravity is outside the support polygon, calculate line between bodyCoG and closest foot CoG. Then place the foot furthest away on this line.
	Vector3 TakeStep(Rigidbody moveFoot, Rigidbody stayFoot){

		isWalking = true;
		Vector3 goalPos = bodyCoG + (bodyCoG - GetCenterOfGravity(stayFoot))/2; //Place feet at half distance to goal in the air (half step).
		Vector3 initialFootPos = GetCenterOfGravity (moveFoot);

		//Move foot to goal, create spring and move it back. This creates spring with restlengh zero between foot and goal.
		footGoalMarker.transform.position = new Vector3(goalPos.x, 0.5f, goalPos.z); 
		moveFoot.transform.position = footGoalMarker.transform.position;
		fScript.CreateJoint (moveFoot);
		moveFoot.transform.position = initialFootPos;
		fScript.Setup (true); //indicates that setup of joint is done

		if (fScript.IsReached()) {
			isWalking = false;
		}
		return goalPos;

	}

	/* Is the body's center of gravity inside the feet's support polygon?*/
	bool IsBodySupported(){

		Vector3 balanceLineStart = GetCenterOfGravity (leftFoot);
		Vector3 balanceLineEnd = GetCenterOfGravity (rightFoot);
		float cogToBalanceLineDist = UnityEditor.HandleUtility.DistancePointLine (bodyCoG, balanceLineStart, balanceLineEnd);
		print ("cogToBalanceLineDist = " + cogToBalanceLineDist);
		if(cogToBalanceLineDist <= allowedCogToBalanceLineDist){
			print("Body is supported!");
			return true; //body is supported.
		} else {
			return false; //body is not supported.
		}
	}
}
