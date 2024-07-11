using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public bool instantiating;
    public GameObject lasso;
    public Vector3[] visitedPositions = new Vector3[45];
    public Vector3 lasso1Tile, lasso2Tile, lasso3Tile;
    public GameObject BrownCowboy, RedCowboy, GreenCowboy;
    public GameObject BrownHorse, RedHorse, GreenHorse;
    private int visitedPositionsCounter;
    private GameObject prefabs;

    public GameObject grid;
    public GameObject win, loss;
    public GameObject resetButton;
    public void Start()
    {
        prefabs = GameObject.Find("Prefabs");
        lasso1Tile = BrownCowboy.transform.position;

        lasso2Tile = RedCowboy.transform.position;

        lasso3Tile = GreenCowboy.transform.position;

        startPosition = new Vector3(0, 0, 0);
        endPosition = new Vector3(0, 0, 0);
        instantiating = false;
        visitedPositionsCounter = 3;
        visitedPositions = new Vector3[45];
        visitedPositions[0] = lasso1Tile;
        visitedPositions[1] = lasso2Tile;
        visitedPositions[2] = lasso3Tile;
    }

    private void Update()
    {
        if (endPosition != new Vector3(0, 0, 0) && startPosition != new Vector3(0, 0, 0))
        {   
            if(startPosition.x==endPosition.x)
                Instantiate(lasso, Vector3.Lerp(startPosition, endPosition, 0.5f), Quaternion.Euler(0,0,90), prefabs.transform);
            else if (startPosition.y == endPosition.y)
                Instantiate(lasso, Vector3.Lerp(startPosition, endPosition, 0.5f), Quaternion.identity, prefabs.transform);

            if (startPosition == lasso1Tile)
                lasso1Tile = endPosition;
            else if (startPosition == lasso2Tile)
                lasso2Tile = endPosition;
            else if (startPosition == lasso3Tile)
                lasso3Tile = endPosition;

            visitedPositions[visitedPositionsCounter] = endPosition;
            visitedPositionsCounter += 1;
            startPosition = endPosition;
            endPosition = new Vector3(0, 0, 0);
        }
        if(lasso1Tile==BrownHorse.transform.position && lasso2Tile == RedHorse.transform.position && lasso3Tile == GreenHorse.transform.position)
            Win();
        if (grid.activeInHierarchy && Time_Manager.minutes == 0 && Time_Manager.seconds == 0)
            Lose();
    }

    
    private void Win()
    {
        resetButton.GetComponent<BoxCollider2D>().enabled = false;
        grid.SetActive(false);
        win.SetActive(true);
        Time.timeScale = 0;
    }
    private void Lose()
    {
        resetButton.GetComponent<BoxCollider2D>().enabled = false;
        grid.SetActive(false);
        loss.SetActive(true);
        Time.timeScale = 0;
    }
}

