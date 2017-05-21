using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonBehaviour : MonoBehaviour {
    private Animator anim;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();		
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.gameObject.tag == "Player")
        {
            anim.SetTrigger("ButtonPressed");            
            ChangeTiles(c.transform.gameObject.GetComponent<SpriteRenderer>().color, "Tile");
            

        }
    }
    void ChangeTiles(Color color, string tag)
    {
        foreach (Transform child in transform.parent.transform)
        {
            if (child.gameObject.tag == tag)
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = color;
               
            }
        }
    }
}
