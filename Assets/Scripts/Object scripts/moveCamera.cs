using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class moveCamera : ExtendedBehaviour {

    [SerializeField]
	Team playerTeam = Team.neutral;
    private GameObject player;
    GameObject[] rooms;
    public bool isLeaving = false;

    [HideInInspector]
    public GameObject activeRoom;

    RoomManager rm;
    Camera cam;
    public float cameraSpeed = 4f;

	// Use this for initialization
	public void Awake() {
        
        rooms = GameObject.FindGameObjectsWithTag("Room");
        player = getPlayerOfTeam (playerTeam);
        cam = GetComponent<Camera> ();
		setAspectRatio (playerTeam);
        rm = GameObject.FindGameObjectWithTag("God").GetComponent<RoomManager>();
    }

    void LateUpdate()
    {
        print("the player is: " + player);
        Debug.Log("the current room is: "+player.GetComponent<PlayerStatistics>().currentRoom);
        activeRoom = player.GetComponent<PlayerStatistics>().currentRoom;
        focusOnRoom(activeRoom);
    }

	// Update is called once per frame
	void FixedUpdate () {

	}

	public bool isInScope(GameObject obj){ //not using now
		Vector3 screenPoint = cam.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
		return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}


	public void focusOnRoom(GameObject room){
        Vector2 midPoint = room.GetComponent<RoomBehaviour>().getCentre();
        
		Vector2 roomDims = roomSize(room);
		cam.orthographicSize = roomDims.y/2;
        Vector3 newRoom = new Vector3(midPoint.x, midPoint.y, -10);
        transform.position = Vector3.Lerp(transform.position, newRoom, Time.fixedDeltaTime * cameraSpeed);
    }

	void setAspectRatio(Team team){
		//set aspect wanted
		float targetaspect = 1.0f / 1.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (((float)Screen.width / (float)Screen.height))/2;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = cam.rect;

			rect.width = 1.0f/2;
			rect.height = scaleheight;

			if (team == Team.blue)
				rect.x = 0;
			else
				rect.x = 0.5f;
			
			rect.y = (1.0f - scaleheight) / 2.0f;

			cam.rect = rect;
		}
		else // else add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = cam.rect;

			rect.width = scalewidth/2;
			rect.height = 1.0f;
			if (team == Team.blue)
				rect.x = (1.0f - scalewidth) / 2.0f;
			else
				rect.x = (1.0f - scalewidth) / 2.0f + 0.5f;
			rect.y = 0;

			cam.rect = rect;
		}

	}

    //get dimensions of room
	public Vector2 roomSize(GameObject room) {
		return room.GetComponent<RoomBehaviour>().roomSize;
	}
	
}



