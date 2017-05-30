using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	GameObject[] rooms;
    RoomManager rm;
	// Use this for initialization
	void Start () {
		rooms = GameObject.FindGameObjectsWithTag ("Room");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool isGameWon(GameObject[] rooms){
		foreach (GameObject room in rooms){


		}
		return false;
	}

    public bool bothInSameRoom()
    {
        foreach (GameObject room in rooms)
        {
            rm = room.GetComponent<RoomManager>();
            if (rm.bothInThisRoom())
                return true;
        }
        return false;
    }
}
