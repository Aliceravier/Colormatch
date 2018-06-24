using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject spawnee;

    public void spawn()
    {
        Instantiate(spawnee);
    }

}
