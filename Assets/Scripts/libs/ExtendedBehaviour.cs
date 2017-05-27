using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtendedBehaviour : MonoBehaviour {

    public Vector2 randomVector2(Vector2 minimumVector2, Vector2 maximumVector2)
    {
        Vector2 randomVector2 = new Vector2();
        randomVector2.x = UnityEngine.Random.Range(minimumVector2.x, maximumVector2.x);
        randomVector2.y = UnityEngine.Random.Range(minimumVector2.y, maximumVector2.y);
        return randomVector2;
    }

	public void Wait (float seconds, Action action){
		StartCoroutine (_wait (seconds, action));
    }

	public static float getScalingFactor(GameObject tile){
		Sprite s = tile.GetComponent<SpriteRenderer> ().sprite;
		return s.rect.width / s.bounds.size.x;

	}
	public static Vector2 getTileSize(GameObject tile){
		/*Gets the size of a tile :)
		 */
		Vector3 size = tile.GetComponent<SpriteRenderer> ().bounds.size;
		return new Vector2 (size.x, size.y);
	}

	public static Vector2 getRealSize(GameObject thing){
		Vector2 size = getTileSize (thing);
		return new Vector2 (size.x * thing.transform.localScale.x, size.y * thing.transform.localScale.y);
	}


	public static GameObject getPlayerOfTeam(Team team){
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
			if (team == player.GetComponent<Health> ().getTeam())
				return player;

		return null;

	}

	public GameObject findChildObjectByName(string name){
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
        print(transform.gameObject);
        print(this.transform.gameObject);
        foreach (Transform tile in transform)
        {
            print(tile.gameObject);
            if (tile.gameObject.tag == tag)
                objs.Add(tile.gameObject);
        }

		return objs.ToArray ();
	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}




}