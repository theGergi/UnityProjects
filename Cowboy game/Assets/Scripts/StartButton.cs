using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject gameManager, outlawGroup, reloadButton;
    private void OnMouseOver()
    {
        transform.localScale = new Vector3(2.1f, 2.1f, 1f);
        if(Input.GetMouseButtonDown(0))
        {
            outlawGroup.SetActive(true);
            gameManager.GetComponent<Time_Manager>().startTimer();
            reloadButton.GetComponent<BoxCollider2D>().enabled = true;

            gameObject.transform.parent.gameObject.SetActive(false);


        }
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(2f, 2f, 1f);
    }
}
