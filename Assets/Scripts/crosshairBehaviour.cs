using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class crosshairBehaviour : MonoBehaviour {
    public float distance;
    public float speed;
    Camera playerCamera;
    
    float move = 0;
    PlayerID id;


    // Use this for initialization
    void Awake () {
        transform.localPosition = new Vector3(distance, 0, 0);
        id = GetComponentInParent<PlayerBehaviour>()._playerID;
        playerCamera = GetComponentInParent<cameraReference>().playerCamera.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!InputHelper.isControllerInput(id))
        {
            Vector3 targetPosition = playerCamera.ScreenToWorldPoint(InputManager.mousePosition);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
        }
    }

   
}
