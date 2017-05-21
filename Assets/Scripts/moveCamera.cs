using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    
    private GameObject player;
	
	float offset = 20;

	Camera camera;

public int playersRoomId()
    {
        
        player = GameObject.FindGameObjectWithTag("Player1");
        foreach(GameObject room in GameObject.FindGameObjectsWithTag("room"))
        {
            Vector2 roomDims = maxRoom(room);
            if (Mathf.Abs(player.transform.x - room.transform.x) < (roomDims.x / 2) &&
                Mathf.Abs(player.transform.y - room.transform.y) < (roomDims.y / 2))
                return roomId;
        }
    }


	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player1");
        camera = GetComponent<Camera> ();
        focusOnRoom();
    }

	// Update is called once per frame
	void FixedUpdate () {
        //focus on centre of that room
        if (!isInScope(player)){
            GameObject newRoom = 
            moveToNextRoom(newRoom);
        }
	}

	bool isInScope(GameObject obj){
		Vector3 screenPoint = camera.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
		return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}


	void focusOnRoom(){
        GameObject room = GameObject.FindGameObjectsWithTag("Room")[0];
       
        Vector2 roomDims = maxRoom(room);
        this.transform.position = new Vector3 (room.transform.position.x, room.transform.position.y, -10);
        print(roomDims);
        camera.orthographicSize = roomDims.y/2;

        //set aspect wanted
        float targetaspect = 1.0f / 1.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        Rect rect = camera.rect;

        float scalewidth = 1.0f / scaleheight;
        rect.width = scalewidth;
        rect.height = 1.0f;
        rect.x = (1.0f - scalewidth) / 2;
        rect.y = 0;

        camera.rect = rect;

        //camera.aspect = (roomDims.x / roomDims.y);
	}

	public void moveToNextRoom(GameObject room){
        this.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
	}

    //get dimensions of room
	Vector2 maxRoom(GameObject Room) {
       Transform firstTile = Room.transform.GetChild(1);
        Transform secondTile = Room.transform.GetChild(2);
        print(firstTile.position.x);
        print(secondTile.position.x);
        float tileSize = Mathf.Abs(firstTile.position.x - secondTile.position.x); //only works for square tiles
        print (tileSize);
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
       float height = Mathf.Abs(ymax - ymin) + tileSize;
       float width = Mathf.Abs(xmax - xmin) + tileSize;
		return new Vector2 (width, height); //dimensions of room
	}
	

}



