using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CopyInventory : MonoBehaviour
{
    public GameObject copiedCell;
    private GameObject copiedItem;
    private GameObject item;

    private void OnEnable()
    {
        copiedItem = copiedCell.transform.GetChild(0).gameObject;
        item = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (copiedItem.activeSelf)
        {
            item.SetActive(true);
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = copiedItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            item.transform.GetChild(1).GetComponent<Image>().sprite = copiedItem.transform.GetChild(1).GetComponent<Image>().sprite;
            item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = copiedItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        }
        else if (item.activeSelf)
            item.SetActive(false);
    }
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }
}
