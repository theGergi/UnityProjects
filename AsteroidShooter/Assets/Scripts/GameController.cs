using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public System.Random rand = new System.Random();
    public Camera MainCamera;
    public GameObject[] AsteroidPrefabs;
    public GameObject[] lives;
    public Text score;
    

    private int spawnX, spawnY;
    private int spawnYMin, spawnYMax;
    private int spawnXMin, spawnXMax;
    private int side;

    public float gameSpeed = 10f;
    public int XP;
    public int livesNum;
    public bool gameover = false;
    private bool[] stages;
    private int stage5Counter;
    void Start()
    {
        Time.timeScale = 1;
        gameover = false;
        livesNum = 3;
        XP = 0;
        gameSpeed = 1f;
        rand = new System.Random();
        stages = new bool[] { true, true, true, true};
        stage5Counter = 0;
    }

    private void stage1()
    {

        for (int i = 0; i < 10; i++)
            SpaceDogeSpawn(0);

    }
    private void stage2()
    {
        for (int i = 0; i < 5; i++)
            SpaceDogeSpawn(0);
        for (int i = 0; i < 5; i++)
            SpaceDogeSpawn(3);
        
    }
    private void stage3()
    {
        for (int i = 0; i < 4; i++)
            SpaceDogeSpawn(0);
        for (int i = 0; i < 3; i++)
            SpaceDogeSpawn(3);
        for (int i = 0; i < 3; i++)
            SpaceDogeSpawn(5);
        
    }
    private void stage4()
    {
        for (int i = 0; i < 4; i++)
            SpaceDogeSpawn(3);
        for (int i = 0; i < 3; i++)
            SpaceDogeSpawn(5);
        for (int i = 0; i < 2; i++)
            SpaceDogeSpawn(2);
        
    }
    private void stage5()
    {
        for (int i = 0; i < 4 + stage5Counter; i++)
            SpaceDogeSpawn(3);
        for (int i = 0; i < 3 + stage5Counter; i++)
            SpaceDogeSpawn(5);
        for (int i = 0; i < 2 + stage5Counter; i++)
            SpaceDogeSpawn(2);
        for (int i = 0; i < 4 + stage5Counter; i++)
            SpaceDogeSpawn(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("AsteroidsHolder").transform.childCount == 0)
        {
            if (stages[0])
            {
                stage1();
                stages[0] = false;
            }
            else if (stages[1] && !stages[0])
            {
                stage2();
                stages[1] = false;
            }
            else if (stages[2] && !stages[1])
            {
                stage3();
                stages[2] = false;
            }
            else if (stages[3] && !stages[2])
            {
                stage4();
                stages[3] = false;
            }
            else if (!stages[3])
            {
                stage5();
                stage5Counter++;
            }
        }

    }
    

    public void loseLife()
    {
        livesNum -= 1;
        if(livesNum < 3 && lives[0].activeInHierarchy)
        {
            lives[0].SetActive(false);
        }
        if (livesNum < 2 && lives[1].activeInHierarchy)
        {
            lives[1].SetActive(false);
        }
        if (livesNum < 1 && lives[2].activeInHierarchy)
        {
            lives[2].SetActive(false);
            gameOver();
        }
        
    }
    private void SpaceDogeSpawn(int i)
    {
        
        spawnYMin = (int)(MainCamera.transform.position.y - MainCamera.orthographicSize);
        spawnYMax = (int)(MainCamera.transform.position.y + MainCamera.orthographicSize);
        spawnXMin = (int)(MainCamera.transform.position.x - MainCamera.orthographicSize * 1.6f);
        spawnXMax = (int)(MainCamera.transform.position.x + MainCamera.orthographicSize * 1.6f);
        side = rand.Next(1, 5);
        if (side == 1)
        {
            spawnY = rand.Next(spawnYMin, spawnYMax);
            spawnX = spawnXMax + 2;
        }
        else if (side == 2)
        {
            spawnY = rand.Next(spawnYMin, spawnYMax);
            spawnX = spawnXMin - 2;
        }
        else if (side == 3)
        {
            spawnY = spawnYMin - 2;
            spawnX = rand.Next(spawnXMin, spawnXMax);
        }
        else if (side == 4)
        {
            spawnY = spawnYMax + 2;
            spawnX = rand.Next(spawnXMin, spawnXMax);
        }

            
            
        GameObject asteroid = Instantiate(AsteroidPrefabs[i], new Vector3(spawnX, spawnY, 0), Quaternion.identity, GameObject.Find("AsteroidsHolder").transform);
        float velocityX = rand.Next(spawnXMin, spawnXMax) - asteroid.transform.position.x;
        float velocityY = rand.Next(spawnYMin, spawnYMax) - asteroid.transform.position.y;
        float scalar = Mathf.Sqrt(Mathf.Pow(velocityX, 2) + Mathf.Pow(velocityY, 2));
        Vector2 velocity = (new Vector2(velocityX, velocityY) / scalar) * asteroid.GetComponent<AsteroidScript>().asteroidSpeed;
        asteroid.GetComponent<Rigidbody2D>().velocity = velocity;
            
        //Debug.Log(velocityX + " " + velocityY + " " + scalar + " " + AsteroidPrefabs[i].GetComponent<AsteroidScript>().asteroidSpeed);
        //Debug.Log(new Vector2(velocityX, velocityY) / scalar * AsteroidPrefabs[i].GetComponent<AsteroidScript>().asteroidSpeed);
        
    }
    private void SantaCoin(int i)
    {
        while (!gameover)
        {
            spawnYMin = (int)(MainCamera.transform.position.y - MainCamera.orthographicSize);
            spawnYMax = (int)(MainCamera.transform.position.y + MainCamera.orthographicSize);
            spawnXMin = (int)(MainCamera.transform.position.x - MainCamera.orthographicSize * 1.6f);
            spawnXMax = (int)(MainCamera.transform.position.x + MainCamera.orthographicSize * 1.6f);

            spawnY = rand.Next(spawnYMin, spawnYMax);
            spawnX = rand.Next(spawnXMin, spawnXMax);


            Instantiate(AsteroidPrefabs[i], new Vector3(spawnX, spawnY, 0), Quaternion.identity, GameObject.Find("AsteroidsHolder").transform);
        }
    }
    

    public void gameOver()
    {
        StartCoroutine(gameOver1());
    }

    IEnumerator gameOver1()
    {
        //Time.timeScale = 0;
        PlayerPrefs.SetInt("score", XP);
        string name = PlayerPrefs.GetString("name");
        FindObjectOfType<ScoreManager>().PostScores(name, XP);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Menu");
        
    }

    public void addXP(int xp)
    {
        XP += xp;
        score.text = "Score " + XP;
    }
}
