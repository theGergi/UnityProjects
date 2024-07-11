using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 5;
    public GameObject bulletPrefab;
    public int bulletSpeed = 20;
    public int cameraSpeed = 1;
    public Camera mainCamera;

    private float[] CameraBoundsX = new float[2];
    private float[] CameraBoundsY = new float[2];
    void Start()
    {
        mainCamera = Camera.main;
        CameraBoundsY[0] = (mainCamera.transform.position.y - mainCamera.orthographicSize);
        CameraBoundsY[1] = (mainCamera.transform.position.y + mainCamera.orthographicSize);
        CameraBoundsX[0] = (mainCamera.transform.position.x - mainCamera.orthographicSize * 16 / 9);
        CameraBoundsX[1] = (mainCamera.transform.position.x + mainCamera.orthographicSize * 16 / 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < CameraBoundsX[0])
            transform.position = new Vector3(CameraBoundsX[1], transform.position.y, 0);
        if (transform.position.x > CameraBoundsX[1])
            transform.position = new Vector3(CameraBoundsX[0], transform.position.y, 0);
        if (transform.position.y < CameraBoundsY[0])
            transform.position = new Vector3(transform.position.x, CameraBoundsY[1], 0);
        if (transform.position.y > CameraBoundsY[1])
            transform.position = new Vector3(transform.position.x, CameraBoundsY[0], 0);
        /*
        if (Input.GetKey(KeyCode.W) && transform.position.y <= mainCamera.transform.position.y + mainCamera.orthographicSize*0.8f)
        {
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y >= mainCamera.transform.position.y - mainCamera.orthographicSize* 0.8f)
        {
            transform.position = transform.position - new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x <= mainCamera.transform.position.x + mainCamera.orthographicSize * 1.4f)
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x >= mainCamera.transform.position.x - mainCamera.orthographicSize * 1.4f)
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
        }
        */
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
    }

    void shootBullet()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float scalar = Mathf.Sqrt(Mathf.Pow(mousePos.x, 2) + Mathf.Pow(mousePos.y, 2));
        GameObject bullet = Instantiate(bulletPrefab , transform.GetChild(0).position, transform.rotation, GameObject.Find("BulletHolder").transform);
        bullet.GetComponent<Rigidbody2D>().velocity = mousePos/scalar*bulletSpeed;
    }
 
    
}
