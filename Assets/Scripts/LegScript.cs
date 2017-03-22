using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LegScript : MonoBehaviour {


	public Rigidbody head;
	public Rigidbody thigh;
	public Rigidbody knee;
	public Rigidbody shin;
	public Rigidbody ankle;
	public Rigidbody foot;
	public Rigidbody toe;

	public Transform parentObject;
	private float parentRotX;
	private float parentRotY;
	private float parentRotZ;

	private Vector3 vectorX;
	private Vector3 vectorY;
	private Vector3 vectorZ;

	private float g;
	private float mh;
	private float mt;
	private float ms;
	private float mf;

	// Use this for initialization
	void Start () {
		vectorX = new Vector3 (1, 0, 0);
		vectorY = new Vector3 (0, 1, 0);
		vectorZ = new Vector3 (0, 0, 1);

		parentRotX = parentObject.rotation.eulerAngles.x;
		parentRotY = parentObject.rotation.eulerAngles.y;
		parentRotZ = parentObject.rotation.eulerAngles.z;

		g = Physics.gravity.y;

		mh = head.mass / 4;
		mt = thigh.mass;
		ms = shin.mass;
		mf = foot.mass;

		print ("parentRotX: " + parentRotX + ", parentRotY: " + parentRotY + ", parentRotZ: " + parentRotZ );
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float thighRotX = thigh.rotation.eulerAngles.x - parentRotX;
		float thighRotY = thigh.rotation.eulerAngles.y - parentRotY;
		float thighRotZ = thigh.rotation.eulerAngles.z - parentRotZ;
		if (thighRotX > 180) {
			thighRotX = thighRotX - 360;
		}

		float shinRotX = shin.rotation.eulerAngles.x - parentRotX;
		float shinRotY = shin.rotation.eulerAngles.y - parentRotY;
		float shinRotZ = shin.rotation.eulerAngles.z - parentRotZ;
		if (shinRotX > 180) {
			shinRotX = shinRotX - 360;
		}

		float footRotX = foot.rotation.eulerAngles.x - parentRotX;
		float footRotY = foot.rotation.eulerAngles.y - parentRotY;
		float footRotZ = foot.rotation.eulerAngles.z - parentRotZ;
		if (footRotX > 180) {
			footRotX = footRotX - 360;
		}

		//Cos(90 - a) = Sin(a) 

		double alphaInRadians = Math.PI * (thighRotX / 180.0);
		float sinAlpha = (float) Math.Sin (alphaInRadians);
		float kneeForce = mh * g * sinAlpha;


		double betaInRadians = Math.PI * (shinRotX / 180.0);
		float sinBeta = (float) Math.Sin (betaInRadians);
		float ankleForce = (mh + mt) * g * sinBeta;


		double gammaInRadians = Math.PI * (footRotX / 180.0);
		float sinGamma = (float) Math.Sin (gammaInRadians);
		float toeForce = (mh + mt + ms) * g;

		knee.AddRelativeForce (new Vector3 (0, 0, kneeForce));
		ankle.AddRelativeForce (new Vector3 (0, 0, -ankleForce));
		toe.AddRelativeForce(new Vector3 (0, toeForce, 0));
	}
}
