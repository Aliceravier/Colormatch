using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : ExtendedBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomValue = 5;

    [HideInInspector]
    public Vector2 roomSize;

	Transform firstTile;

    private GameObject positionOverlay;
    private GameObject button;
    moveCamera mC;
    moveCamera mC2;

    private GameObject overlay;
	// Use this for initialization
	void Awake () {
        mC2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<moveCamera>();
        mC = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera>();
        positionOverlay = transform.Find("PositionOverlay").gameObject;
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;
        roomSize = getSize();
		firstTile = transform.GetChild (1);
        overlay = transform.Find("Overlay").gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        resetState();
    }

    public void resetState()
        /*sets all rooms the players aren't in to have 
         * no visible positionOverlay and have their button unpressed*/
    {

        if (mC.newRoom != this.transform.gameObject && mC2.newRoom != this.transform.gameObject) //if the new room the player is going to is not this one... (also works for first room somehow)
                                                       //basically, if the players aren't in this room
        {
            //set positionOverlay to invisible
            positionOverlay = this.transform.Find("PositionOverlay").gameObject;
            positionOverlay.GetComponent<SpriteRenderer>().enabled = false;

            //set button animation to unpressed
            button = this.transform.Find("Button").gameObject;
            Animator buttonAnim = button.GetComponent<Animator>();
            buttonAnim.SetBool("ButtonOn", false); //changes anim to unpushed
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


		//find max, min points
		Vector2 min = getMinPoint();
		Vector2 max = getMaxPoint ();


		//get height and width
		float height = Mathf.Abs(max.y - min.y);
		float width = Mathf.Abs (max.x - min.x);

		return new Vector2 (width, height); //dimensions of room
	}

	public Vector2 getMinPoint(){
		/*Gets minimum point of room.
		 */ 
		if (firstTile == null)
			firstTile = transform.GetChild (1);

		Vector2 tileSize = getTileSize(firstTile.gameObject);
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
		return new Vector2 (xmin - tileSize.x/2, ymin - tileSize.y/2);

	}

	public Vector2 getMaxPoint(){
		/* Gets maximum point of room.
		 */

		if (firstTile == null)
			firstTile = transform.GetChild (1);

		Vector2 tileSize = getTileSize(firstTile.gameObject);
		
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
		return new Vector2 (xmax + tileSize.x/2, ymax + tileSize.y/2);
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


		Debug.Log (centre);
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

			if (tile.name == "Tile")
				Debug.Log (tile.position);
		}
		return closestobj;
	}

	public Vector2 diffBetweenCentre(Transform tile){
		return transform.position - tile.position;
	}

	public Vector2 diffBetweenCentres(){
		return (Vector2) transform.position - getCentre ();
	}

		
}
