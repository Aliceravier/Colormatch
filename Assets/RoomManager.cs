using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	Team roomTeam;
	// Use this for initialization
	void Start () {
		roomTeam = Team.neutral;
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
