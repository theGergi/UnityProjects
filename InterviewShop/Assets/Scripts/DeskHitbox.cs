using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskHitbox : MonoBehaviour
{
    public bool playerInside = false;
    private void Start()
    {
        playerInside = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInside = false;
    }
}
