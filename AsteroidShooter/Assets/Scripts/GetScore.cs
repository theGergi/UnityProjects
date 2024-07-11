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
        scores = FindObjectOfType<Leaderboard>().scores;
        for(int i = 0; i < scores.Count; i++)
        {
            if(scores[i].score == playerScore && scores[i].name == PlayerPrefs.GetString("name"))
            {
                //playerName = scores[i].name;
                place = i + 1;
            }
        }
        GetComponent<Text>().text = place.ToString() + "#  " + playerScore;
    }
}
