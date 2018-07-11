using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;


//potential problem: minimap only works if the minimap layer is set above all the other layers. Reason: unknown

public class Blinking : MonoBehaviour {

    private bool isBlink;
    public float flashTime = 0.5f;
    public float downTime = 0.5f;
    Camera minimapCamera1;
    Camera minimapCamera2;
    GameObject positionOverlay;
    GameObject tile;
    GameHandler gh;

    // Use this for initialization
    void Start () {
        gh = GameObject.FindGameObjectWithTag("God").GetComponent<GameHandler>();

        if (GameObject.FindGameObjectWithTag("MinimapCamera1") != null)
        minimapCamera1 = GameObject.FindGameObjectWithTag("MinimapCamera1").GetComponent<Camera>();
        if (GameObject.FindGameObjectWithTag("MinimapCamera2") != null)
            minimapCamera2 = GameObject.FindGameObjectWithTag("MinimapCamera2").GetComponent<Camera>();

        StartCoroutine(blink(flashTime, downTime));

    }
	
	// Update is called once per frame
	void Update () { 
    }

    public void makeBlink(GameObject positionOverlay, Team team)
    {
		/* Handles the position on the minimap blinking on and off repeatedly. Also, ensures that the correct player sees the correct player's position on their map */
        positionOverlay.GetComponent<SpriteRenderer>().enabled = isBlink;

		//ensures only blue player can see blue player's pos
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

		//ensures only green player can see green player's pos
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
    {/*If you want a picture of the future, imagine a bool blinking on and off forever, at repeated set intervals*/
        while (true)
        {
            isBlink = true;
            yield return new WaitForSeconds(flashTime);
            isBlink = false;
            yield return new WaitForSeconds(downTime);
        }
    }
}
