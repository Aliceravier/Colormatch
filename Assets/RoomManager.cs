using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LastCurRooms
{
    public GameObject lastRoom;
    public GameObject currentRoom;
    public LastCurRooms(GameObject lastRoom, GameObject currentRoom)
    {
        this.lastRoom = lastRoom;
        this.currentRoom = currentRoom;
    }
}

public class RoomManager : MonoBehaviour {
    Dictionary<GameObject, LastCurRooms> playerToRooms = new Dictionary<GameObject, LastCurRooms>();
    GameObject[] rooms;
    GameObject[] players;
    // Use this for initialization
    void Start () {
        rooms = GameObject.FindGameObjectsWithTag("Room");

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerToRooms.Add(player, new LastCurRooms(null, null));
           
            updateActiveRoom(player);
            Debug.Log("Awake player is " + player + "with rooms" + playerToRooms[player].currentRoom + ", " + playerToRooms[player].lastRoom);
            playerToRooms[player].lastRoom = playerToRooms[player].currentRoom;
        }
	}

    // Update is called once per frame
    void Update() {
        foreach (GameObject player in players)
        {
            Debug.Log("Update player is " + player + "with rooms" + playerToRooms[player].currentRoom + ", " + playerToRooms[player].lastRoom);
            updateActiveRoom(player);
        }
    }

    public void updateActiveRoom(GameObject player)
    {
        /* for the given player, updates in the dictionnary which room they are currently in
         */
        foreach (GameObject room in rooms)
        {
            RoomBehaviour rm = room.GetComponent<RoomBehaviour>();
            Vector2 roomDims = rm.roomSize;
            if (Mathf.Abs(player.transform.position.x - rm.getCentre().x) < (roomDims.x / 2) &&
                Mathf.Abs(player.transform.position.y - rm.getCentre().y) < (roomDims.y / 2))
            {
                LastCurRooms before = playerToRooms[player];
                before.currentRoom= room;
                playerToRooms[player] = before;
            }
        }
    }

    public GameObject getActiveRoom(GameObject player)
    {
        return playerToRooms[player].currentRoom;
    }
}

//code to update whenever player moves from room to room: exposes active rooms to camera with getters
//spawns rooms
//handles spawning of things in rooms
//dictionnary points to last room and active room of player last room is public, active is public