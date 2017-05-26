using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour {

	[SerializeField]
	float damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.CompareTag ("Player") || c.CompareTag ("Enemy")) {
			c.gameObject.GetComponent<Health> ().hurt (damage);
			
		
		}
	}
}
