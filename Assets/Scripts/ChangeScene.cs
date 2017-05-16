using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("1")){
			SceneManager.LoadScene ("Scenes/DemoPID");
		} else if(Input.GetKey("2")){
			SceneManager.LoadScene ("Scenes/DemoMuscles");
		} else if(Input.GetKey("3")){
			SceneManager.LoadScene ("Scenes/MusclePIDScene");
		}else if(Input.GetKey("4")){
			SceneManager.LoadScene ("Scenes/Arena3");
		}else if(Input.GetKey("5")){
			SceneManager.LoadScene ("Scenes/DestructibleTestRange");
		}
	}
}
