using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{
    public void ChangeOutfit(GameObject outfit)
    {
        GetComponent<Image>().sprite = outfit.GetComponent<Image>().sprite;
    }
}
