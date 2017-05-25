using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtendedBehaviour : MonoBehaviour {

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
			if (team == player.GetComponent<Movement> ().playerTeam)
				return player;

		return null;

	}

	public Transform findChildObjectByName(string name){
		foreach (Transform tile in this.transform)
			if (tile.name.CompareTo (name) == 0)
				return tile;

		return null;
	}

	public Transform[] findChildObjectsByName(string name){
		List<Transform> objs = new List<Transform> ();
		foreach (Transform tile in this.transform)
			if (tile.name.CompareTo (name) == 0)
				objs.Add (tile);

		return objs.ToArray ();
	}

	public Transform findChildObjectByTag(string tag){
		foreach (Transform tile in this.transform)
			if (tile.CompareTag(tag))
				return tile;

		return null;	
	}

	public Transform[] findChildObjectsByTag(string tag){
		List<Transform> objs = new List<Transform> ();
		foreach (Transform tile in this.transform)
			if (tile.CompareTag(tag))
				objs.Add (tile);

		return objs.ToArray ();
	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}




}