using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerBehaviour : ExtendedBehaviour {
    public float Threshold = 30f;
    public PlayerID _playerID;
    Team playerTeam;
    public List<int> values;
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;
    public float rotateSpeed = 2;
    
	private SpriteRenderer sr;
    private Collider2D c;
    private bool canReswing = true;
    private Rigidbody2D body;
    private Transform crosshair;

	[HideInInspector]
    public bool canMove = true;

    Vector2 movement;
    public Vector2 stickRotation;

	bool isDead;
    bool stopKilling = false;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
		c = GetComponent<BoxCollider2D>();
		playerTeam = GetComponent<Health> ().getTeam ();
        crosshair = transform.Find("Crosshair");
    }

    void killPlayer()
    {   
		Pause (0.1f,0.1f);
        c.enabled = false;
        sr.enabled = false;
        canMove = false;
    }

    public void spawnPlayer()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag(playerTeam.ToString() + "Spawn");
        transform.position = spawn.transform.position;
    }

    public void respawnPlayer()
    {
        stopKilling = true;
        Wait(1, () => {
            //place player at respawn 
			GetComponent<Health> ().setDeath (false);
            GameObject spawn = GameObject.FindGameObjectWithTag(playerTeam.ToString() + "Spawn");
            transform.position = spawn.transform.position;
            //make player visible
            canMove = true;
            sr.enabled = true;
            c.enabled = true;            
			GetComponent<Health>().setUp();
            stopKilling = false;
        });

    }

	void Update(){
        //if controller, use triggers
        stickRotation = new Vector2(InputManager.GetAxisRaw("LookHorizontal", _playerID), InputManager.GetAxisRaw("LookVertical", _playerID));
        if (stickRotation.sqrMagnitude > 1.0f) stickRotation.Normalize(); //makes stick move circular :）


        isDead = GetComponent<Health>().getDeath ();

        movement = new Vector2(InputManager.GetAxisRaw("Horizontal", _playerID),InputManager.GetAxisRaw("Vertical", _playerID));
        Debug.Log(movement.magnitude);
        if (movement.magnitude < 0.19f)
        {
            movement = Vector2.zero;
        }
        if (isStabbing() && !anim.GetBool("isSwing") && !isDead && canReswing)
        {
            anim.SetTrigger("isSwing");
        }

        if (isStabbing())
            canReswing = false;
        else
            canReswing = true;

		if (isDead && !stopKilling)
		{
			killPlayer();
			respawnPlayer();
		}

	}


	// Update is called once per frame
	void FixedUpdate () {
        if (!canMove) return;

        rb.velocity = speed*movement;
 

        //walking animation set
        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("PlayerMoving", true);
        }
        else
            anim.SetBool("PlayerMoving", false);


        //rotate player
        Vector3 targetPoint = crosshair.position - transform.position;
        float targetAngle = Vector2.Angle(transform.up, targetPoint);
        Vector3 cross = Vector3.Cross(transform.up, targetPoint);
        Debug.Log("Torque is " + (targetAngle * cross.z * rotateSpeed));
        rb.AddTorque(targetAngle * cross.z * rotateSpeed);
        
    }


    bool isStabbing()
    {
        if (InputHelper.isControllerInput(_playerID))
        {
            return InputManager.GetAxis("Slash", _playerID) != 0;
        } else
        {
            return InputManager.GetButton("Slash", _playerID);
        }
    }

}
