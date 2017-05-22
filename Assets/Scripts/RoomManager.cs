using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomId = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeTiles(Color color, string tag)
	{
		foreach (Transform child in transform)
			if (child.gameObject.tag == tag)
				child.gameObject.GetComponent<SpriteRenderer>().color = color;
	}

	public void setRoomTeam(Team t){
		roomTeam = t;
	}
}
