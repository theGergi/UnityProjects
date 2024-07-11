using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject SignInMenu;
    public GameObject dimBackground;

    public Sprite musicOn;
    public Sprite musicOff;
    public GameObject musicButton;

    public void openSignInMenu()
    {
        dimBackground.SetActive(true);
        SignInMenu.SetActive(true);
    }
    public void closeSignInMenu()
    {
        dimBackground.SetActive(false);
        SignInMenu.SetActive(false);
    }

    public void startStopMusic()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("music");
        if (!musicPlayer.GetComponent<AudioSource>().mute)
        {
            musicPlayer.GetComponent<AudioSource>().mute = true;
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
            
        else
        {
            musicPlayer.GetComponent<AudioSource>().mute = false;
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
            
    }
}
