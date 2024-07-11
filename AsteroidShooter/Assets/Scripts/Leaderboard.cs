using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Score> scores;
    string leaderboardText;
    string scoreText;
    void Start()
    {
        scores = FindObjectOfType<ScoreManager>().RetrieveScores();
    }

    // Update is called once per frame
    void Update()
    {
        leaderboardText = "";
        scoreText = "";
        if (scores.Count > 0)
        {
            for (int i = 0; i < Mathf.Min(10, scores.Count); i++)
            {
                string number;
                if (i != 9)
                {
                    number = (i + 1).ToString() + "#    ";
                }
                else
                    number = (i + 1).ToString() + "#  ";
                string name = scores[i].name + ":";
                string score = scores[i].score.ToString();
                string line = number + name.PadRight(20);
                leaderboardText += line + "\n";
                scoreText += score + "\n";
                transform.GetChild(0).GetComponent<Text>().text = scoreText;

            }
            GetComponent<Text>().text = leaderboardText;
        }
    }
}
