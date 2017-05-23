using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
    private bool isBlink;
    public float flashTime = 0.5f;
    public float downTime = 0.5f;
    moveCamera2 mC2;
    GameObject positionOverlay;
    GameObject tile;
    public GameObject oldRoom;
    // Use this for initialization
    void Start () {
        
        mC2 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera2>();
        StartCoroutine(blink(flashTime, downTime));

    }
	
	// Update is called once per frame
	void Update () { //constantly gets the room the player is in and makes this room blink
        

        GameObject room = mC2.getPlayersRoom();
        makeBlink(room.transform.Find("PositionOverlay").gameObject);
    }

    void makeBlink(GameObject positionOverlay)
    {
        positionOverlay.GetComponent<SpriteRenderer>().enabled = isBlink;
    }

    IEnumerator blink(float flashTime, float downTime)
    {
        while (true)
        {
            isBlink = true;
            yield return new WaitForSeconds(flashTime);
            isBlink = false;
            yield return new WaitForSeconds(downTime);
        }
    }

    public void hidePositionOverlay(GameObject oldRoom)
    {
        GameObject positionOverlay;
        positionOverlay = oldRoom.transform.Find("PositionOverlay").gameObject;
        positionOverlay.GetComponent<SpriteRenderer>().enabled = false;
    }
}
