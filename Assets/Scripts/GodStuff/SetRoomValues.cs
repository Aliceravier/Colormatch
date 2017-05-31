using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetRoomValues : MonoBehaviour {

    private class roomInfo
    {
        public Vector2 roomPosition;
        public GameObject room;

        //new class that has a vector2 and gameobject for position and room
        public roomInfo(Vector2 roomPosition, GameObject room)
        {
            this.roomPosition = roomPosition;
            this.room = room;
        }
    }

    int[] values = { 4, 3, 8, 9, 5, 1, 2, 7, 6 };

    GameObject[] orderedRooms()
    //gives an array of all the rooms in the order they are in the scene
    {
        GameObject[] orderedRooms = new GameObject[9];
        GameObject[] roomz = GameObject.FindGameObjectsWithTag("Room");
		List<GameObject> rooms = new List<GameObject> ();

		foreach (GameObject room in roomz) {
			if (room.GetComponentInChildren<ButtonBehaviour> () != null)
				rooms.Add (room);
		}

        Vector2[] roomPositions = new Vector2[rooms.Count];
        int i = 0;

        //makes array with positions
        foreach(GameObject room in rooms)
		{
			
				roomPositions [i] = room.transform.position;
				//debug works
				i++;
        }

        //combines positions and rooms in roomInfo array
		roomInfo[] roomPosAndGo = new roomInfo[rooms.Count];
        for (int j = 0; j < 9; j++)
        {
            roomPosAndGo[j] = new roomInfo(roomPositions[j], rooms[j]);
            //debug works
        }

        //makes variables for storing rooms into three lists, one for each row in the grid
        float y1 = Mathf.Round(getMaxY(roomPositions));
        List<roomInfo> highRow = new List<roomInfo>();
        List<roomInfo> middleRow = new List<roomInfo>(); 
        float y3 = Mathf.Round(getMinY(roomPositions));
        List<roomInfo> lowRow = new List<roomInfo>();
        

        //adds the rooms to the various lists based on their y value
        foreach(roomInfo roomInfo in roomPosAndGo) //debug works
        {	
            if (Mathf.Round(roomInfo.roomPosition[1]) == y1)
            {
                highRow.Add(roomInfo);
            }
            else if (Mathf.Round(roomInfo.roomPosition[1]) == y3)
            {
                lowRow.Add(roomInfo);
            }
            else
            {
                middleRow.Add(roomInfo);
            }
        }


        //makes variables to compare with x positions of rooms
        float x1 = Mathf.Round(getMinX(roomPositions));     
		float x3 = Mathf.Round(getMaxX(roomPositions));

        //places rooms from the highest row into the first places of the orderedRooms array
        foreach (roomInfo roomInfo in highRow)
        {
            if (Mathf.Round(roomInfo.roomPosition[0]) == x1) {
                orderedRooms[0] = roomInfo.room;
            }            
			else if (Mathf.Round(roomInfo.roomPosition[0]) == x3) {
                orderedRooms[2] = roomInfo.room;
            }            
			else { orderedRooms[1] = roomInfo.room;}
            }

        //places rooms from the middle row into the 3-5 places of the orderedRooms array
        foreach (roomInfo roomInfo in middleRow)
        {
            if (Mathf.Round(roomInfo.roomPosition[0]) == x1)
            {
                orderedRooms[3] = roomInfo.room;
            }
			else if (Mathf.Round(roomInfo.roomPosition[0]) == x3)
            {
                orderedRooms[5] = roomInfo.room;
            }
            else
            {
                orderedRooms[4] = roomInfo.room;
            }
        }

        //places rooms from the lowest row into the last places of the orderedRooms array
        foreach (roomInfo roomInfo in lowRow)
		{
			if (Mathf.Round (roomInfo.roomPosition [0]) == x1) {
				orderedRooms [6] = roomInfo.room;
			} else if (Mathf.Round (roomInfo.roomPosition [0]) == x3) {
				orderedRooms [8] = roomInfo.room;
			} else {
				orderedRooms [7] = roomInfo.room;
			}
        }
      return orderedRooms;
    }

    public void giveRoomsValues()
        /*maps the values from values to the orderedRooms array and sets the roomValue of each room to the correct value*/
    {
        GameObject [] ordRooms = orderedRooms();
        for (int i = 0; i < 9; i++)
        {

            ordRooms[i].GetComponent<RoomManager>().roomValue = values[i]; //debug orderedRooms does not contain gOs
        }
    }

   

    float getMinX(Vector2[] roomPositions)
    {
        float xMin = roomPositions[0][0];
        foreach (Vector2 roomPosition in roomPositions)
        {
            if (roomPosition[0] < xMin)
                xMin = roomPosition[0];
        }
        return xMin;
    }

    float getMaxX(Vector2[] roomPositions)
    {
        float xMax = roomPositions[0][0];
        foreach (Vector2 roomPosition in roomPositions)
        {
            if (roomPosition[0] > xMax)
                xMax = roomPosition[0];
        }
        return xMax;
    }

    float getMinY(Vector2[] roomPositions)
    {
        float yMin = roomPositions[0][1];
        foreach (Vector2 roomPosition in roomPositions)
        {
            if (roomPosition[1] < yMin)
                yMin = roomPosition[1];
        }
        return yMin;
    }

    float getMaxY(Vector2[] roomPositions)
    {
        float yMax = roomPositions[0][1];
        foreach (Vector2 roomPosition in roomPositions)
        {
            if (roomPosition[1] > yMax)
                yMax = roomPosition[1];
        }
        return yMax;
    }

    // Use this for initialization
    void Start () {
        giveRoomsValues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
