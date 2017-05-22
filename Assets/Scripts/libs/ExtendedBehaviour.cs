using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtendedBehaviour : MonoBehaviour {

	public void Wait (float seconds, Action action){
		StartCoroutine (_wait (seconds, action));

	}

	public Vector2 getSize(Transform tile){
		/*Gets the size of a tile :)
		 */
		Vector3 size = tile.GetComponent<SpriteRenderer> ().bounds.size;
		return new Vector2 (size.x, size.y);
	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}

}