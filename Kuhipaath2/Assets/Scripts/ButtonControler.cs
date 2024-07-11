using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonControler : MonoBehaviour
{
    public void openModule1()
    {
        SceneManager.LoadScene(sceneName: "Module1");
    }
    public void openAlphabetTracing()
    {
        SceneManager.LoadScene(sceneName: "AlphabetTracing");
    }
    public void openAlphabetTracingCapital()
    {
        SceneManager.LoadScene(sceneName: "AlphabetTracingCapital");
    }
    public void openAlphabetTracingSmall()
    {
        SceneManager.LoadScene(sceneName: "AlphabetTracingSmall");
    }
    /*
    public void openSubscription()
    {
        SceneManager.LoadScene(sceneName: "Subscription");
    }
    
    public void openLoginMenu()
    {
        SceneManager.LoadScene(sceneName: "Auth");  
    }
    */
    public void openSettings()
    {
        SceneManager.LoadScene(sceneName: "Settings");
    }
    public void openLevel(string letter)
    {
        //if(GetComponent<isSubscribed>().subscribed || letter == "A" || letter == "aSmall" || letter == "B" || letter == "bSmall")
        SceneManager.LoadScene(letter);
    }
    public void openLevelSpelling(string levelName)
    {
        SceneManager.LoadScene(sceneName: levelName);
    }
    public void openSpelling()
    {
        SceneManager.LoadScene(sceneName: "Spelling");
    }
    public void openFeeding()
    {
        SceneManager.LoadScene(sceneName: "Feeding");
    }
    
}
