using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;
using System;

public class ExtendedBehaviour : NetworkBehaviour {

    public Vector2 randomVector2(Vector2 minimumVector2, Vector2 maximumVector2)
    {
		/*Takes two vectors, returns a random vector between the two*/
        Vector2 randomVector2 = new Vector2();
        randomVector2.x = UnityEngine.Random.Range(minimumVector2.x, maximumVector2.x);
        randomVector2.y = UnityEngine.Random.Range(minimumVector2.y, maximumVector2.y);
        return randomVector2;
    }

	public void Wait (float seconds, Action action){
		/*Does any code inside action (format ()=>{<CODE>}) after seconds seconds */
		StartCoroutine (_wait (seconds, action));
    }

	public void Pause(float seconds, float timeScale = 0f){
		/* Slows time to timeScale for seconds seconds (by default stops time)*/
		StartCoroutine (_pause (seconds, timeScale));
	}
		
	public static Vector2 getTileSize(GameObject tile){
		/*Gets the size of a tile :)
		 */
		Vector3 size = tile.GetComponent<SpriteRenderer> ().bounds.size;
		return new Vector2 (size.x, size.y);
	}

	public static Vector2 getRealSize(GameObject thing){
		/*Gets size of a tile * by the objects size, getting total overall size */
		Vector2 size = getTileSize (thing);
		return new Vector2 (size.x * thing.transform.localScale.x, size.y * thing.transform.localScale.y);
	}


	public static GameObject getPlayerOfTeam(Team team){
		/*Given a team, returns a player of that team*/
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
			if (team == player.GetComponent<Health> ().getTeam())
				return player;

		return null;

	}

	public GameObject findChildObjectByName(string name){
		/*Returns the first child gameobject with a specific name it can find*/
		foreach (Transform tile in this.transform)
			if (tile.name.CompareTo (name) == 0)
				return tile.gameObject;

		return null;
	}

	public GameObject[] findChildObjectsByName(string name){
		
		List<GameObject> objs = new List<GameObject> ();
		foreach (Transform tile in this.transform)
			if (tile.name.CompareTo (name) == 0)
				objs.Add (tile.gameObject);

		return objs.ToArray ();
	}

	public GameObject findChildObjectByTag(string tag){
		foreach (Transform tile in this.transform)
			if (tile.CompareTag(tag))
				return tile.gameObject;

		return null;	
	}

	public GameObject[] findChildObjectsByTag(string tag){
		List<GameObject> objs = new List<GameObject> ();
        foreach (Transform tile in transform)
        {
            if (tile.gameObject.tag == tag)
                objs.Add(tile.gameObject);
        }

		return objs.ToArray ();
	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}

	IEnumerator _pause(float seconds, float timeScale){
		Time.timeScale = timeScale;
		float pauseEnd = Time.realtimeSinceStartup + seconds;
		while (Time.realtimeSinceStartup < pauseEnd)
			yield return 0;
		Time.timeScale = 1;

	}




}