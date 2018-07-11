using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;

public class castRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit;
        float theDistance;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
		hit = Physics2D.Raycast(transform.position, (forward));
        if (hit)
        {
            theDistance = hit.distance;
            print(theDistance + " " + hit.collider.gameObject.name);
        }
	}
}
