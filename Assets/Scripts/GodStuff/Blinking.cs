using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//potential problem: minimap only works if the minimap layer is set above all the other layers. Reason: unknown

public class Blinking : MonoBehaviour {

    private bool isBlink;
    public float flashTime = 0.5f;
    public float downTime = 0.5f;
    moveCamera mC;
    moveCamera mC2;
    Camera minimapCamera1;
    GameObject positionOverlay;
    GameObject tile;

    // Use this for initialization
    void Start () {
        
        mC = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera>();
        minimapCamera1 = GameObject.FindGameObjectWithTag("MinimapCamera1").GetComponent<Camera>();
        mC2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<moveCamera>();
        StartCoroutine(blink(flashTime, downTime));

    }
	
	// Update is called once per frame
	void Update () { //constantly gets the room the player is in and makes this room blink

        GameObject room1 = mC.getPlayersRoom();
        GameObject room2 = mC2.getPlayersRoom();
        if (room1 == room2)
        {
            minimapCamera1.cullingMask |= (1 << LayerMask.NameToLayer("MinimapPos2"));
        }
        else
        {
            minimapCamera1.cullingMask &= ~(1 << LayerMask.NameToLayer("MinimapPos2"));
        }
        makeBlink(room1.transform.Find("PositionOverlay").gameObject, Team.blue);
        makeBlink(room2.transform.Find("PositionOverlay").gameObject, Team.green);
    }

    void makeBlink(GameObject positionOverlay, Team team)
    {
        positionOverlay.GetComponent<SpriteRenderer>().enabled = isBlink;
        if (team == Team.blue)
            positionOverlay.layer = LayerMask.NameToLayer("MinimapPos1");

        if (team == Team.green)
            positionOverlay.layer = LayerMask.NameToLayer("MinimapPos2");
                
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
}
