using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDControl : MonoBehaviour {

	private float previousInValX;
	private float previousInValZ;
	private float goalX = 0;
	private float goalZ = 0;
	private float muscleForce = 7000f;


	private float inValX;
	private float inValZ;
	public Rigidbody legWestMusclePoint, footWestMusclePoint, legEastMusclePoint, footEastMusclePoint, 
	legSouthMusclePoint, footSouthMusclePoint, legNorthMusclePoint, footNorthMusclePoint;
	private Muscle legWestMuscle, legEastMuscle, legSouthMuscle, legNorthMuscle;

	// Use this for initialization
	void Start () {
		Vector3 rotation = transform.localEulerAngles;
		inValX = rotation.x;
		inValZ = rotation.z;
		previousInValX = inValX;
		previousInValZ = inValZ;

		legWestMuscle = new Muscle(legWestMusclePoint, footWestMusclePoint);
		legEastMuscle = new Muscle(legEastMusclePoint, footEastMusclePoint);
		legSouthMuscle = new Muscle(legSouthMusclePoint, footSouthMusclePoint);
		legNorthMuscle = new Muscle(legNorthMusclePoint, footNorthMusclePoint);
	}

	// Update is called once per frame
	void Update () {
		Vector3 rotation = transform.localEulerAngles;
		print ("rotation = " + rotation.ToString ());
		inValX = rotation.x;
		print ("inValX = " + inValX);
		inValZ = rotation.z;
		//print ("inValZ = " + inValZ);


		//TODO: Think about positive/negative derivative and their effects.
		float derivativeX = (inValX - previousInValX) / Time.deltaTime;
		float derivativeZ = (inValZ - previousInValZ) / Time.deltaTime;

		if (inValX > 180) {
			inValX -= 360;
		}
		if (inValZ > 180) {
			inValZ -= 360;
		}

		//errorX = (invalX + 360 - goalX) % 360
		float errorX = inValX;
		print ("errorX = " + errorX);
		//errorZ = (invalZ + 360 - goalZ) % 360
		float errorZ = inValZ;

		float errorXInRadians = (Mathf.PI * errorX) / 180.0f;
		print ("errorXInRadians = " + errorXInRadians);
		float outValX = Mathf.Sin (errorXInRadians / 2f) * muscleForce;// - derivativeZ / 10f;
		print ("outValX = " + outValX);

		float errorZInRadians = (Mathf.PI * errorZ) / 180.0f;
		float outValZ = Mathf.Sin (errorZInRadians / 2f) * muscleForce;// - derivativeZ / 10f;

		//print ("errorX = " + errorX);
		if (outValX > 0) {
			//positive X -> contract west
			legWestMuscle.Contract(outValX);
		} else {
			outValX = Mathf.Abs (outValX); //Negative outVal determines which muscles to contract but contraction force is always positive.
			//negative X -> contract east
			legEastMuscle.Contract(outValX);
		}
		//print ("errorZ = " + errorZ);
		if (outValZ > 0) {
			//positive Z -> contract south
			legSouthMuscle.Contract(outValZ);
		} else {
			outValZ = Mathf.Abs (outValZ); //Negative outVal determines which muscles to contract but contraction force is always positive.
			//negative Z -> contract north
			legNorthMuscle.Contract(outValZ);
		}

		previousInValX = inValX;
		previousInValZ = inValZ;

	}

	public class Muscle{
		private Rigidbody upper, lower;

		public Muscle(Rigidbody upper, Rigidbody lower) {
			this.upper = upper;
			this.lower = lower;
		}

		//Seems to work if force is within the range [1000 - 9000]. Depends on masses of the body parts.
		public void Contract(float force) {
			Vector3 direction1 = upper.transform.position - lower.transform.position;
			direction1.Normalize();
			direction1 = direction1 * force;
			Vector3 direction2 = -direction1;
			upper.AddForce(direction2 * Time.deltaTime, ForceMode.Impulse);
			lower.AddForce(direction1 * Time.deltaTime, ForceMode.Impulse);
		}
	}
}
