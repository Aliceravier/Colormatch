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