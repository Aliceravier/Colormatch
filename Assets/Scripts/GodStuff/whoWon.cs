using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class whoWon : ExtendedBehaviour {
	public static Team winningTeam;
	public static Team losingTeam;

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
        List<int> values = player.GetComponent<Movement>().values;
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
                    if (value1 + value2 + value3 == 15)
                    {
						winningTeam = player.GetComponent<Movement> ().playerTeam; //REFACTOR LATER OK :)
						losingTeam = findOtherPlayer (player).GetComponent<Movement> ().playerTeam;
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
        player.GetComponent<Movement>().values.Add(roomValue);
        GameObject otherPlayer = findOtherPlayer(player);
        otherPlayer.GetComponent<Movement>().values.Remove(roomValue);
        playerWinState(player);
    }

    void endGame(GameObject player)
    {
    //gameOverText.text = "Game Over!";
}

    GameObject findOtherPlayer(GameObject player)
    {
		if (player.GetComponent<Movement> ().playerTeam == Team.blue)
			return getPlayerOfTeam (Team.green);
		else
			return getPlayerOfTeam (Team.blue);
    }

	public Team getWinningTeam(){
		return winningTeam;
	}
}

