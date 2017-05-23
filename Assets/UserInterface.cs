using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
    private bool isBlink;
    public float flashTime = 0.5f;
    public float downTime = 0.5f;
    moveCamera2 mC2;
    Color roomColor;
    GameObject positionOverlay;
    // Use this for initialization
    void Start () {
        
        mC2 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<moveCamera2>();
        GameObject room = mC2.getPlayersRoom();
        positionOverlay = room.transform.Find("PositionOverlay").gameObject;
        StartCoroutine(blink(flashTime, downTime));

    }
	
	// Update is called once per frame
	void Update () {
        GameObject room = mC2.getPlayersRoom();
        makeBlinkOnce(room.transform.Find("Overlay").gameObject);
    }

    void makeBlinkOnce(GameObject overlay)
    {
        if (isBlink)
        {
            overlay.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 1);
        }
        else
        {
            overlay.GetComponent<SpriteRenderer>().color = roomColor;
            roomColor = overlay.GetComponent<SpriteRenderer>().color;
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
