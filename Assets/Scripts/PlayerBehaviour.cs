using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : ExtendedBehaviour {
	
    public string horizontalMovInput;
    public string verticalMovInput;
    public Team playerTeam = Team.blue;
    public List<int> values;
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;
    public bool isDead;
    private SpriteRenderer renderer;
    private Collider2D collider;
    private Rigidbody2D body;
    private bool canMove = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }
	
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        isDead = true;
    }

    void killPlayer()
    {        
        collider.enabled = false;
        renderer.enabled = false;
        canMove = false;
    }

    void respawnPlayer()
    {
        Wait(1, () => {
            //place player at respawn 
            GameObject spawn = GameObject.FindGameObjectWithTag(playerTeam.ToString() + "Spawn");
            transform.position = spawn.transform.position;
            //make player visible
            renderer.enabled = true;
            collider.enabled = true;
            canMove = true;
        });
        isDead = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        float moveHori = 0;
        float moveVert = 0;
        if (canMove)
        {
            moveHori = Input.GetAxisRaw(horizontalMovInput);
            moveVert = Input.GetAxisRaw(verticalMovInput);
        }
        Vector2 movement = new Vector2(moveHori, moveVert);
        rb.velocity = speed*movement;

        //kill and respawn player
        if (isDead)
        {
            //erase player
            killPlayer();
            respawnPlayer();
            

        }


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
