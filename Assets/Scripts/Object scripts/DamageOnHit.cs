using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {

	[SerializeField]
	float damage = 300;
	Health theirHealth;
	Team myTeam;


	// Use this for initialization
	void Start () {
		myTeam = GetComponent<Health> ().getTeam ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D c){
		theirHealth = c.gameObject.GetComponent<Health> ();
		if (theirHealth != null && theirHealth.getTeam () != myTeam)
			theirHealth.hurt (damage);

	}


}
