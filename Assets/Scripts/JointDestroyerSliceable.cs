using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NobleMuffins.TurboSlicer;

public class JointDestroyerSliceable : AbstractSliceHandler {

	public GameObject connector;

	public override void handleSlice( GameObject[] results ) {

		//Results of the slice
		GameObject alfaObject = results [0];
		GameObject bravoObject = results [1];

		Vector3 alfaPosition = alfaObject.transform.position + alfaObject.GetComponent<Rigidbody> ().centerOfMass;
		Vector3 bravoPosition = bravoObject.transform.position + bravoObject.GetComponent<Rigidbody> ().centerOfMass;

		//Make connector connect to closest of alfa and bravo. Set connector of other to null.
		if (connector != null) {
			Vector3 connectorBodyPosition = connector.transform.position;
			Vector3 alfaToConnector = alfaPosition - connectorBodyPosition;
			Vector3 bravoToConnector = bravoPosition - connectorBodyPosition;
			GameObject closestToConnector = null;
			GameObject furthestFromConnector = null;
			if (alfaToConnector.magnitude < bravoToConnector.magnitude) {
				closestToConnector = alfaObject;
				furthestFromConnector = bravoObject;
			} else {
				closestToConnector = bravoObject;
				furthestFromConnector = alfaObject;
			}
			Joint connection = (Joint)connector.GetComponent<Joint> ();
			connection.connectedBody = closestToConnector.GetComponent<Rigidbody> ();
			furthestFromConnector.GetComponent<JointDestroyerSliceable> ().connector = null;
		}

		//Remove joint of furthest.
		Joint joint = alfaObject.GetComponent<Joint> (); //TODO Destroy joint if has one joint but no ConnectorHolder!
		if (joint != null) {
			Vector3 connectedBodyPosition = joint.connectedBody.transform.position;

			Vector3 alfaToConnected = alfaPosition - connectedBodyPosition;
			Vector3 bravoToConnected = bravoPosition - connectedBodyPosition;

			GameObject furthestFromConnected = null;

			if (alfaToConnected.magnitude < bravoToConnected.magnitude) {
				furthestFromConnected = bravoObject;
			} else {
				furthestFromConnected = alfaObject;
			}
			Destroy (furthestFromConnected.GetComponent<Joint> ());
		}
	} 
}
