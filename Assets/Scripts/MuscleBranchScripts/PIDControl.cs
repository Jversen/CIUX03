using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDControl : MonoBehaviour {

	private float previousInValX;
	private float previousInValZ;
	private float goalX = 0;
	private float goalZ = 0;
	private float integralX;
	private float integralZ;

	private float kp = 3000f;
	private float kd = 60f;
	private float ki = 160f;

	private float maxDerivativeX = 400f;
	private float maxDerivativeZ = 400f;
	private float minDerivativeX = -40f;
	private float minDerivativeZ = -40f;

	public static float maxIntegralX = -100f;
	public static float maxIntegralZ = -100f;
	public static float minIntegralX = 60f;
	public static float minIntegralZ = 60f;



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
		integralX = 0;
		integralZ = 0;

		legWestMuscle = new Muscle(legWestMusclePoint, footWestMusclePoint);
		legEastMuscle = new Muscle(legEastMusclePoint, footEastMusclePoint);
		legSouthMuscle = new Muscle(legSouthMusclePoint, footSouthMusclePoint);
		legNorthMuscle = new Muscle(legNorthMusclePoint, footNorthMusclePoint);
	}

	// Update is called once per frame
	void Update () {
		Vector3 rotation = transform.localEulerAngles;

		inValX = rotation.x;
		inValX = (Mathf.PI * inValX) / 180.0f;

		inValZ = rotation.z;
		inValZ = (Mathf.PI * inValZ) / 180.0f;


		//TODO: Think about positive/negative derivative and their effects.
		float derivativeX = (inValX - previousInValX) / Time.deltaTime;
		float derivativeZ = (inValZ - previousInValZ) / Time.deltaTime;

		if (inValX > Mathf.PI) {
			inValX -= (2 * Mathf.PI);
		}
		if (inValZ > Mathf.PI) {
			inValZ -= (2 * Mathf.PI);
		}



		//errorX = (invalX + (2 * Mathf.PI) - goalX) % (2 * Mathf.PI)
		float errorX = inValX;
		//errorZ = (invalZ + (2 * Mathf.PI) - goalZ) % (2 * Mathf.PI)
		float errorZ = inValZ;

		integralX += errorX;
		integralZ += errorZ;

		derivativeX = (derivativeX - minDerivativeX) / (maxDerivativeX - minDerivativeX);
		derivativeZ = (derivativeZ - minDerivativeZ) / (maxDerivativeZ - minDerivativeZ);

		integralX = (integralX - minIntegralX) / (maxIntegralX - minIntegralX);
		integralZ = (integralZ - minIntegralZ) / (maxIntegralZ - minIntegralZ);

		float outValX = Mathf.Sin (errorX / 2f) * kp - derivativeX * kd + integralX * ki;
		float outValZ = Mathf.Sin (errorZ/ 2f) * kp - derivativeZ * kd + integralX * ki;

		//print ("errorX = " + errorX);
		if (outValX > 0) {
			//positive X -> contract west
			legWestMuscle.Contract(outValX*0.5f);
		} else {
			outValX = Mathf.Abs (outValX); //Negative outVal determines which muscles to contract but contraction force is always positive.
			//negative X -> contract east
			legEastMuscle.Contract(outValX*0.5f);
		}
		//print ("errorZ = " + errorZ);
		if (outValZ > 0) {
			//positive Z -> contract south
			legSouthMuscle.Contract(outValZ*0.5f);
		} else {
			outValZ = Mathf.Abs (outValZ); //Negative outVal determines which muscles to contract but contraction force is always positive.
			//negative Z -> contract north
			legNorthMuscle.Contract(outValZ*0.5f);
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
