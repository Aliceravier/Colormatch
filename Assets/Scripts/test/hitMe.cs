using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(this.transform.position);
        print(this.transform.position + new Vector3(0, 0, -10));

        print(this.transform.position + new Vector3(0, 0, 20));
//        Vector3 start = this.transform.position + new Vector3(0, 0, -10);
        Vector3 end = this.transform.position + new Vector3(0, 0, 20);
        RaycastHit2D hit = Physics2D.Linecast(this.transform.position + new Vector3(0, 0, -10), this.transform.position + new Vector3(0, 0, 20));
        //Debug.DrawRay(start, new Vector3(0, 0, 30), Color.green);
        Debug.DrawRay(end, new Vector3(0, 0, 30));
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerInteraction"))  
            print("hit something on player plane");
        if (!hit)
            print("hit nothing");
        if (hit)
            print("hit something");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
