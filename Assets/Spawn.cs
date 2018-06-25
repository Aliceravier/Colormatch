using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject spawneePrefab;
    bool hasSpawned = false;

    public void spawn(Team team=Team.neutral)  //If this causes problems can change to spawnees having teams and using those
    {
        if (!hasSpawned)
        {

            GameObject spawnee = Instantiate(spawneePrefab, transform.position, Quaternion.identity, transform.parent);
            spawnee.GetComponent<Health>().setTeam(team);
            hasSpawned = true;
        }
    }

    /*Getters and setters
     */
    public void setHasSpawned(bool hasSpawned)
    { 
        this.hasSpawned = hasSpawned;
    }

}
