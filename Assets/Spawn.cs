using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject spawnee;
    bool hasSpawned = false;

    public void spawn()
    {
        if (!hasSpawned)
        {
            Instantiate(spawnee, transform.position, Quaternion.identity, transform.parent);
            hasSpawned = true;
        }
    }

    public void setHasSpawned(bool hasSpawned)
    {
        this.hasSpawned = hasSpawned;
    }

}
