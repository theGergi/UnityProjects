using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceDisplay : MonoBehaviour
{
    public int price;
    void Start()
    {
        price = 0;
    }

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Price: " + price;
    }
    public void priceChange(TextMeshProUGUI itemPrice)
    {
        price = int.Parse(itemPrice.text);
    }
}
