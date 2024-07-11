using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject gameScreen, gameManager;
    private void OnMouseOver()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        if(Input.GetMouseButtonDown(0))
        {
            gameScreen.SetActive(true);
            gameManager.GetComponent<Time_Manager>().startTimer();

            gameObject.transform.parent.gameObject.SetActive(false);


        }
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
