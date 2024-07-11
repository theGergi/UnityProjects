using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using TMPro;

public class LineDraw : MonoBehaviour
{
    private AudioSource voiceClip;

    public int numLines;
    public int i;
    public GameObject linePrefab;
    public GameObject currentLine;
    public static LineRenderer lineRenderer;
    public List<Vector2> fingerPositions;
    public Transform[] boundsArray = new Transform[3];
    public float[] starRotations = new float[3];
    public bool[] turnedAround = new bool[3];
    private Transform border;
    private Transform bounds, start, end;
    public GameObject animal;
    private int layerMask;
    private bool animalMoving;
    public GameObject template;

    public Sprite background;
    public GameObject animalAnimation;
    public TextMeshProUGUI text;

    public bool enlargeAnimal;
    public Vector3 maxScale;

    private void Start()
    {
        voiceClip = GetComponent<AudioSource>();
        voiceClip.enabled = false;
        Advertisement.Initialize("4437229");
        
        text.fontSize = 0;
        animalMoving = false;
        layerMask = 1 << 6;
        i = 0;
        bounds = boundsArray[i];
        start = bounds.GetChild(0);
        end = bounds.GetChild(1);
        border = bounds.GetChild(2);
        animal.transform.position = start.position;
        if(animalAnimation != null)
        {
            maxScale = animalAnimation.transform.localScale;
            animalAnimation.GetComponent<Animator>().enabled = false;
            animalAnimation.SetActive(false);
        }
        if (turnedAround[i])
            animal.transform.rotation = Quaternion.Euler(0,180, starRotations[i]);
        else
            animal.transform.rotation = Quaternion.Euler(0, 0, starRotations[i]);
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(4f);

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Z")
            SceneManager.LoadScene("AlphabetTracingCapital");
        else if (currentScene.name == "zSmall")
            SceneManager.LoadScene("AlphabetTracingSmall");
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }

        if (currentScene.buildIndex % 5 == 0 && Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }

