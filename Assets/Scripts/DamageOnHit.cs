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
        if (c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Enemy"))
        {
            theirHealth = c.gameObject.GetComponent<Health>();
            print("their health is: " + theirHealth.getHealth());
            print("my health is: " + theirHealth.getHealth());
            print("Their team is: " + theirHealth.getTeam() + " and they are " + theirHealth.gameObject + " at position " + c.transform.position);
            print("my team is: " + myTeam + " and I am " + transform.gameObject + " at position " + transform.localPosition);
            if (theirHealth != null && theirHealth.getTeam() != myTeam)
                theirHealth.hurt(damage);
        }
	}


}
