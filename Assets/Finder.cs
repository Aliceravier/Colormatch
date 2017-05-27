using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour {

	string target = "Player";

	List<GameObject> usefulStuff = new List<GameObject>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void onTriggerEnter2D(Collider2D c){
		if (c.gameObject.tag == (target)) {
			usefulStuff.Add (c.gameObject);
		}

	}

	void onTriggerExit2D(Collider2D c){
		if (c.gameObject.tag ==(target))
			usefulStuff.Remove (c.gameObject);

	}

	public List <GameObject> getList(){
		return usefulStuff;
	}

	public void setTarget(string target){
		usefulStuff.Clear ();
		this.target = target;
	}


}
