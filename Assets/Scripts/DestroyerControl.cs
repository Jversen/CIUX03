using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerControl : MonoBehaviour {

	private Rigidbody body;
	public float step;

	public string forward;
	public string backward;
	public string right;
	public string left;
	public string up;
	public string down;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3 (0, 0, 0);
		if (Input.GetKey (forward)) {
			move += transform.forward;
		}
		if (Input.GetKey (backward)) {
			move -= transform.forward;
		}
		if (Input.GetKey (left)) {
			move -= transform.right;
		}
		if (Input.GetKey (right)) {
			move += transform.right;
		}
		if (Input.GetKey (down)) {
			move -= transform.up;
		}
		if (Input.GetKey (up)) {
			move += transform.up;
		}
		transform.position += (step * move);
		//body.AddForce(step * move);
	}
}
