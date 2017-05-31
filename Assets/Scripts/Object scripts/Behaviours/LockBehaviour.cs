using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBehaviour : ExtendedBehaviour {

    private RoomManager rm;
    
    // Use this for initialization
    void Start () {
		rm = this.transform.parent.parent.gameObject.GetComponent<RoomManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (noEnemiesLeft())
        {
            Destroy(this.transform.gameObject);
        }
	}

    bool noEnemiesLeft()
    {
        GameObject[] enemies = rm.findChildObjectsByTag("Enemy");
        foreach (GameObject enemy in enemies) {
            if (rm.isInRoom(enemy)) {
                return false;
            }
        }
       return true;

    }
}
