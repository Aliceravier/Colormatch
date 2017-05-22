using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomId = 0;
	public int roomValue = 5;
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

	public Vector2 getSize() {
		Transform firstTile = transform.GetChild(1);
		Transform secondTile = transform.GetChild(2);
		Vector3 tileSize = firstTile.GetComponent<SpriteRenderer> ().bounds.size;

		//sets all vars to look at first tile
		float xmax, xmin = firstTile.position.x;
		xmax = xmin;
		float ymax, ymin = firstTile.position.y;
		ymax = ymin;

		//makes max, min checks for both x and y values
		foreach (Transform tile in transform)
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

		//find diff between 
		float height = Mathf.Abs(ymax - ymin) + tileSize.y;
		float width = Mathf.Abs(xmax - xmin) + tileSize.x;
		return new Vector2 (width, height); //dimensions of room
	}


}
