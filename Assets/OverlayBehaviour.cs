using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayBehaviour : MonoBehaviour {

    SpriteRenderer sr;
	void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void setColour(Color color)
    {
        sr.color = color;
    }
}
