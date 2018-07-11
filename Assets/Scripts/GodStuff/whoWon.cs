using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class whoWon : ExtendedBehaviour {
	public static Team winningTeam;
	public static Team losingTeam;
    bool gameIsWon = false;

    // Use this for initialization
    void Start() {
        //gameOverText.text = "something";
    }

        //public text gameOverText; 


	
	// Update is called once per frame
	void Update () {
		
	}

    void playerWinState(GameObject player)
    {
        
        List<int> values = player.GetComponent<PlayerBehaviour>().values;
        foreach (int value1 in values)
        {
            foreach (int value2 in values)
            {
                if (value2 == value1)
                    continue;
                foreach (int value3 in values)
                {
                    if (value3 == value2 || value3 == value1)
                        continue;
                    if (value1 + value2 + value3 == 15 && !gameIsWon)
                    {
                        gameIsWon = true;
						winningTeam = player.GetComponent<Health> ().getTeam (); //REFACTOR LATER OK :)
						losingTeam = findOtherPlayer (player).GetComponent<Health> ().getTeam();   
                            Wait(0.5f, () =>
                            {
                                SceneManager.LoadScene("word screen");
                            });


                        
                        return;
                    }
                }
            }
        }
    }

    public void updateInfo(GameObject player, int roomValue)
    //updates the players' value tables and checks if someone won  
    {
        player.GetComponent<PlayerBehaviour>().values.Add(roomValue);
        GameObject otherPlayer = findOtherPlayer(player);
        otherPlayer.GetComponent<PlayerBehaviour>().values.Remove(roomValue);
        playerWinState(player);
    }

    void endGame(GameObject player)
    {
    //gameOverText.text = "Game Over!";
}

    public GameObject findOtherPlayer(GameObject player)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject person in players)
        {
            if (person != player)
                return person;
        }
        return null;
    }

	public Team getWinningTeam(){
		return winningTeam;
	}
}

