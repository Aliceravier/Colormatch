using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonBehaviour : ExtendedBehaviour {

    LockBehaviour locker;

    private Animator anim;
    private GameObject button;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        locker = transform.GetChild(0).GetComponent<LockBehaviour>();
        
	}
	
	// Update is called once per frame

    void OnTriggerEnter2D(Collider2D c)
    {
		//if player touches button
		if (c.transform.gameObject.CompareTag("Player") && anim.GetBool("ButtonOn") == false)
        {
            //change anim + change room colour
            GameObject player = c.transform.gameObject;
            anim.SetBool ("ButtonOn", true); //changes anim to pushed
			Wait (0.5f, () => { //changes room color				           
				setRoom (player);
			});

			updateInfo (player);
                    
        }
    }

    public void makeLock()
    {
        Instantiate(locker, transform);
    }
    
	void setRoom(GameObject player){//RECOTJJAJNFJOR COLLOOUR
		/* Takes a player, sets the room to be their team and colour */
		RoomBehaviour rm = transform.parent.GetComponent<RoomBehaviour> ();
		rm.setRoomTeam (player.GetComponent<Health> ().getTeam());
		rm.ChangeTiles(player.GetComponent<SpriteRenderer>().color, "Floor");

	}

	void updateInfo(GameObject player){
		/*Tells God what room was got by which player*/
		GameObject god = GameObject.FindGameObjectWithTag("God");
		whoWon ww = god.GetComponent<whoWon>();
		int roomValue = this.GetComponentInParent<RoomBehaviour>().roomValue;
		ww.updateInfo(player, roomValue);  //updates the players' value tables and checks if someone won  
	}


}
