using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    public GameObject inventoryBoard;
    public GameObject selectedItem;
    public GameObject priceDisplay;


    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Balance: " + FindObjectOfType<BalanaceTracker>().balance;
    }

    public void buyOutfit()
    {
        if (selectedItem)
        {
            GameObject EmptySpace = inventoryBoard.transform.Find("EmptySpace").gameObject;
            GameObject newInventoryItem = EmptySpace.transform.GetChild(0).gameObject;
            newInventoryItem.SetActive(true);
            newInventoryItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(selectedItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)/2).ToString();
            newInventoryItem.transform.GetChild(1).GetComponent<Image>().sprite = selectedItem.transform.GetChild(1).GetComponent<Image>().sprite;
            newInventoryItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = selectedItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;

            EmptySpace.name = "UsedSpace";
            FindObjectOfType<BalanaceTracker>().balance -= priceDisplay.GetComponent<PriceDisplay>().price;
            priceDisplay.GetComponent<PriceDisplay>().price = 0;
            selectedItem.SetActive(false);
        }
    }

    public void sellOutfit()
    {
        FindObjectOfType<BalanaceTracker>().balance += priceDisplay.GetComponent<PriceDisplay>().price;
        selectedItem.SetActive(false);
    }
    public void changeSelectedItem(GameObject newSelectedItem)
    {
        selectedItem = newSelectedItem;
    }
}
