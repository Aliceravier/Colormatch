using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransSkullBehaviour : ExtendedBehaviour {

	[SerializeField]
	float moveSpeed;
	[SerializeField]
	string targetTag;

	bool isDead;
	Team skullteam;

	Finder f; 
	Health h;
	Rigidbody2D rb;

	List<GameObject> targets = new List<GameObject>();
	Vector2 target = new Vector2 (0, 0);

	float deathTime = 0.5f;

	Vector2 place2Go;

	// Use this for initialization

	void Awake(){
		f = GetComponentInChildren<Finder>();
		h = GetComponent<Health> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	void Start () {
		
		f.setTarget (targetTag);
	}
	
	// Update is called once per frame
	void Update () {

		isDead = h.getDeath ();
		skullteam = h.getTeam ();


		if (isDead)
			killSkull();

		targets = f.getList ();
		GameObject closestPlayer = findClosestPlayer ();
		if (closestPlayer != null) {
			target = transform.position - findClosestPlayer ().transform.position;
		}
		else
			target = new Vector2 (0, 0);


		//find player in room if a player is in the room
        //move towards player
        
	}

	void FixedUpdate(){
		rb.AddForce (target.normalized * moveSpeed * Time.smoothDeltaTime);

	}

	void killSkull(){
		Wait (deathTime, () => {
			Destroy (this.gameObject);
		});
	}

	GameObject findClosestPlayer(){
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
}
