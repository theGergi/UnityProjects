using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    public int sum= 0;
    public int shots = 0;
    public GameObject WinLabel;
    public GameObject LoseLabel;
    public bool GameOver = false;
    public float WinDelayTime = 0.5f;
    public GameObject EmptyBullets;
    public GameObject StartScreen;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartScreen.activeSelf && Time_Manager.minutes == 0 && Time_Manager.seconds == 0 && !GameOver)
        {
            FindObjectOfType<MouseClick>().noTime = true;
            StartCoroutine(Lose());

        }
        if (sum==1000 && shots == 5)
        {
            FindObjectOfType<MouseClick>().noTime = true;
            StartCoroutine(Win());

        }
    }
    public void Setup()
    {
        StopAllCoroutines();
        WinLabel.SetActive(false);
        LoseLabel.SetActive(false);
        sum = 0;
        shots = 0;
        GameOver = false;
        for(int i = 0;i<5;i++)
            EmptyBullets.transform.GetChild(i).gameObject.SetActive(false);
    }

    IEnumerator Lose()
    {
        GameOver = true;
        yield return new WaitForSeconds(WinDelayTime);
        LoseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Win()
    {
        GameOver = true;
        yield return new WaitForSeconds(WinDelayTime);
        WinLabel.SetActive(true);
        Time.timeScale = 0;
    }
}
