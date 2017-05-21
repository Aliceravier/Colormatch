using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

	[SerializeField]
	GameObject player;
	[SerializeField]
	float offset = 20;

	Camera camera;


	// Use this for initialization
	void Awake () {
		camera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//find if player is in frame
		if (!isInScope (player))
			print ("Im NOT HERE");
		else print("IM HERE");


		//focus on centre of that room

	}

	bool isInScope(GameObject obj){
		Vector3 screenPoint = camera.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
		return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

	}



	bool focusOnRoom(){
		return true;

	}

	void moveToNextRoom(){


	}

	float maxRoom(GameObject Room){
		return 0.0f;
	}
	

}



