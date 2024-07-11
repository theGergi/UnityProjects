using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject shopDesk;
    public GameObject canvas;
    public float movementSpeed = 0.2f;
    public float maxSpeed = 1.5f;


    void Update()
    {
        if(canvas.transform.Find("Instructions").gameObject.activeSelf && Input.GetKey(KeyCode.Return))
        {
            canvas.transform.Find("Instructions").gameObject.SetActive(false);
            canvas.SetActive(false);
        }

        if (!canvas.activeInHierarchy)
            if (Input.GetKey(KeyCode.A) && GetComponent<Rigidbody2D>().velocity.x > -maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementSpeed, 0));
                GetComponent<Animator>().SetInteger("direction", 1);
                GetComponent<Animator>().SetBool("inMotion", true);
            }
            else if (Input.GetKey(KeyCode.D) && GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed, 0));
                GetComponent<Animator>().SetInteger("direction", 2);
                GetComponent<Animator>().SetBool("inMotion", true);
            }
            else if (Input.GetKey(KeyCode.W) && GetComponent<Rigidbody2D>().velocity.y < maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, movementSpeed));
                GetComponent<Animator>().SetInteger("direction", 4);
                GetComponent<Animator>().SetBool("inMotion", true);
            }
            else if (Input.GetKey(KeyCode.S) && GetComponent<Rigidbody2D>().velocity.y > -maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -movementSpeed));
                GetComponent<Animator>().SetInteger("direction", 3);
                GetComponent<Animator>().SetBool("inMotion", true);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Animator>().SetBool("inMotion", false);
            }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("inMotion", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && shopDesk.GetComponent<DeskHitbox>().playerInside)
        {
            if (!canvas.activeInHierarchy)
            {
                canvas.SetActive(true);
                canvas.transform.Find("DialogBox").gameObject.SetActive(true);
            }


            else
            {
                canvas.SetActive(false);
                foreach (Transform child in canvas.transform)
                    child.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
            if (!canvas.activeInHierarchy)
            {
                canvas.SetActive(true);
                canvas.transform.Find("Inventory").gameObject.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
                foreach (Transform child in canvas.transform)
                    child.gameObject.SetActive(false);
            }
    }
}
