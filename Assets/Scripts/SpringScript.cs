using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour {

	public Rigidbody connector;

	// Update is called once per frame
	void OnMouseDown(){
		if (connector != null) {
			gameObject.AddComponent<SpringJoint> ();
			SpringJoint joint = gameObject.GetComponent<SpringJoint> ();
			Vector3 connectorPosition = connector.transform.position;
			connector.isKinematic = true;
			connector.gameObject.AddComponent<RemoveSpringScript> ();
			connector.gameObject.GetComponent<RemoveSpringScript> ().connector = gameObject;
			connector.transform.position = gameObject.transform.position;
			joint.enableCollision = true;
			joint.connectedBody = connector;
			connector.transform.position = connectorPosition;
		}
	}
}
