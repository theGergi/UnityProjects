using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject LassoInstantiator;
    public GameObject gameManager;
    private void OnMouseDown()
    {
        ;
        if (transform.position == LassoInstantiator.GetComponent<Lasso>().lasso1Tile || transform.position == LassoInstantiator.GetComponent<Lasso>().lasso2Tile || transform.position == LassoInstantiator.GetComponent<Lasso>().lasso3Tile)
            LassoInstantiator.GetComponent<Lasso>().startPosition = transform.position;
    }
    private void OnMouseDrag()
    {

        LassoInstantiator.GetComponent<Lasso>().instantiating = true;
    }
    private void OnMouseUp()
    {
        LassoInstantiator.GetComponent<Lasso>().instantiating = false;
        LassoInstantiator.GetComponent<Lasso>().endPosition = new Vector3(0, 0, 0);
        LassoInstantiator.GetComponent<Lasso>().startPosition = new Vector3(0, 0, 0);
    }
    private void OnMouseEnter()
    {
        
        if (Input.GetMouseButton(0) && FindObjectOfType<Time_Manager>().time < 119)
            if (transform.position != LassoInstantiator.GetComponent<Lasso>().startPosition && !LassoInstantiator.GetComponent<Lasso>().visitedPositions.Contains(transform.position)
                && (transform.position.x == LassoInstantiator.GetComponent<Lasso>().startPosition.x || transform.position.y == LassoInstantiator.GetComponent<Lasso>().startPosition.y))
            {
                LassoInstantiator.GetComponent<Lasso>().endPosition = transform.position;
            }
            else
            {
                LassoInstantiator.GetComponent<Lasso>().startPosition = new Vector3(0, 0, 0);
            }
    }
}
