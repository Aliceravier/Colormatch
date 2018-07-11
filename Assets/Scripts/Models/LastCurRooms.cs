using UnityEngine; using UnityEngine.Networking;
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