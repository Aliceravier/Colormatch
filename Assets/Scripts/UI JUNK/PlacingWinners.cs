using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;

public class PlacingWinners : MonoBehaviour {
	Team winner;
	//Team loser;

	RectTransform los;
	RectTransform hwin;

	Vector3 bluePos = new Vector3(200, 200, 0);
	Vector3 greenPos = new Vector3 (700,200,0);

	// Use this for initialization
	void Awake () {
		winner = GameObject.FindGameObjectWithTag ("God").GetComponent<whoWon> ().getWinningTeam();
		//loser = GameObject.FindGameObjectWithTag ("God").GetComponent<whoWon> ().losingTeam;

		los = GameObject.Find ("Losser").GetComponent<RectTransform>();
		hwin = GameObject.Find ("Weiner").GetComponent<RectTransform> ();

		placeWinners ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void placeWinners(){
		if (winner == Team.blue) {
			los.transform.position = greenPos;
			hwin.transform.position = bluePos;	
		} else if (winner == Team.green) {
			los.transform.position = bluePos;
			hwin.transform.position = greenPos;
		} else
			Debug.Log ("PANIC");
	}
}
