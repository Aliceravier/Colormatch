﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOnStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<SpriteRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
