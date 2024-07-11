using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{   
    public GameObject canvas;
    public void openBuyMenu()
    {
        canvas.transform.Find("BuyMenu").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void openSellMenu()
    {
        canvas.transform.Find("SellMenu").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void exitDialogBox()
    {
        canvas.SetActive(false);
    }
}
