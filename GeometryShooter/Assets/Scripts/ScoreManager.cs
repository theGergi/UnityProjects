using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class ScoreManager : MonoBehaviour
{
    private const string highscoreURL = "https://asteroidshooter1.000webhostapp.com/site.php";

    private static ScoreManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public List<Score> RetrieveScores()
    {
        List<Score> scores = new List<Score>();
        StartCoroutine(DoRetrieveScores(scores));
        return scores;
    }

    public void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }

    IEnumerator DoRetrieveScores(List<Score> scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError)
            if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }
                        scores.Add(entry);
                    }
                }
            }
        }
    }

    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError)
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }
}

public struct Score
{
    public string name;
    public int score;
}
