using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour {

	[SerializeField]
	float damage = 300;
	[SerializeField]
	float pushback = 20;
	[SerializeField]
	bool isStay = false;
	[SerializeField]
	float startHurt = 8000;
	[SerializeField]
	float endHurt = 0.001f;


	Health theirHealth;
	Team myTeam;
	// Use this for initialization
	void Start () {//right now only good for swords/weapons ok  DO NOT USE FOR TRAPS
		myTeam = GetComponentInParent<Health> ().getTeam ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c){
		if (!isStay) {
			theirHealth = c.gameObject.GetComponent<Health> ();
			if (theirHealth != null && theirHealth.getTeam () != myTeam) {
				theirHealth.rigidBodyHurt (damage, pushback, transform);
				theirHealth.hitFlash (startHurt, endHurt);
			}
		}

	}

	void OnTriggerStay2D(Collider2D c){
		if (isStay) {
			theirHealth = c.gameObject.GetComponent<Health> ();
			if (theirHealth != null && theirHealth.getTeam () != myTeam) {
				theirHealth.rigidBodyHurt (damage, pushback, transform);
				theirHealth.hitFlash (startHurt, endHurt);
			}
		}


	}
}
