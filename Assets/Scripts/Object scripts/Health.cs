using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class Health : ExtendedBehaviour {

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
        {
            isDead = true;
        }
	}

    public float getHealth()
    {
        return health;
    }

	public float getMaxHealth(){
		return maxHealth;
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

	public void colourByTeam(){
		/*finds a player of team t, gets their color, sets thing to be that color. Else, white*/
		GameObject player = getPlayerOfTeam (objectTeam);
		if (player != null)
			GetComponent<SpriteRenderer> ().color = player.GetComponent<SpriteRenderer> ().color;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;
	}
}