using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;

public class trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit2D()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
