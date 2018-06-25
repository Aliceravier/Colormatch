﻿using System.Collections;
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

    [HideInInspector]
    public Vector2 roomSize;

	Transform firstTile;

    private GameObject positionOverlay;
    private GameObject button;
    moveCamera mC;
    moveCamera mC2;
    whoWon ww;
    Blinking blink;
    ButtonBehaviour bb;
    GameObject[] players;
    GameObject[] spawners;



    private GameObject overlay;

	// Use this for initialization
	void Awake () {
        walls = findChildObjectByName("Walls").GetComponent<Tilemap>();
        spawners = findChildObjectsByTag("Spawner");
        mC2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<moveCamera>();
        mC = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera>();
        blink = GameObject.FindGameObjectWithTag("God").GetComponent<Blinking>();
        ww = GameObject.FindGameObjectWithTag("God").GetComponent<whoWon>();
        if(findChildObjectByTag("Button") != null)
        bb = findChildObjectByTag("Button").GetComponent<ButtonBehaviour>();
        positionOverlay = transform.Find("PositionOverlay").gameObject;
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;
        
        roomSize = getSize();
		firstTile = transform.GetChild (1);
        players = GameObject.FindGameObjectsWithTag("Player");
        

        if (transform.Find("Overlay") != null)
        overlay = transform.Find("Overlay").gameObject;

    }

    void Start()
    {
        
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;

        players[0].GetComponent<PlayerBehaviour>().spawnPlayer();
        players[1].GetComponent<PlayerBehaviour>().spawnPlayer();
    }
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerStay2D(Collider2D c)
    {
        
        if (minimapActive)
        {
            if (c.CompareTag("Player"))
            {
                blink.makeBlink(positionOverlay, c.GetComponent<Health>().getTeam());
            }
        }

    }

    void OnTriggerExit2D(Collider2D c)
    {   /*Checks if there are players still in the room when a player leaves and resets the rooms state if not
         *
         */        
        if (c.CompareTag("Player"))
        {
            if (!isInRoom(ww.findOtherPlayer(c.gameObject)) && !isInRoom(c.gameObject))
            {
                resetState();
            }
        }
    
    }
		

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            Health h = c.GetComponent<Health>();
            Team team = h.getTeam();


            //focuses on room if thing that entered is a player and they're alive
            if (!h.getDeath() && team == Team.blue)
            {
                mC.focusOnRoom(this.gameObject);
                    
            }
            if (!h.getDeath() && team == Team.green)
            {
                mC2.focusOnRoom(this.gameObject);
            }

            //make enemies appear
            if (enemy != null)
            {
                    if (!isInRoom(ww.findOtherPlayer(c.gameObject)))
                        makeEnemies();
                
            }
            if (findChildObjectByTag("Button") != null)
            {
                bb.makeLock();
            }
        }
    }


    public bool bothInThisRoom()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        return (isInRoom(players[0]) && isInRoom(players[1]));
    }

    public bool isInRoom(GameObject thing)
    {
        return (Mathf.Abs(thing.transform.position.x - this.transform.position.x) < (getSize().x / 2) && //rename to roomSize
                Mathf.Abs(thing.transform.position.y - this.transform.position.y) < (getSize().y / 2) &&
                thing.GetComponent<Collider2D>().enabled);
    }

	public void makeEnemies()
    {
        //use spawn on spawners
        foreach(GameObject spawner in spawners)
        {
            spawner.GetComponent<Spawn>().spawn();
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
        positionOverlay = this.transform.Find("PositionOverlay").gameObject;
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;

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

	public void ChangeTiles(Color color, string layer){
        /*Changes all child tiles with a specified tag to a specified color.
		 */
        Tilemap tileMap = findChildObjectByName(layer).GetComponent<Tilemap>();
        foreach(Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
        {
            //Instantiate(marker, tileMap.CellToWorld(pos), Quaternion.identity);
            tileMap.SetTileFlags(pos, TileFlags.None);
            tileMap.SetColor(pos, color);
        }
	}

	public void setRoomTeam(Team t){
		/*Worthless setter atm :)
		 */
		roomTeam = t;
	}

	public Vector2 getSize() {
        /*Gets size of room as a Vector2
		 */

        Vector3 size = walls.size;
        Vector3 cellSize = walls.cellSize;
        //print("raw size is: " + size);
        //print("cell size is: " + cellSize);
        size = Vector3.Scale(size, cellSize);
        //print("size is: "+(Vector2)size);
        return (Vector2)size;       
	}

	public Vector2 getMinPoint(){
        /*Gets minimum point of room.
		 */
        print("min point " + walls.CellToWorld(new Vector3Int(0, 0, 1)));
        return walls.CellToWorld(new Vector3Int(0, 0, 1));
		}

	public Vector2 getMaxPoint(){
        /* Gets maximum point of room.
		 */
        Vector3 midPoint = walls.CellToWorld(walls.size);
        print("max cell position "+midPoint);
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
		
}
