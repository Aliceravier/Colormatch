using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;
using Luminosity.IO;

public class CrosshairBehaviour : MonoBehaviour {
    public float range;
    public float tolerance = 0.25f;
    Camera playerCamera;
    Vector2 stickRotation;


    private Quaternion rotation;
    
    float move = 0;
    PlayerID id;


    // Use this for initialization
    void Awake () {
        id = GetComponentInParent<PlayerBehaviour>()._playerID;
        playerCamera = GetComponentInParent<cameraReference>().playerCamera.GetComponent<Camera>();
        rotation = transform.rotation;

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (InputHelper.isControllerInput(id))
        {
            transform.rotation = rotation;
            moveToStickPosition();
            
        } else
        {
            Vector3 targetPosition = playerCamera.ScreenToWorldPoint(InputManager.mousePosition);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
        }
    }


    void moveToStickPosition()
    {
        stickRotation = GetComponentInParent<PlayerBehaviour>().stickRotation;
        if (stickRotation.magnitude < tolerance)
        {
            stickRotation = Vector2.zero;
        }

      
        Vector3 targetPosition = new Vector3(stickRotation.x * -range, stickRotation.y * -range, 0);
        transform.position = transform.parent.position - targetPosition;
        
    }

   
}
