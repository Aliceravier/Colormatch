using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*A handler for moving between rooms - todo: make into a nice observer design pattern */
//could potentially just store the room on the player itself? do iin future
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
            PlayerStatistics playerToRoom = player.GetComponent<PlayerStatistics>();
           
            updateActiveRoom(player);
            playerToRoom.lastRoom = playerToRoom.currentRoom;
            player.GetComponent<PlayerBehaviour>().spawnPlayer();
        }
	}

    // Update is called once per frame
    //this is awful and will be dramatically improved when we observer it UP
    void Update() {
        
        foreach (GameObject player in players)
        {
            PlayerStatistics playerToRoom = player.GetComponent<PlayerStatistics>();
            updateActiveRoom(player);
            GameObject currentRoom = playerToRoom.currentRoom;
            GameObject lastRoom = playerToRoom.lastRoom;
                     

            if (currentRoom != lastRoom) //referenceEquals if '!=' doesn't work
            {
                print("currentRoom room and last room are different");
                updatePopulations(player);
                if (!isOthersInRoom(lastRoom, player))
                    
                {
                    lastRoom.GetComponent<RoomBehaviour>().resetState();
                }
                else
                {
                    lastRoom.GetComponent<RoomBehaviour>().resetOverlays();
                }

                if(!isOthersInRoom(currentRoom, player))
                {
                    currentRoom.GetComponent<RoomBehaviour>().spawnRoom(); 
                    resetMasks();
                }
                else
                {
                    showAllMiniMaps(); //currently 
                }

                currentRoom.GetComponent<RoomBehaviour>().startBlink(player);
                playerToRoom.lastRoom = currentRoom;

            }
           

        }
    }

    private bool isOthersInRoom(GameObject room, GameObject player)
    {
        RoomBehaviour rb = room.GetComponent<RoomBehaviour>();
        foreach (GameObject otherplayer in players)
        {
            if (player != otherplayer && rb.isInRoom(otherplayer)){
                return true;
            }
        }
        return false;
    }

    public void updatePopulations(GameObject player)
    { //updates populations of player's last and current rooms
        PlayerStatistics playerToRoom = player.GetComponent<PlayerStatistics>();
        GameObject currentRoom = playerToRoom.currentRoom;
        GameObject lastRoom = playerToRoom.lastRoom;
        currentRoom.GetComponent<RoomBehaviour>().getRoomPopulation();
        lastRoom.GetComponent<RoomBehaviour>().getRoomPopulation();
    }

    public void updateActiveRoom(GameObject player)
    {
        /* for the given player, updates in playerStatistics which room they are currently in
         */
        PlayerStatistics playerToRoom = player.GetComponent<PlayerStatistics>();
        foreach (GameObject room in rooms)
        {
            RoomBehaviour rb = room.GetComponent<RoomBehaviour>();
            if (rb.isInRoom(player))
            {
                playerToRoom.currentRoom = room;
            }
        }
    }

    public GameObject getActiveRoom(GameObject player)
    {
        return playerToRooms[player].currentRoom;
    }

    private void resetMasks()
    {
        foreach (GameObject player in players)
        {
            cameraReference cr = player.GetComponent<cameraReference>();
            cr.resetMask();
        }
    }

    /* currently gets all players and forces them to all show all players on their miniimaps
    will break with more than 2 players
    todo: fix*/

    private void showAllMiniMaps()
    {
        foreach (GameObject player in players)
        {
            cameraReference cr = player.GetComponent<cameraReference>();
            cr.viewAllMinimaps(players);
        }
    }
}



//code to update whenever player moves from room to room: exposes active rooms to camera with getters
//spawns rooms
//handles spawning of things in rooms
//dictionnary points to last room and active room of player last room is public, active is public