using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedTheAnimals : MonoBehaviour
{
    private Plane dragPlane;

    private Vector3 offset;

    private Camera myMainCamera;

    private Vector2 startPosition;
    private GameObject clickedFood;
    // Start is called before the first frame update
    void Start()
    {
        myMainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 tempFingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit && (hit.collider.tag == "Food" || hit.collider.tag == "CorrectFood"))
            {
                clickedFood = hit.collider.gameObject;
                startPosition = clickedFood.transform.position;
            }
                
        }

        if (Input.GetMouseButton(0) && clickedFood != null)
            dragScript();

        if (Input.GetMouseButtonUp(0) && clickedFood != null)
        {
            Ray ray = myMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << 7);
            if (hit)
                if (clickedFood.tag == "CorrectFood")
                {
                    Scene currentScene = SceneManager.GetActiveScene();
                    if (currentScene.name == "ZebraFeeding")
                    {
                        SceneManager.LoadScene("Feeding");
                    }
                    else
                        SceneManager.LoadScene(currentScene.buildIndex + 1);
                }
                    
                else
                    Debug.Log("Dislike");
            clickedFood.transform.position = startPosition;
            clickedFood = null;
        }
            


    }

    private void dragScript()
    {

        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray ray = myMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1<<7);


        float planeDist;
        dragPlane.Raycast(ray, out planeDist);
        clickedFood.transform.position = ray.GetPoint(planeDist);

        

    }
    
}
