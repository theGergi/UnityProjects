using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Text winnerDisplay;
    public GameObject endScreen;
    public GameObject player1;
    public GameObject player2;

    private Vector3 positionPlayer1 = new Vector3(-7.5f, 0.5f, 0);
    private Vector3 positionPlayer2 = new Vector3(7.5f, 0.5f, 0);
    public void gameEnd(string player)
    {
        winnerDisplay.text = player + " won";
        endScreen.SetActive(true);
        Time.timeScale = 0;
        player1.GetComponent<PlayerController>().gameover = true;
        player2.GetComponent<PlayerController>().gameover = true;
    }

    public void restart()
    {
        player1.transform.position = positionPlayer1;
        player2.transform.position = positionPlayer2;
        endScreen.SetActive(false);
        Time.timeScale = 1;
        player1.GetComponent<PlayerController>().player1LivesCount = 3;
        player2.GetComponent<PlayerController>().player2LivesCount = 3;
        for(int i=0; i < 3; i++)
        {
            player1.GetComponent<PlayerController>().Player1Lives.transform.GetChild(i).gameObject.SetActive(true);
            player2.GetComponent<PlayerController>().Player2Lives.transform.GetChild(i).gameObject.SetActive(true);
        }
        player1.GetComponent<PlayerController>().gameover = false;
        player2.GetComponent<PlayerController>().gameover = false;
        

    }
}
