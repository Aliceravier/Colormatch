using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransSkullBehaviour : ExtendedBehaviour {

	[SerializeField]
	float moveSpeed;
	[SerializeField]
	string targetTag;
	[SerializeField]
	float retargetTime;

	bool isDead;
	Team skullteam;

    RoomBehaviour parentRoom;

	Finder f; 
	Health h;
	Rigidbody2D rb;

	List<GameObject> targets = new List<GameObject>();
	Vector2 target = new Vector2 (0, 0);

	public float deathTime = 0.5f;


	Vector2 place2Go;

	// Use this for initialization

	void Awake(){
		f = GetComponentInChildren<Finder>();
		h = GetComponent<Health> ();
		rb = GetComponent<Rigidbody2D> ();
        parentRoom = GetComponentInParent<RoomBehaviour>();
	}
	void Start () {
		
		f.setTag (targetTag);
	}
	
	// Update is called once per frame
	void Update () {
		//checks if dead also the current team of the skull
		isDead = h.getDeath ();
		skullteam = h.getTeam ();

        if (isDead)
        {
            killSkull();
        }

		//gets a list of players maybe. then finds the closest one
		targets = f.getList ();
		GameObject closestPlayer = findClosestPlayer ();

        //finds the vector between player and position, or just doesn't lol
        if (closestPlayer != null && isInRoom(closestPlayer))
        {
            target = closestPlayer.transform.position - transform.position;
        }
        else {
            target = new Vector2(0, 0);
        }


		//find player in room if a player is in the room
        //move towards player
        
	}
    
	void FixedUpdate(){
		rb.velocity = (target.normalized * moveSpeed * Time.smoothDeltaTime);

	}
    bool isInRoom(GameObject target)
    {
        foreach (GameObject player in parentRoom.getRoomPopulation())
        {
            if (target == player) return true;
        }
        return false;
    }
	void killSkull(){
		GetComponent<Collider2D> ().enabled = false;
		Wait (deathTime, () => {
			Destroy (this.gameObject);
		});
	}

	GameObject findClosestPlayer(){
		/*Given a list of players, finds the closest one :)
		 * 
		 */
		float mindist = float.MaxValue;
		GameObject closestPlayer = null;
		foreach (GameObject player in targets){
			if (player.GetComponent<Health> ().getTeam () != skullteam && Vector2.Distance (transform.position, player.transform.position) < mindist) {
				closestPlayer = player;
				mindist = Vector2.Distance (transform.position, player.transform.position);
			}
		}

		return closestPlayer;
	}

	void setColour(Team t){
		/*finds a player of team t, gets their color, sets enemy to be that color. Else, white*/
		GameObject player = getPlayerOfTeam (t);
		if (player != null)
			GetComponent<SpriteRenderer> ().color = player.GetComponent<SpriteRenderer> ().color;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;
	}


}
