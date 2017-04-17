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
	public Muscle back; */
	public Rigidbody leftFoot, rightFoot;
	//public float forceHamstring, forceGluteus, forceHip, forceAbs, forceBack;
	private bool leftFootContact = false; 
	private bool rightFootContact = false;
	private Vector3 leftFootCoG, rightFootCoG, bodyCoG, betweenFeetVector, leftToBodyCoG, rightToBodyCoG, leftFootGoalCoG, rightFootGoalCoG;
	private float feetDistance, balanceLineDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		leftFootCoG = leftFoot.
		rightFootCoG
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
}
