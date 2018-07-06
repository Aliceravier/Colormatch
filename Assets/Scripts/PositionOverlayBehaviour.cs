using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOverlayBehaviour : MonoBehaviour {

    float timer = 0f;
    public float blinkThreshold = 0.5f;
    private bool isBlink = false;
    SpriteRenderer sr;


	// Use this for initialization
	void Awake () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > blinkThreshold && isBlink)
        { 
            sr.enabled = !sr.enabled;
            timer = 0f;
        }
        timer += Time.fixedDeltaTime;
	}


    public void startBlinking(int layer)
    {
        
        isBlink = true;
        gameObject.layer = layer;
    }
    
    public void stopBlinking()
    {
        isBlink = false;
        sr.enabled = false;
    }


}
