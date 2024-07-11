using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(selfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    
}
