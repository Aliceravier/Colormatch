using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onLeave : MonoBehaviour {
    GameObject camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onTriggerEnter2D(Collider2D c){
        GameObject nextRoom;
        camera.GetComponent<moveCamera>().moveToNextRoom(nextRoom);

	}
}
