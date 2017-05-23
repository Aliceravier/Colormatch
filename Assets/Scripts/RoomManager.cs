using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : ExtendedBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomId = 0;
	public int roomValue = 5;

    [HideInInspector]
    public Vector2 roomSize;

	public Transform firstTile;

    private GameObject overlay;
    moveCamera2 mC2;
	// Use this for initialization
	void Awake () {
        roomSize = getSize();
		firstTile = transform.GetChild (1);
        overlay = transform.Find("Overlay").gameObject;
        mC2 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera2>();

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
        overlay.GetComponent<SpriteRenderer>().color = color;
	}

    


    //makes room on minimap blink yellow once


    /*public void UpdateMinimap(GameObject player)
    {
        GameObject room;
        moveCamera2 mC2 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera2>();
        room = mC2.getPlayersRoom();
        overlay = room.transform.Find("Overlay").gameObject;
        if (mC2.isInScope(player))
        {
            makeBlinkOnce(overlay);
        }
    }*/
	public void setRoomTeam(Team t){
		/*Worthless setter atm :)
		 */
		roomTeam = t;
	}

	public Vector2 getSize() {
		/*Gets size of room
		 */

		//special case checks :))))
		if (firstTile == null)
			firstTile = transform.GetChild (1);
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
