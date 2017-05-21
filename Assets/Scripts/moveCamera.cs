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
        player = GameObject.FindGameObjectWithTag("Player1");
        camera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//find if player is in frame
		if (isInScope (player))
			print ("I'm HERE");
		else print("I'M not HERE");
		//focus on centre of that room

	}

	bool isInScope(GameObject obj){
		Vector3 screenPoint = camera.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
        print (screenPoint.x + " " + screenPoint.y);
		return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

	}



	void focusOnRoom(){
        GameObject room = GameObject.FindGameObjectsWithTag("Room")[0];
        Vector2 roomDims = maxRoom(room);
        this.transform.position = new Vector2 (room.transform.position.x, room.transform.position.y);
        //camera.pixelWidth = roomDims.x;
	}

	void moveToNextRoom(){


	}

    //get dimensions of room
	Vector2 maxRoom(GameObject Room) {
       Transform firstTile = Room.transform.GetChild(0);

        float xmax, xmin = firstTile.position.x;
        xmax = xmin;
        float ymax, ymin = firstTile.position.y;
        ymax = ymin;

       foreach (Transform tile in Room.transform)
        {
            float xtile = tile.position.x;
            float ytile = tile.position.y;

            if (xtile > xmax)
                xmax = xtile;
            if (xtile < xmin)
                xmin = xtile;
            if (ytile > ymax)
                ymax = ytile;
            if (ytile < ymin)
                ymin = ytile;      
        }
       float height = ymax - ymin;
       float width = xmax - xmin;
		return new Vector2 (width, height); //dimensions of room
	}
	

}



