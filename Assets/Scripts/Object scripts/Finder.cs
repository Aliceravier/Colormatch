using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;

public class Finder : MonoBehaviour {

	string toFind = "Player";

	List<GameObject> usefulStuff = new List<GameObject>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.CompareTag(toFind)) {
			usefulStuff.Add (c.gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D c){
		if (c.gameObject.tag ==(toFind))
			usefulStuff.Remove (c.gameObject);

	}

	public List <GameObject> getList(){
		return usefulStuff;
	}

	public void setTag(string tag){
		usefulStuff.Clear ();
		toFind = tag;
	}


}
