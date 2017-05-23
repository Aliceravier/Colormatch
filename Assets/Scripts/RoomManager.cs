using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : ExtendedBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomId = 0;
	public int roomValue = 5;
	public Transform firstTile;
	// Use this for initialization
	void Awake () {
		firstTile = transform.GetChild (1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeTiles(Color color, string tag){
		/*Changes all child tiles with a specified tag to a specified color.
		 */
		foreach (Transform child in transform)
			if (child.gameObject.CompareTag(tag))
				child.gameObject.GetComponent<SpriteRenderer>().color = color;
	}

	public void setRoomTeam(Team t){
		/*Worthless setter atm :)
		 */
		roomTeam = t;
	}

	public Vector2 getSize() {
		/*Gets size of room
		 */

		print (firstTile.gameObject);
		//get size of a single tile, for magical variable purposes
		Vector2 tileSize = getTileSize(firstTile.gameObject);

		//find max, min points
		Vector2 min = getMinPoint();
		Vector2 max = getMaxPoint ();


		//get height and width
		float height = Mathf.Abs(max.y - min.y) + tileSize.y;
		float width = Mathf.Abs (max.x - min.x) + tileSize.x;

		return new Vector2 (width, height); //dimensions of room
	}

	public Vector2 getMinPoint(){
		/*Gets minimum point of room.
		 */ 


		float xmin = firstTile.position.x;
		float ymin = firstTile.position.y;

		foreach (Transform tile in transform) {
			float xtile = tile.position.x;
			float ytile = tile.position.y;

			if (xtile < xmin)
				xmin = xtile;
			if (ytile < ymin)
				ymin = ytile;

		}
		return new Vector2 (xmin, ymin);

	}

	public Vector2 getMaxPoint(){
		/* Gets maximum point of room.
		 */
		float ymax = firstTile.position.y;
		float xmax = firstTile.position.x;

		foreach (Transform tile in transform)
		{
			float xtile = tile.position.x;
			float ytile = tile.position.y;


			if (ytile > ymax)
				ymax = ytile;
			if (xtile > xmax)
				xmax = xtile;      
		}
		return new Vector2 (xmax, ymax);
	}


}
