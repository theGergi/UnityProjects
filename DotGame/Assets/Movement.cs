using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 0, 0));
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Down");
            float deltaX;
            float deltaY;
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            Debug.Log("DeltaX = "+ deltaX + "/nDeltaY = " + deltaY);
            GetComponent<Rigidbody2D>().AddForce(new Vector3(1*Time.deltaTime, 1*Time.deltaTime*deltaY/deltaX, 0));
        }
        
        
    }

}
