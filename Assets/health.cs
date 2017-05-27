using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class Health : MonoBehaviour {

	[SerializeField]
	Team objectTeam = Team.neutral;
	[SerializeField]
	float maxHealth  = 300;
	float health;
	bool isDead;
	// Use this for initialization
	void Start () {
		setUp ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
			isDead = true;
	}

	public void hurt(float damage){
		if (!isDead)
			health -= damage;

	}

	public bool getDeath(){
		return isDead;
	}

	public void setDeath(bool deathState){
		isDead = deathState;
	}

	public void setUp(){
		health = maxHealth;
		isDead = false;
	}

	public Team getTeam(){
		return objectTeam;
	}

	public void setTeam(Team t){
		objectTeam = t;

	}
}