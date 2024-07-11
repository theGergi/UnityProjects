using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private Plane dragPlane;

    private Vector3 offset;

    private Camera myMainCamera;
    //top 3 are about draging

    public Vector3 originalPosition;
    public Vector3 originalScale;
    private Vector3 enlargedPosition;
    private float cardHeight = 0.23f;
    private float cardWidth = 0.23f;
    public int originalSort;
    public Vector3[] emptySpaces;
    public bool cardSet = false;
    private bool cardPicked = false;

    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    public GameObject WinLabel;

    private void Start()
    {
        originalSort = GetComponent<SpriteRenderer>().sortingOrder;
        originalPosition = transform.position;
        enlargedPosition = originalPosition;
        enlargedPosition.y += 0.51f;
        enlargedPosition.x += 0.3f;
        emptySpaces = new[] { new Vector3(-1.942f, 0.072f, 0f), new Vector3(-0.125f, 0.063f, 0f), new Vector3(1.695f, 0.067f, 0f) };
        originalScale = transform.localScale;
        
        myMainCamera = Camera.main;
    }

    private void Update()
    {
        if (!cardSet)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.463781f, 10.56f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-2.647465f, 0f);

        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(6.724663f, 10.56f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.01702428f, 0f);

        }

        if (transform.GetSiblingIndex() == 12 || (transform.GetSiblingIndex() == 11 && transform.parent.GetChild(12).GetComponent<CardScript>().cardSet)
            || (transform.GetSiblingIndex() == 10 && transform.parent.GetChild(11).GetComponent<CardScript>().cardSet && transform.parent.GetChild(12).GetComponent<CardScript>().cardSet))
        {
            GetComponent<BoxCollider2D>().size = new Vector2(6.724663f, 10.56f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.01702428f, 0f);
        }
        if (cardSet && !Input.GetMouseButton(0))
            GetComponent<SpriteRenderer>().sortingOrder = 0;

        if(cardPicked)
            GetComponent<SpriteRenderer>().sortingOrder = 100;
        
        if (GameOver())
            GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseOver()
    {
        if (!cardSet && !Input.GetMouseButton(0))
        {
            
            transform.localScale = new Vector3(cardWidth, cardHeight, 1);
            transform.position = enlargedPosition;
            
        }
        for (int i = 0; i < 3; i++)
            if (transform.position == emptySpaces[i])
                if (Input.GetMouseButtonDown(0))
                    FindObjectOfType<ResetButton>().emptySpacesTeaken[i] = false;


    }

    private void OnMouseEnter()
    {
        if (!cardSet && !Input.GetMouseButton(0))
            FindObjectOfType<AudioManager>().Play("CardRoll");
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sortingOrder = originalSort;
        if (!cardSet && !Input.GetMouseButton(0))
        {
            transform.position = originalPosition;
            transform.localScale = originalScale;
        }

    }

    void OnMouseDown()
    {
        cardPicked = true;
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {

 


        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }

    private void OnMouseUp()
    {
        cardPicked = false;
        cardSnap();
        if (GameOver())
            StartCoroutine(Win());
    }

    public bool GameOver()
    {
        if (Card1.transform.position == FindObjectOfType<CardScript>().emptySpaces[0]
           && Card2.transform.position == FindObjectOfType<CardScript>().emptySpaces[1]
           && Card3.transform.position == FindObjectOfType<CardScript>().emptySpaces[2])
            return true;
        return false;
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(.5f);

        WinLabel.SetActive(true);
    }


    private void cardSnap()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(transform.position.x - emptySpaces[i].x) <= 0.5 && Mathf.Abs(transform.position.y - emptySpaces[i].y) <= 0.5 && FindObjectOfType<ResetButton>().emptySpacesTeaken[i] == false)
            {
                transform.position = emptySpaces[i];
                FindObjectOfType<AudioManager>().Play("CardSlap");
                FindObjectOfType<ResetButton>().emptySpacesTeaken[i] = true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (transform.position == emptySpaces[i])
            {
                cardSet = true;
                break;
            }
            cardSet = false;

        }
        if (!cardSet)
        {
            transform.position = originalPosition;
            transform.localScale = originalScale;
            GetComponent<SpriteRenderer>().sortingOrder = originalSort;
        }
    }

}

//collider small size 1.463781, 10.56 offset -2.647465, 0
//collider big size 6.724663, 10.56 offset -0.01702428,0
