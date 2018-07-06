using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairBehaviour : MonoBehaviour {
    public string horizontalMovInput;
    public float distance;

    public float speed;

    float move = 0;


    // Use this for initialization
    void Start () {
        transform.localPosition = new Vector3(distance, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
