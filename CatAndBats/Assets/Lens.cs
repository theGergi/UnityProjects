using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lens : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 mousePosition;
    public GameObject winLabel;
    public GameObject loseLabel;
    private float startingTime;
    public float time = 120;
    public TextMeshProUGUI timer;
    private bool gameOver = false;
    private float t;
    private string Minutes;
    private string Seconds;
    public float delay = 0.5f;
    private int Chances;

    private void Start()
    {
        Chances = 3;
        startingTime = Time.time;

        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }
    private void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -2f;
        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "CatCircle")
                    StartCoroutine(Win());
                
            }
            else
            {
                Chances -= 1;
                //FAILED ATTEMPT
            }
                
        }
        if (Chances == 0)
            StartCoroutine(Lose());

        if (!gameOver)
        {
            t = time - (Time.time - startingTime);
            Minutes = ((int)t / 60).ToString();
            if((int)t % 60 <=9)
                Seconds = "0"+((int)t % 60).ToString();
            else
                Seconds =((int)t % 60).ToString();
        }

        timer.text = Minutes + ":" + Seconds;
        if (t <= 0)
            StartCoroutine(Lose());
        
    }
    IEnumerator Win()
    {
        gameOver = true;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        winLabel.SetActive(true);
        
    }
    IEnumerator Lose()
    {
        gameOver = true;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        loseLabel.SetActive(true);
        
    }
}
