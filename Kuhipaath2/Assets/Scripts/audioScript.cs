using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour
{
    
    void Start()
    {
        InvokeRepeating("playSound", 0.0001f,5f);
    }

    void playSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
