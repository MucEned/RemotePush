using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSong : MonoBehaviour
{
    public AudioClip classic, newschool;
    private AudioClip currentClip;
    void Start()
    {
        if(PlayerPrefs.GetString("GameMode") == "Classic")
        {
            currentClip = classic;
        }
        else
            currentClip = newschool;

        GetComponent<AudioSource>().clip = currentClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }
}
