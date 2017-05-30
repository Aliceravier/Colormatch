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
	List<GameObject> targets = new List<GameObject>();



	public float deathTime = 0.5f;

	Vector2 place2Go;

	// Use this for initialization

	void Awake(){
		f = GetComponentInChildren<Finder>();
		h = GetComponent<Health> ();
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

	GameObject findClosestPlayer(){
		Vector2 mindist = new Vector2(0,0);
		foreach (GameObject player in targets){
			if (player.GetComponent<Health> ().getTeam() != skullteam)
				print ("haha");

		}

		return null;
	}
}
