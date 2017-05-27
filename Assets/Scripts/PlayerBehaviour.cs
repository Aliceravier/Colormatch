using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : ExtendedBehaviour {
	
    public string horizontalMovInput;
    public string verticalMovInput;
	public string StabInput;

    public Team playerTeam = Team.blue;
    public List<int> values;
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;
    
	private SpriteRenderer renderer;
    private Collider2D collider;
    private Rigidbody2D body;

	[HideInInspector]
    public bool canMove = true;

	float moveHori = 0;
	float moveVert = 0;

	bool isDead;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
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
			GetComponent<Health>().setUp();
        });
		GetComponent<Health> ().setDeath (false);
    }

	void Update(){
		isDead = GetComponent<Health>().getDeath ();
		moveHori = 0;
		moveVert = 0;
		if (canMove)
		{
			moveHori = Input.GetAxisRaw(horizontalMovInput);
			moveVert = Input.GetAxisRaw(verticalMovInput);
		}
		if (Input.GetButtonDown (StabInput) && !anim.GetBool("isSwing") && !isDead)
			anim.SetTrigger ("isSwing");


		if (isDead)
		{
			killPlayer();
			respawnPlayer();
		}

	}


	// Update is called once per frame
	void FixedUpdate () {
        
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
            rb.MoveRotation(270);
        }
        if (moveHori < 0)//left
        {
            rb.MoveRotation(90);
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
