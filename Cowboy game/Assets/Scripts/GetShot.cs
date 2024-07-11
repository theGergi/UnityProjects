using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetShot : MonoBehaviour
{

    public int bounty = 0;
    public GameObject BountyCounter;
    public bool isAlive = true;
    public GameObject DeadFace;
    public GameObject EmptyBullets;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    private void OnMouseOver()
    {
        bool GameOver = BountyCounter.GetComponent<MoneyCounter>().GameOver;
        if (isAlive && !GameOver)
        {

            this.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            if (Input.GetMouseButtonDown(0) && BountyCounter.GetComponent<MoneyCounter>().shots < 5)
            {
                EmptyBullets.transform.GetChild(BountyCounter.GetComponent<MoneyCounter>().shots).gameObject.SetActive(true);
                BountyCounter.GetComponent<MoneyCounter>().sum += bounty;
                BountyCounter.GetComponent<MoneyCounter>().shots += 1;
                isAlive = false;
                this.transform.localScale = new Vector3(1f, 1f, 1);
                StartCoroutine(PosterShot());
                DeadFace.SetActive(true);

                FindObjectOfType<AudioManager>().Play("Gunshot");
            }
        }
    }

    IEnumerator PosterShot()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        DeadFace.SetActive(false);
        isAlive = true;
    }

}
