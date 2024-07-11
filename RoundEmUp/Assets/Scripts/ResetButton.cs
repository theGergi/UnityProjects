using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject gameManager;
    private GameObject prefabs;
    private void Start()
    {
        prefabs = GameObject.Find("Prefabs");
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Click");
            Reset();
        }
    }
    public void Reset()
    {
        gameManager.GetComponent<Lasso>().Start();
        FindObjectOfType<AudioManager>().Play("woahboy");

        for (int i = 0; i < prefabs.transform.childCount; i++)
        {
            Destroy(prefabs.transform.GetChild(i).gameObject);
        }
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    IEnumerator Click()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
