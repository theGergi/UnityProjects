using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal : MonoBehaviour
{
    public GameObject Cards;
    public GameObject resetButton;

    private void Start()
    {
        Cards.SetActive(false);
        resetButton.SetActive(false);
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 13; j++)
                Cards.transform.GetChild(i).GetChild(j).GetComponent<CardScript>().enabled = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dealt());
        }
    }

    IEnumerator Dealt()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1);
        FindObjectOfType<AudioManager>().Play("CardShuffle");
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 13; j++)
                Cards.transform.GetChild(i).GetChild(j).GetComponent<CardScript>().enabled = true;

        transform.localScale = new Vector3(1f, 1f, 1);
        resetButton.SetActive(true);
        Cards.SetActive(true);
        Destroy(gameObject);
    }
}
