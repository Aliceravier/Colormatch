using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	
    public string horizontalMovInput;
    public string verticalMovInput;
    public Team playerTeam = Team.blue;
    public List<int> values;
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHori = Input.GetAxisRaw(horizontalMovInput);
        float moveVert = Input.GetAxisRaw(verticalMovInput);
        Vector2 movement = new Vector2(moveHori, moveVert);
        rb.velocity = speed*movement;

        //walking animation set
        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("PlayerMoving", true);
        }
        else
            anim.SetBool("PlayerMoving", false);

        //rotate player
        if (moveHori > 0)//right
        {
            rb.MoveRotation(90);
        }
        if (moveHori < 0)//left
        {
            rb.MoveRotation(270);
        }

        if (moveVert > 0)//up
        {
            rb.MoveRotation(0);
        }
        if (moveVert < 0)//down
        {
            rb.MoveRotation(180);
        }
    }
}
