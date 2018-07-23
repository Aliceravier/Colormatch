using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBehaviour : ExtendedBehaviour {

	public Team roomTeam = Team.neutral;
	public int roomValue = 5;
    public int nbEnemies;
    public GameObject enemy;
	[HideInInspector] //WHAT THE FUCK
    public LayerMask playerInteraction;
    public bool minimapActive;
    public Tilemap walls;
    public Tilemap floor;
    public GameObject[] roomPopulation;
    public GameObject[] coins;
    

    [HideInInspector]
    public Vector2 roomSize;

	Transform firstTile;

    private PositionOverlayBehaviour positionOverlay;
    private GameObject button;
    whoWon ww;
    Blinking blink;
    public ButtonBehaviour bb;
    GameObject[] players;
    GameObject[] spawners;
    GameObject[] enemies;



    private GameObject overlay;

	// Use this for initialization
	void Awake () {
        walls = findChildObjectByName("Walls").GetComponent<Tilemap>();
        floor = findChildObjectByName("Floor").GetComponent<Tilemap>();
        spawners = findChildObjectsByTag("Spawner");
        blink = GameObject.FindGameObjectWithTag("God").GetComponent<Blinking>();
        ww = GameObject.FindGameObjectWithTag("God").GetComponent<whoWon>();
        if(findChildObjectByTag("Button") != null)
        bb = findChildObjectByTag("Button").GetComponent<ButtonBehaviour>();
        positionOverlay = GetComponentInChildren<PositionOverlayBehaviour>();
        
        roomSize = roomDims();
		firstTile = transform.GetChild (1);
        players = GameObject.FindGameObjectsWithTag("Player");
        coins = GameObject.FindGameObjectsWithTag("Coin");
        

        if (transform.Find("Overlay") != null)
        overlay = transform.Find("Overlay").gameObject;

    }

    void Start()
    {

        
    }
	// Update is called once per frame
	void Update () {
    }

    public void startBlink(GameObject player)
    {      
        if (minimapActive)
        {
            positionOverlay.startBlinking(player.GetComponent<cameraReference>().getVisibleLayers());
        }
    }


    public GameObject[] getRoomPopulation()
    {
        
        List<GameObject> playersInRoom = new List<GameObject>();
		
        foreach (GameObject player in players)
        {
            if (isInRoom(player))
            {
                print("player " + player + "is in room with value " + roomValue);
                playersInRoom.Add(player);
            }
        }
        
        roomPopulation = playersInRoom.ToArray();
        print("got room population for room with value: " + roomValue + " of "+ roomPopulation.Length);
        return roomPopulation;

    }

    public bool isInRoom(GameObject thing)
    {
        return (Mathf.Abs(thing.transform.position.x - getCentre().x) < (roomDims().x / 2) && 
                Mathf.Abs(thing.transform.position.y - getCentre().y) < (roomDims().y / 2) &&
                thing.GetComponent<Collider2D>().enabled);
    }

	public void spawnRoom()
    {
        //use spawn on spawners
        foreach(GameObject spawner in spawners)
        {
            spawner.GetComponent<Spawn>().spawn(roomTeam);
        }
    }

    public void resetOverlays()
    {
        foreach (GameObject player in roomPopulation)
        {
            startBlink(player); //TODO: refactor pls
        }
    }
    public void resetState()
        /* sets room to have 
         * no visible positionOverlay and have their button unpressed
         * and erase enemies
         * 
         */
    {
        //set positionOverlay to invisible
        positionOverlay.stopBlinking();
       

        //set button animation to unpressed
        if (transform.Find("Button") != null)
        {
            button = transform.Find("Button").gameObject;
            Animator buttonAnim = button.GetComponent<Animator>();
            buttonAnim.SetBool("ButtonOn", false); //changes anim to unpushed
        }

        //make enemies dissapear
        GameObject[] enemies = findChildObjectsByTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        resetSpawners();
    }

    //find way to make this not use string layer
	public void ChangeTiles(Color color, string layer = "Floor"){
        /*Changes all child tiles with a specified tag to a specified color.
		 */
        Tilemap tileMap = findChildObjectByName(layer).GetComponent<Tilemap>();
        foreach(Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
        {
            //Instantiate(marker, tileMap.CellToWorld(pos), Quaternion.identity);
            tileMap.SetTileFlags(pos, TileFlags.None);
            tileMap.SetColor(pos, color);
        }

        GetComponentInChildren<OverlayBehaviour>().setColour(color);
    }

	public void setRoomTeam(Team t){
		/*Worthless setter atm :)
		 */
		roomTeam = t;
	}

	public Vector2 roomDims() {
        /*Gets size of room as a Vector2
		 */

        Vector3 size = new Vector3(walls.size.x -1, walls.size.y,walls.size.z);
        Vector3 cellSize = walls.cellSize;
        size = Vector3.Scale(size, cellSize);
        return (Vector2)size;       
	}

	public Vector2 getMinPoint(){
        /*Gets minimum point of room.
		 */
        return walls.CellToWorld(new Vector3Int(0, 0, 1));
		}

	public Vector2 getMaxPoint(){
        /* Gets maximum point of room.
		 */
        Vector3 midPoint = walls.CellToWorld(walls.size);
        return new Vector2(midPoint.x + walls.cellSize.x, midPoint.y + walls.cellSize.y);
	}

	public Vector2 getCentre(){
        /*Gets the centre point :)
		 */
        Vector3 center = walls.cellBounds.center;
        Vector3Int roundedCenter = new Vector3Int((int)center.x - 1, (int)center.y, (int)center.z);
        return walls.GetCellCenterWorld(roundedCenter);
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
				
		}
		return closestobj;
	}

	public Vector2 diffBetweenCentre(Transform tile){
		return transform.position - tile.position;
	}

	public Vector2 diffBetweenCentres(){
		return (Vector2) transform.position - getCentre ();
	}

    public void resetSpawners()
    {
        foreach(GameObject spawner in spawners)
        {
            spawner.GetComponent<Spawn>().setHasSpawned(false);
        }
    }

    public void checkCoins()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        if(coins.Length == 0)
        {
            ChangeTiles(Color.yellow);
        }
    }
		
}
