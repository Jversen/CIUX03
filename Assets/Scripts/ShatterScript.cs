using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterScript : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		//transform.Rotate (-90, 0, 0);
		Instantiate (prefab, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
