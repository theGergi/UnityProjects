using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void openGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void openMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
