using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

	public Rigidbody characterChest;
	public float forceConstant;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float translationDir = Input.GetAxis ("Vertical");
		float translation = translationDir * forceConstant;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		//Add force in Y-axis to compensate for rearing.
		characterChest.AddRelativeForce(0, (-1) * characterChest.transform.forward.y, translation);
		if (translationDir < 0) {
			//Multiply with translationDir to reverse rotation when moving backwards
			characterChest.AddTorque (0, rotation * translationDir, 0);
		} else {
			characterChest.AddTorque (0, rotation, 0);
		}		 
	}
}
