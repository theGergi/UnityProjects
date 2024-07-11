using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    private int playerScore;
    //private string playerName;
    private int place;
    List<Score> scores;
    void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Text>().text = playerScore.ToString();
    }
}
