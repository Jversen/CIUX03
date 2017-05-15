using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicLegs : MonoBehaviour {

	public GameObject leftThigh;
	public GameObject rightThigh;
	public GameObject leftLeg;
	public GameObject rightLeg;
	public GameObject leftFoot;
	public GameObject rightFoot;

	private Vector3 thighBackPosition = new Vector3(0.47f, 3.31f, 0f);
	private float thighBackRotationZ = 32;
	private Vector3 legBackPosition = new Vector3(1.68f, 1.62f, 0f);
	private float legBackRotationZ = 39.5f;
	private Vector3 footBackPosition = new Vector3(2.44f, 0.61f, 0f);
	private float footBackRotationZ = 27.3f;

	private Vector3 thighMidBackPosition = new Vector3(-0.75f, 3.57f, 0f); //FROM BACK TO THIS
	private float thighMidBackRotationZ = -54;
	private Vector3 legMidBackPosition = new Vector3(-0.78f, 2.31f, 0f);
	private float legMidBackRotationZ = 60f;
	private Vector3 footMidBackPosition = new Vector3(0.2f, 1.66f, 0f);
	private float footMidBackRotationZ = 60f;

	private Vector3 thighMidFrontPosition = new Vector3(0f, 3.24f, 0f); //FROM FRONT TO THIS
	private float thighMidFrontRotationZ = -7;
	private Vector3 legMidFrontPosition = new Vector3(0f, 1.34f, 0f);
	private float legMidFrontRotationZ = 7f;
	private Vector3 footMidFrontPosition = new Vector3(0.1f, 0.3f, 0f);
	private float footMidFrontRotationZ = 12.3f;

	private Vector3 thighFrontPosition = new Vector3(-0.56f, 3.48f, 0f);
	private float thighFrontRotationZ = -45;
	private Vector3 legFrontPosition = new Vector3(-1.51f, 1.72f, 0f);
	private float legFrontRotationZ = -15f;
	private Vector3 footFrontPosition = new Vector3(-1.86f, 0.51f, 0f);
	private float footFrontRotationZ = -12f;

	private Vector3 thighNeutralPosition = new Vector3(0f, 3.25f, 0f);
	private Vector3 legNeutralPosition = new Vector3(0f, 1.2f, 0f);
	private Vector3 footNeutralPosition = new Vector3(0f, 0f, 0f);
	private float neutralRotationZ = 0;

	private Vector3 rightThighStoppingPosition;
	private float rightThighStoppingRotationZ;
	private Vector3 rightLegStoppingPosition;
	private float rightLegStoppingRotationZ;
	private Vector3 rightFootStoppingPosition;
	private float rightFootStoppingRotationZ;
	private Vector3 leftThighStoppingPosition;
	private float leftThighStoppingRotationZ;
	private Vector3 leftLegStoppingPosition;
	private float leftLegStoppingRotationZ;
	private Vector3 leftFootStoppingPosition;
	private float leftFootStoppingRotationZ;

	private Vector3[] thighPositions;
	private Vector3[] legPositions;
	private Vector3[] footPositions;
	private float[] thighRotations;
	private float[] legRotations;
	private float[] footRotations;

	private int i = 0;
	public bool startRunning = false;
	public bool running = false;
	public bool stopRunning = false;
	public bool stopping = false;
	private int percentagePerFrame = 10;

	private int leftLegIndex;
	private int rightLegIndex;
	private int leftLegNextIndex;
	private int rightLegNextIndex;

	void Start () {
		thighPositions = new Vector3[]{ thighBackPosition, thighMidBackPosition, thighFrontPosition, thighMidFrontPosition };
		legPositions = new Vector3[]{ legBackPosition, legMidBackPosition, legFrontPosition, legMidFrontPosition };
		footPositions = new Vector3[]{ footBackPosition, footMidBackPosition, footFrontPosition, footMidFrontPosition };
		thighRotations = new float[]{ thighBackRotationZ, thighMidBackRotationZ, thighFrontRotationZ, thighMidFrontRotationZ };
		legRotations = new float[]{ legBackRotationZ, legMidBackRotationZ, legFrontRotationZ, legMidFrontRotationZ };
		footRotations = new float[]{ footBackRotationZ, footMidBackRotationZ, footFrontRotationZ, footMidFrontRotationZ };
	}

	void Update () {

		if(Input.GetKey("s")){
			startRunning = true;
		}
		if(Input.GetKey("p")){
			stopRunning = true;
		}

		if (startRunning) {
			int leftLegNextIndex = 0;
			int rightLegNextIndex = 2;
			InterpolatePosition (leftThigh.transform, thighNeutralPosition, thighPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftThigh.transform, neutralRotationZ, thighRotations [leftLegNextIndex], i % 100);
			InterpolatePosition (leftLeg.transform, legNeutralPosition, legPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftLeg.transform, neutralRotationZ, legRotations [leftLegNextIndex], i % 100);
			InterpolatePosition (leftFoot.transform, footNeutralPosition, footPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftFoot.transform, neutralRotationZ, footRotations [leftLegNextIndex], i % 100);

			InterpolatePosition (rightThigh.transform, thighNeutralPosition, thighPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightThigh.transform, neutralRotationZ, thighRotations [rightLegNextIndex], i % 100);
			InterpolatePosition (rightLeg.transform, legNeutralPosition, legPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightLeg.transform, neutralRotationZ, legRotations [rightLegNextIndex], i % 100);
			InterpolatePosition (rightFoot.transform, footNeutralPosition, footPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightFoot.transform, neutralRotationZ, footRotations [rightLegNextIndex], i % 100);

			i += percentagePerFrame;

			if (i >= 100) {
				i = 0;
				startRunning = false;
				running = true;
			}
		} else if (stopRunning){
			rightThighStoppingPosition = rightThigh.transform.localPosition;
			rightThighStoppingRotationZ = rightThigh.transform.localEulerAngles.z;
			rightLegStoppingPosition = rightLeg.transform.localPosition;
			rightLegStoppingRotationZ = rightLeg.transform.localEulerAngles.z;
			rightFootStoppingPosition = rightFoot.transform.localPosition;
			rightFootStoppingRotationZ = rightFoot.transform.localEulerAngles.z;
			leftThighStoppingPosition = leftThigh.transform.localPosition;
			leftThighStoppingRotationZ = leftThigh.transform.localEulerAngles.z;
			leftLegStoppingPosition = leftLeg.transform.localPosition;
			leftLegStoppingRotationZ = leftLeg.transform.localEulerAngles.z;
			leftFootStoppingPosition = leftFoot.transform.localPosition;
			leftFootStoppingRotationZ = leftFoot.transform.localEulerAngles.z;
			running = false;
			stopRunning = false;
			stopping = true;
			i = 0;

		} else if (running) {

			leftLegIndex = i / 100;
			leftLegNextIndex = (leftLegIndex + 1) % legPositions.Length;
			rightLegIndex = (leftLegIndex + 2) % legPositions.Length;
			rightLegNextIndex = (rightLegIndex + 1) % legPositions.Length;


			InterpolatePosition (leftThigh.transform, thighPositions [leftLegIndex], thighPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftThigh.transform, thighRotations [leftLegIndex], thighRotations [leftLegNextIndex], i % 100);
			InterpolatePosition (leftLeg.transform, legPositions [leftLegIndex], legPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftLeg.transform, legRotations [leftLegIndex], legRotations [leftLegNextIndex], i % 100);
			InterpolatePosition (leftFoot.transform, footPositions [leftLegIndex], footPositions [leftLegNextIndex], i % 100);
			InterpolateRotation (leftFoot.transform, footRotations [leftLegIndex], footRotations [leftLegNextIndex], i % 100);

			InterpolatePosition (rightThigh.transform, thighPositions [rightLegIndex], thighPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightThigh.transform, thighRotations [rightLegIndex], thighRotations [rightLegNextIndex], i % 100);
			InterpolatePosition (rightLeg.transform, legPositions [rightLegIndex], legPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightLeg.transform, legRotations [rightLegIndex], legRotations [rightLegNextIndex], i % 100);
			InterpolatePosition (rightFoot.transform, footPositions [rightLegIndex], footPositions [rightLegNextIndex], i % 100);
			InterpolateRotation (rightFoot.transform, footRotations [rightLegIndex], footRotations [rightLegNextIndex], i % 100);

			i += percentagePerFrame;

			if (i >= thighPositions.Length * 100) {
				i = 0;
			}
		} else if (stopping) {

			InterpolatePosition (leftThigh.transform, leftThighStoppingPosition, thighNeutralPosition, i);
			InterpolateRotation (leftThigh.transform, leftThighStoppingRotationZ, 0f, i);
			InterpolatePosition (leftLeg.transform, leftLegStoppingPosition, legNeutralPosition, i);
			InterpolateRotation (leftLeg.transform, leftLegStoppingRotationZ, 0f, i);
			InterpolatePosition (leftFoot.transform, leftFootStoppingPosition, footNeutralPosition, i);
			InterpolateRotation (leftFoot.transform, leftFootStoppingRotationZ, 0f, i);

			InterpolatePosition (rightThigh.transform, rightThighStoppingPosition, thighNeutralPosition, i);
			InterpolateRotation (rightThigh.transform, rightThighStoppingRotationZ, 0f, i);
			InterpolatePosition (rightLeg.transform, rightLegStoppingPosition, legNeutralPosition, i);
			InterpolateRotation (rightLeg.transform, rightLegStoppingRotationZ, 0f, i);
			InterpolatePosition (rightFoot.transform, rightFootStoppingPosition, footNeutralPosition, i);
			InterpolateRotation (rightFoot.transform, rightFootStoppingRotationZ, 0f, i);

			i += percentagePerFrame;

			if (i > 100) {
				stopping = false;
				i = 0;
			}
		}
	}

	public void InterpolatePosition(Transform transform, Vector3 startingPos, Vector3 goalPos, int percentage){
		float currentZ = transform.localPosition.z;
		Vector3 newPosition = startingPos * (1f - 0.01f * percentage) + goalPos * (0.01f * percentage);
		newPosition.z = currentZ;
		transform.localPosition = newPosition;
	}


	public void InterpolateRotation(Transform transform, float startingRotZ, float goalRotZ, int percentage){
		float baseRotation = MakeNegativeAngle(startingRotZ);
		float otherRotation = MakeNegativeAngle(goalRotZ);
		float newRotZ = baseRotation * (1f - (0.01f * percentage)) + otherRotation * (0.01f * percentage);
		Vector3 interPolatedRotation = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, newRotZ);
		transform.localEulerAngles = interPolatedRotation;
	}

	public Vector3 MakeNagativeAngleVector(Vector3 rotation){
		float x = rotation.x;
		float y = rotation.y;
		float z = rotation.z;
		return new Vector3 (MakeNegativeAngle (x), MakeNegativeAngle (y), MakeNegativeAngle (z));
	}

	public float MakeNegativeAngle(float angle){
		if (angle > 180f) {
			angle -= 360f;
		}
		return angle;
	}
}
