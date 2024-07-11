using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject Cards;

    public GameObject WinLabel;

    public bool[] emptySpacesTeaken;

    private void Start()
    {
        WinLabel.SetActive(false);
        emptySpacesTeaken = new bool[]{false, false, false};
    }




    private void OnMouseOver()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            WinLabel.SetActive(false);
            transform.localScale = new Vector3(1.1f, 1.1f, 1);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 13; j++)
                {
                    GameObject Card = Cards.transform.GetChild(i).GetChild(j).gameObject;
                    Card.transform.position = Card.GetComponent<CardScript>().originalPosition;
                    Card.transform.localScale = Card.GetComponent<CardScript>().originalScale;
                    Card.GetComponent<SpriteRenderer>().sortingOrder = Card.GetComponent<CardScript>().originalSort;
                    Card.GetComponent<CardScript>().cardSet = false;
                    Card.GetComponent<BoxCollider2D>().enabled = true;
                }
            FindObjectOfType<AudioManager>().Play("CardShuffle");
            for (int i = 0; i < 3; i++)
                emptySpacesTeaken[i] = false;
        }

    }
    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    
}
