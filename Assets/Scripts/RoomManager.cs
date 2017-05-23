using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : ExtendedBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomId = 0;
	public int roomValue = 5;

    [HideInInspector]
    public Vector2 roomSize;

	Transform firstTile;

    private GameObject positionOverlay;
    moveCamera2 mC2;

    private GameObject overlay;
	// Use this for initialization
	void Awake () {
        mC2 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera2>();
        positionOverlay = transform.Find("PositionOverlay").gameObject;
        print(positionOverlay);
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;
        roomSize = getSize();
		firstTile = transform.GetChild (1);
        overlay = transform.Find("Overlay").gameObject;

    }
	
	// Update is called once per frame
	void Update () {


    }

    void resetState()
    {
        if(mC2.newRoom != this)
        {
            positionOverlay = this.transform.Find("PositionOverlay").gameObject;
            positionOverlay.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
	public void ChangeTiles(Color color, string tag){
		/*Changes all child tiles with a specified tag to a specified color.
		 */
		foreach (Transform child in transform)
			if (child.gameObject.CompareTag(tag))
				child.gameObject.GetComponent<SpriteRenderer>().color = color;
        overlay.GetComponent<SpriteRenderer>().color = color;
	}

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
		if (firstTile == null)
			firstTile = transform.GetChild (1);

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

		if (firstTile == null)
			firstTile = transform.GetChild (1);
		
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

	public Vector2 getCentre(){
		/*Gets the centre point :)
		 */
		Vector2 min = getMinPoint();
		Vector2 size = getSize();

		return new Vector2 (min.x + (size.x / 2), min.y + (size.y / 2));
	}

	public Transform getClosestCentreTile(){
		Vector2 centre = getCentre();



		if (firstTile == null)
			firstTile = transform.GetChild (1);

		Transform closestobj = firstTile;
		float closest = Vector2.Distance(centre, firstTile.position);

		foreach (Transform tile in transform)
		{
			if (Vector2.Distance(centre, tile.position) < closest){
				closest = Vector2.Distance(centre, tile.position);
				closestobj = tile;
			}
		}

		return closestobj;
	}

	public Vector2 diffBetweenCentre(Transform tile){
		return transform.position - tile.position;
	}

		
}
