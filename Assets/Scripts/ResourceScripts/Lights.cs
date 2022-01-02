using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.GetString("GameMode") == "Classic")
        {
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }
}
