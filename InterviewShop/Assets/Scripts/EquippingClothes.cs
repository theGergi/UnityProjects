using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquippingClothes : MonoBehaviour
{
    public GameObject player;
    public GameObject selectedItem;
    public void equipClothes(Sprite defaultPlayer)
    {
        player.GetComponent<Animator>().enabled = false;
        if (selectedItem)
        {
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selectedItem.transform.GetChild(1).GetComponent<Image>().sprite;
            player.GetComponent<SpriteRenderer>().sprite = defaultPlayer;
        }
        player.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void changeSelectedItem(GameObject newSelectedItem)
    {
        selectedItem = newSelectedItem;
    }
}
