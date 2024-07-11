using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private float[] CameraBoundsX = new float[2];
    private float[] CameraBoundsY = new float[2];
    private GameObject gameController;
    private Camera MainCamera;
    public float asteroidSpeed;
    public int xp;
    System.Random rand;
    Vector2 sideMovement;
    void Start()
    {
        rand = new System.Random();

        MainCamera = Camera.main;
        gameController = GameObject.Find("GameController");
        
        CameraBoundsY[0] = (MainCamera.transform.position.y - MainCamera.orthographicSize);
        CameraBoundsY[1] = (MainCamera.transform.position.y + MainCamera.orthographicSize);
        CameraBoundsX[0] = (MainCamera.transform.position.x - MainCamera.orthographicSize * 16/9);
        CameraBoundsX[1] = (MainCamera.transform.position.x + MainCamera.orthographicSize * 16/9);

        sideMovement.x = -GetComponent<Rigidbody2D>().velocity.y;
        sideMovement.y = GetComponent<Rigidbody2D>().velocity.x;
    }

    private void Update()
    {
        if (transform.position.x < CameraBoundsX[0])
            transform.position = new Vector3(CameraBoundsX[1], transform.position.y, 0);
        if (transform.position.x > CameraBoundsX[1])
            transform.position = new Vector3(CameraBoundsX[0], transform.position.y, 0);
        if (transform.position.y < CameraBoundsY[0])
            transform.position = new Vector3(transform.position.x, CameraBoundsY[1], 0);
        if (transform.position.y > CameraBoundsY[1])
            transform.position = new Vector3(transform.position.x, CameraBoundsY[0], 0);

        if (gameObject.tag == "ShibaFloki")
        {
            
            //Vector2 curSideMovement = sideMovement * Mathf.Sin(2*(Time.time+Mathf.PI/2));
            //GetComponent<Rigidbody2D>().velocity += curSideMovement/200;
            
            Vector3 curSideMovement = sideMovement * Mathf.Sin(Time.time);
            transform.position += curSideMovement/500;

        }

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            if (gameObject.tag == "Moonway")
            {
                GameObject smallerAsteroid = gameController.GetComponent<GameController>().AsteroidPrefabs[3];
                float xSpawn = (float)Random.Range(0, 100) / 100 * (Random.Range(0, 2) * 2 - 1);
                float ySpawn = (float)Mathf.Sqrt(1 - Mathf.Pow(xSpawn, 2)) * (Random.Range(0, 2) * 2 - 1);
                GameObject asteroidleft = Instantiate(smallerAsteroid, transform.position + new Vector3(xSpawn, ySpawn, 0), transform.rotation, GameObject.Find("AsteroidsHolder").transform);
                GameObject asteroidright = Instantiate(smallerAsteroid, transform.position - new Vector3(xSpawn, ySpawn, 0), transform.rotation, GameObject.Find("AsteroidsHolder").transform);

                float xVelocity = xSpawn * smallerAsteroid.GetComponent<AsteroidScript>().asteroidSpeed;
                float yVelocity = ySpawn * smallerAsteroid.GetComponent<AsteroidScript>().asteroidSpeed;

                asteroidleft.GetComponent<Rigidbody2D>().velocity = new Vector3(xVelocity, yVelocity, 0);
                asteroidright.GetComponent<Rigidbody2D>().velocity = -asteroidleft.GetComponent<Rigidbody2D>().velocity;

            }
            else if (gameObject.tag == "Pooniverse")
            {
                GameObject smallerAsteroid = gameController.GetComponent<GameController>().AsteroidPrefabs[1];
                float xSpawn = (float)Random.Range(0, 100) / 100 * (Random.Range(0, 2) * 2 - 1);
                float ySpawn = (float)Mathf.Sqrt(1 - Mathf.Pow(xSpawn, 2)) * (Random.Range(0, 2) * 2 - 1);
                GameObject asteroidleft = Instantiate(smallerAsteroid, transform.position + new Vector3(xSpawn , ySpawn,0), transform.rotation, GameObject.Find("AsteroidsHolder").transform);
                GameObject asteroidright = Instantiate(smallerAsteroid, transform.position - new Vector3(xSpawn , ySpawn, 0), transform.rotation, GameObject.Find("AsteroidsHolder").transform);

                float xVelocity = xSpawn * smallerAsteroid.GetComponent<AsteroidScript>().asteroidSpeed;
                float yVelocity = ySpawn * smallerAsteroid.GetComponent<AsteroidScript>().asteroidSpeed;

                asteroidleft.GetComponent<Rigidbody2D>().velocity = new Vector3(xVelocity, yVelocity, 0);
                asteroidright.GetComponent<Rigidbody2D>().velocity = -asteroidleft.GetComponent<Rigidbody2D>().velocity;

            }
            gameController.GetComponent<GameController>().addXP(xp);
            Destroy(gameObject);
            
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            if(gameObject.tag == "Moonway")
            {
                GameObject smallerAsteroid = gameController.GetComponent<GameController>().AsteroidPrefabs[1];
                Instantiate(smallerAsteroid, transform.GetChild(0).position, transform.rotation, GameObject.Find("AsteroidsHolder").transform);
                Instantiate(smallerAsteroid, transform.GetChild(1).position, transform.rotation, GameObject.Find("AsteroidsHolder").transform);
            }
            Destroy(gameObject);
            
        }
        gameController.GetComponent<GameController>().loseLife();
    }
}
/*
 Vector3 difference = ship.transform.position - transform.position;
        float scalar = Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2));
        Vector3 scaledDifference = (difference / scalar) * asteroidSpeed * Time.deltaTime;
        Vector3 sideMovement = new Vector3(0, 0, 0);

        if(gameObject.tag == "ShibaFloki")
        {
            
            sideMovement.x = scaledDifference.y;
            sideMovement.y = -scaledDifference.x;
            sideMovement = sideMovement * Mathf.Sin(Time.time) * 3;
            
        }
        

        transform.position += scaledDifference + sideMovement;
 */