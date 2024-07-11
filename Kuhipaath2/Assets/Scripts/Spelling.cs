using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Spelling : MonoBehaviour
{
    private Plane dragPlane;

    private Vector3 offset;

    private Camera myMainCamera;

    private Vector2 startPosition;
    private GameObject spaceship;
    public Transform[] spaceships;
    public Transform[] stands;
    public Sprite[] litStands;
    private int i = 0;
    private int n;

    private AudioSource voiceClip;
    // Start is called before the first frame update
    void Start()
    {
        voiceClip = GetComponent<AudioSource>();
        voiceClip.enabled = false;

        n = spaceships.Length;
        i = 0;
        myMainCamera = Camera.main;
        Advertisement.Initialize("4437229");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit)
            {
                spaceship = hit.collider.gameObject;
                startPosition = spaceship.transform.position;
            }

        }

        if (Input.GetMouseButton(0) && spaceship != null)
            dragScript();

        

        if (Input.GetMouseButtonUp(0) && spaceship != null)
        {
            Ray ray = myMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << 7);
            if (hit)
                if (hit.transform == stands[i] && spaceship.GetComponent<Image>().sprite == spaceships[i].GetComponent<Image>().sprite)
                {
                    if (spaceship.transform != spaceships[i])
                    {
                        spaceship.transform.position = spaceships[i].position;
                        startPosition = spaceships[i].position;
                    }
                    Destroy(spaceships[i].gameObject);
                    stands[i].GetComponent<Image>().sprite = litStands[i];
                    i++;
                    if(i == n)
                    {
                        StartCoroutine(changeScene());
                    }
                }
                
            spaceship.transform.position = startPosition;
            spaceship = null;
        }



    }

    

    IEnumerator changeScene()
    {
        voiceClip.enabled = true;

        yield return new WaitForSeconds(2f);

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Zebra")
        {
            SceneManager.LoadScene("Spelling");
        }
        else
            SceneManager.LoadScene(currentScene.buildIndex + 1);

        if (currentScene.buildIndex % 5 == 0 && Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
    }

    private void dragScript()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spaceship.transform.position = position;
    }

}
