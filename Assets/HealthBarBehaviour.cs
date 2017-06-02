using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour {
	Health h;
	SpriteRenderer s;
	float healthprop, healthdiff;
	Vector2 barSize, pos;
	// Use this for initialization
	void Awake () {
		h = GetComponentInParent<Health> ();
		s = transform.GetChild(0).GetComponent<SpriteRenderer> ();
	}
	void Start(){
		barSize = s.size;

		}

	// Update is called once per frame
	void Update () {
		//stop it from rotating, and store where the parent (THE PLAYER) is atm
		transform.rotation = Quaternion.identity;
		pos = transform.parent.transform.position;

		//shift it to where the player is
		transform.position = new Vector2 (pos.x, pos.y + 1.85f);

		//shift inner red bit along + deplete it if it gets too low
		healthprop = barSize.x * (h.getHealth ()/h.getMaxHealth());
		Mathf.Clamp (healthprop, 0f, h.getMaxHealth ()); //special case for negative values of health

		healthdiff = (s.size.x - healthprop) / 2;
		s.size = new Vector2 (healthprop, barSize.y);
		s.transform.localPosition = new Vector2(s.transform.localPosition.x - healthdiff, s.transform.localPosition.y);
	}
}
