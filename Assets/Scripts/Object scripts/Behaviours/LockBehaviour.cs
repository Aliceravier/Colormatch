using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBehaviour : ExtendedBehaviour {

    private RoomBehaviour rm;
    private SpriteRenderer sr;
    private BoxCollider2D bx;
    // Use this for initialization
    void Start () {
		rm = this.transform.parent.parent.gameObject.GetComponent<RoomBehaviour>();
        bx = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        toggleLock(enemiesLeft());
	}

    bool enemiesLeft()
    {
        GameObject[] enemies = rm.findChildObjectsByTag("Enemy");
        foreach (GameObject enemy in enemies) {
            if (rm.isInRoom(enemy)) {
                return true;
            }
        }
        return false;

    }

    public void toggleLock(bool isOn)
    {
        sr.enabled = isOn;
        bx.enabled = isOn;

    }
}
