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
			if (team == player.GetComponent<PlayerBehaviour> ().playerTeam)
				return player;

		return null;

	}

	IEnumerator _wait(float seconds, Action callback){
		yield return new WaitForSeconds (seconds);
		callback();

	}




}