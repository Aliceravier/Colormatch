using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransSkullBehaviour : ExtendedBehaviour {

	[SerializeField]
	float moveSpeed;
	[SerializeField]
	Team skullteam = Team.neutral;
	[SerializeField]
	string targetTag;

	bool isDead;

	Finder f; 
	List<GameObject> targets = new List<GameObject>();



	float deathTime = 0.5f;

	Vector2 place2Go;

	// Use this for initialization

	void Awake(){
		f = GetComponentInChildren<Finder>();
	}
	void Start () {
		
		f.setTarget (targetTag);
	}
	
	// Update is called once per frame
	void Update () {

		isDead = GetComponent<Health> ().getDeath ();
		if (isDead)
			killSkull();

		targets = f.getList ();

		//find player in room if a player is in the room
        //move towards player
        
	}

	void FixedUpdate(){


	}

	void killSkull(){
		Wait (deathTime, () => {
			Destroy (this.gameObject);
		});
	}

	Vector2 findClosestPlayer(){
		Vector2 mindist;
		foreach (GameObject player in targets){
			if (player.GetComponent<PlayerBehaviour> ().playerTeam != skullteam)
				print ("haha");

		}

		return new Vector2 (0, 0);
	}
}
