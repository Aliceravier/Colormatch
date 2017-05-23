using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtendedBehaviour : MonoBehaviour {

	public void Wait (float seconds, Action action){
		StartCoroutine (_wait (seconds, action));

	}

	public static Vector2 getTileSize(GameObject tile){
		/*Gets the size of a tile :)
		 */
		print (tile);
		Vector3 size = tile.GetComponent<SpriteRenderer> ().bounds.size;
		return new Vector2 (size.x, size.y);
	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}

}