        enabled = false;
        Destroy(animal);
    }
    

    private IEnumerator enlarge()
    {
        Debug.Log("enlarging");
        float time = 2.2f;
        float i = 0.0f;
        float rate = (1.0f / time) * 2f;
        if (animalAnimation != null)
            animalAnimation.SetActive(true);
        while (i<1.0f)
        {
            i += Time.deltaTime * rate;
            text.fontSize = i*50f;

            if (enlargeAnimal)
            {
                
                animalAnimation.transform.localScale = Vector3.Lerp(new Vector3(0f,0f,0f),maxScale, i);
            }
                
            yield return null;
        }
        if (animalAnimation != null)
        {
            animalAnimation.transform.localScale = maxScale;
            animalAnimation.GetComponent<Animator>().enabled = true;
        }

        voiceClip.enabled = true;

        Debug.Log(maxScale);

    }

    void Update()
    {
        Vector2 tempFingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (Input.GetMouseButtonDown(0))
            if (hit.collider != null && hit.collider.transform == start)
                CreateLine(tempFingerPosition);




        if (Input.GetMouseButton(0))
        {
            if(hit.collider != null && hit.collider.transform.parent != null && hit.collider.transform.parent.name=="Dot")
            {
                CreateLine(tempFingerPosition);
                StartCoroutine(enlarge());



                template.GetComponent<SpriteRenderer>().sortingLayerName = "Animation";
                template.GetComponent<SpriteRenderer>().sprite = background;
                StartCoroutine(changeScene());
            }

            eatFruit(ray);
            if (hit.collider != null && lineRenderer != null && (hit.collider.transform == bounds || hit.collider.transform == start))
            {
                if (Vector2.Distance(tempFingerPosition, fingerPositions[fingerPositions.Count - 1]) > 0.2f)
                {
                    UpdateLine(tempFingerPosition);
                }
            }
            else if (hit.collider != null && hit.collider.transform == end && currentLine != null)
            {
                Debug.Log(hit.collider.name);
                if (numLines-1 == i)
                {
                    Debug.Log("numLines zero");
                    //transform.localScale = new Vector3(1.5f, 1.5f, 1);
                    if (template != null)
                    {
                        StartCoroutine(enlarge());
                        


                        template.GetComponent<SpriteRenderer>().sortingLayerName = "Animation";
                        template.GetComponent<SpriteRenderer>().sprite = background;
                        //transform.localScale = new Vector3(0.82f, 0.82f, 1);
                        
                        StartCoroutine(changeScene());
                    }
                    /*
                    else
                    {
                        voiceClip.enabled = true;
                        Debug.Log("changing scene");
                        Scene currentScene = SceneManager.GetActiveScene();
                        if (currentScene.name == "Z")
                            SceneManager.LoadScene("AlphabetTracingCapital");
                        else if (currentScene.name == "zSmall")
                            SceneManager.LoadScene("AlphabetTracingSmall");
                        else
                        {
                            SceneManager.LoadScene(currentScene.buildIndex + 1);
                        }

                        enabled = false;
                        Destroy(animal);
                    }
                    */
                    
                    
                }
                else
                {
                    i++;
                    bounds.GetComponent<Collider2D>().enabled = false;
                    start.GetComponent<Collider2D>().enabled = false;
                    end.GetComponent<Collider2D>().enabled = false;
                    lineRenderer.sortingLayerName = "Finished";
                    lineRenderer.sortingOrder = i * 2 - 1;
                    currentLine = null;
                    lineRenderer = null;
                    SpriteRenderer borderSprite = border.gameObject.GetComponent<SpriteRenderer>();
                    borderSprite.sortingLayerName = "Finished";
                    borderSprite.sortingOrder = i * 2;
                    if (border.childCount > 0)
                    {   
                        for(int r = 0; r < border.childCount; r++)
                        {
                            border.GetChild(r).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Finished";
                            border.GetChild(r).gameObject.GetComponent<SpriteRenderer>().sortingOrder = i * 2;
                        }
                    }
                    //bounds.GetComponent<Collider2D>().enabled = true;
                    //start.GetComponent<Collider2D>().enabled = true;
                    //end.GetComponent<Collider2D>().enabled = true;
                    
                    bounds = boundsArray[i];
                    start = bounds.GetChild(0);
                    end = bounds.GetChild(1);
                    border = bounds.GetChild(2);
                    bounds.gameObject.SetActive(true);
                    animal.transform.position = start.transform.position;
                    if (turnedAround[i])
                        animal.transform.rotation = Quaternion.Euler(0, 180, starRotations[i]);
                    else
                        animal.transform.rotation = Quaternion.Euler(0, 0, starRotations[i]);

                    animal.gameObject.GetComponent<Animator>().SetBool("moving", false);
                    /*
                    for (int j = 0; j < bounds.transform.childCount; j++)
                        if (bounds.transform.GetChild(j).tag == "Fruit")
                            bounds.transform.GetChild(j).gameObject.SetActive(true);
                    */
                    //border.gameObject.SetActive(true);
                    animalMoving = false;
                }    
                
            }
            else if (hit.collider == null)
            {
                for (int j = 0; j < bounds.transform.childCount; j++)
                    if (bounds.transform.GetChild(j).tag == "Fruit")
                        bounds.transform.GetChild(j).gameObject.SetActive(true);
                animal.gameObject.GetComponent<Animator>().SetBool("moving", false);
                Destroy(currentLine);
                animal.transform.position = start.transform.position;
                animalMoving = false;
                if (turnedAround[i])
                    animal.transform.rotation = Quaternion.Euler(0, 180, starRotations[i]);
                else
                    animal.transform.rotation = Quaternion.Euler(0, 0, starRotations[i]);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            for (int j = 0; j < bounds.transform.childCount; j++)
                if (bounds.transform.GetChild(j).tag == "Fruit")
                    bounds.transform.GetChild(j).gameObject.SetActive(true);
            animal.gameObject.GetComponent<Animator>().SetBool("moving", false);
            Destroy(currentLine);
            animal.transform.position = start.transform.position;
            animalMoving = false;
            if (turnedAround[i])
                animal.transform.rotation = Quaternion.Euler(0, 180, starRotations[i]);
            else
                animal.transform.rotation = Quaternion.Euler(0, 0, starRotations[i]);
        }
    
    }

    void CreateLine(Vector2 fingerPosition)
    {
        animalMoving = true;
        animal.gameObject.GetComponent<Animator>().SetBool("moving",true);
        animal.transform.position = fingerPosition;
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity,bounds);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //endPoint = startPoint;
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }
    void UpdateLine(Vector2 newFingerPosition)
    {
        if(animalMoving)
            animal.transform.position = newFingerPosition;
        fingerPositions.Add(newFingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,newFingerPosition);
        //endPoint = newFingerPosition;
    }
    private void eatFruit(Ray ray)
    {
        
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);
        if (hit && animalMoving)
        {
            if (hit.transform.gameObject.GetComponent<FruitScript>().rotationEnabled)
            {
                if (turnedAround[i])
                    animal.transform.rotation = Quaternion.Euler(0, 180, hit.transform.gameObject.GetComponent<FruitScript>().rotation);
                else
                    animal.transform.rotation = Quaternion.Euler(0, 0, hit.transform.gameObject.GetComponent<FruitScript>().rotation);
            }
            hit.transform.gameObject.SetActive(false);
        }
    }
}
