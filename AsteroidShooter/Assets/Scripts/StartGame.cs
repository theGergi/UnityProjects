using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class StartGame : MonoBehaviour
{
    public InputField input;
    public GameObject warning;

    public void startGame()
    {
        string name = input.text;
        
        if (!isValid(name))
        {
            warning.GetComponent<Text>().text = "Name must contain only latin letters, digits and underscores";
            warning.SetActive(true);
        }
        else if(!char.IsLetter(name[0]))
        {
            warning.GetComponent<Text>().text = "Name must start with a letter";
            warning.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("name", name);
            SceneManager.LoadScene("MainScene");
        }
       
    }
    private bool containsLetters(string name)
    {
        for(int i = 0; i < name.Length; i++)
        {
            if (char.IsLetter(name[i]))
            {
                return true;
            }
        }
        return false;
    }
    public bool isValid(string input)
    {
        return Regex.IsMatch(input, "^[a-zA-Z0-9_]+$");
    }
}
