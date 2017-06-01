using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairBehaviour : MonoBehaviour {
    public string horizontalMovInput;
    public string verticalMovInput;
    public float distance;

    public float speed;

    float moveHori = 0;
    float moveVert = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        moveHori = Input.GetAxisRaw(horizontalMovInput);
        moveVert = Input.GetAxisRaw(verticalMovInput);
        Vector2 movement = new Vector2(moveHori, moveVert);
        transform.position += (Vector3)movement;
        if (transform.localPosition.magnitude > distance)
            transform.localPosition.magnitude = distance;
    }
}
