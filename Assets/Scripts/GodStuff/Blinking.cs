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
    Camera minimapCamera2;
    GameObject positionOverlay;
    GameObject tile;
    GameHandler gh;

    // Use this for initialization
    void Start () {
        
        mC = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera>();
        mC2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<moveCamera>();
        gh = GameObject.FindGameObjectWithTag("God").GetComponent<GameHandler>();

        if (GameObject.FindGameObjectWithTag("MinimapCamera1") != null)
        minimapCamera1 = GameObject.FindGameObjectWithTag("MinimapCamera1").GetComponent<Camera>();
        if (GameObject.FindGameObjectWithTag("MinimapCamera2") != null)
            minimapCamera2 = GameObject.FindGameObjectWithTag("MinimapCamera2").GetComponent<Camera>();

        StartCoroutine(blink(flashTime, downTime));

    }
	
	// Update is called once per frame
	void Update () { 

        GameObject room1 = mC.getPlayersRoom();
        GameObject room2 = mC2.getPlayersRoom();

        //make the position overlays of the room(s) the players are in blink
        makeBlink(room1.transform.Find("PositionOverlay").gameObject, Team.blue);
        makeBlink(room2.transform.Find("PositionOverlay").gameObject, Team.green);
    }

    public void makeBlink(GameObject positionOverlay, Team team)
    {

        positionOverlay.GetComponent<SpriteRenderer>().enabled = isBlink;

        if (team == Team.blue)
        {
            if (gh.bothInSameRoom())
            {
                positionOverlay.layer = LayerMask.NameToLayer("MinimapPos2");
                minimapCamera1.cullingMask |= (1 << LayerMask.NameToLayer("MinimapPos2"));
            }
            else
            {
                positionOverlay.layer = LayerMask.NameToLayer("MinimapPos1");
                minimapCamera1.cullingMask &= ~(1 << LayerMask.NameToLayer("MinimapPos2"));
            }
            
        }

        if (team == Team.green)
        {
            if (gh.bothInSameRoom())
            {
                positionOverlay.layer = LayerMask.NameToLayer("MinimapPos1");
                minimapCamera2.cullingMask |= (1 << LayerMask.NameToLayer("MinimapPos1"));
            }
            else
            {
                positionOverlay.layer = LayerMask.NameToLayer("MinimapPos2");
                minimapCamera2.cullingMask &= ~(1 << LayerMask.NameToLayer("MinimapPos1"));
            }
        }
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
