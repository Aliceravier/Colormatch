using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : ExtendedBehaviour {
	
    public string horizontalMovInput;
    public string verticalMovInput;
    public string rotationx;
    public string rotationy;
	public string StabInput;

	Team playerTeam;
    public List<int> values;
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;

    private bool canReswing = true;
	private SpriteRenderer renderer;
    private Collider2D collider;
    private Rigidbody2D body;
    private Camera cam;

	[HideInInspector]
    public bool canMove = true;

	float moveHori = 0;
	float moveVert = 0;
    float rotx = 0;
    float roty = 0;

	bool isDead;
    bool stopKilling = false;

    // Use this for initialization
    void Start()
    {
        if (name == "Player")
            cam = GameObject.FindGameObjectWithTag("Camera1").GetComponent<Camera>();
        if (name == "Player2")
            cam = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>();
    }
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
		playerTeam = GetComponent<Health> ().getTeam ();
    }

    void killPlayer()
    {        
        collider.enabled = false;
        renderer.enabled = false;
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
            renderer.enabled = true;
            collider.enabled = true;            
			GetComponent<Health>().setUp();
            stopKilling = false;
        });

    }

	void Update(){
		isDead = GetComponent<Health>().getDeath ();
		moveHori = 0;
		moveVert = 0;
		if (canMove)
		{
			moveHori = Input.GetAxisRaw(horizontalMovInput);
			moveVert = Input.GetAxisRaw(verticalMovInput);
            rotx = Input.GetAxis(rotationx);
            roty = Input.GetAxis(rotationy);
		}
		if (Input.GetAxis (StabInput) > 0.8f && !anim.GetBool("isSwing") && !isDead && canReswing)
			anim.SetTrigger ("isSwing");

        if (Input.GetAxis(StabInput) > 0.8)
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
        {
            if (Mathf.Abs(rotx) > 0.01f && Mathf.Abs(roty) > 0.01f)
            {
                float angle = Mathf.Atan2(roty, rotx) * Mathf.Rad2Deg;
                //rotate by that angle plus 90° to get player face rather than side facing 
                rb.MoveRotation(angle - 90);
            }

            
            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        }
        
    }

}
