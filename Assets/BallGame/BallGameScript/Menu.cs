using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    GameObject[] button;
    public GameObject[] gameMode = new GameObject[2];
    public GameObject[] numOfPlayers = new GameObject[4];
    public GameObject god;
    private GameObject[] players;

    void Start () {
        
        button = GameObject.FindGameObjectsWithTag("Button");
        players = GameObject.FindGameObjectsWithTag("Player");   
        Time.timeScale = 0;
    }
	
    public void playGame()
    {
        foreach (GameObject item in button)
        {
            item.SetActive(false);
        }
        gameMode[0].SetActive(true);
        gameMode[1].SetActive(true);
    }

    public void setGameModeFFA()
    {
        God godScript = god.GetComponent<God>();
        godScript.gameMode = "FFS";
        gameMode[0].SetActive(false);
        gameMode[1].SetActive(false);
        foreach (GameObject item in numOfPlayers)
        {
            item.SetActive(true);
        }
    }

    public void setGameModeTeams()
    {
        God godScript = god.GetComponent<God>();
        godScript.gameMode = "Teams";
        gameMode[0].SetActive(false);
        gameMode[1].SetActive(false);
        foreach (GameObject item in numOfPlayers)
        {
            item.SetActive(true);
        }
    }

    public void onePlayer()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].name == "Player_1")
            {
                Movement movement;
                movement = players[i].GetComponent<Movement>();
                movement.isAI = false;
                movement.moveSpeed = 6;
            }
        }
        foreach (GameObject item in numOfPlayers)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void twoPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == "Player_1" || players[i].name == "Player_2")
            {
                Movement movement;
                movement = players[i].GetComponent<Movement>();
                movement.isAI = false;
                movement.moveSpeed = 6;
            }
        }
        foreach (GameObject item in numOfPlayers)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void threePlayer()
    {
        print("Does nothing yet!");
    }

    public void fourPlayer()
    {
        print("Does nothing yet!");
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
