using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.GetString("GameMode") != "Classic")
        {
            GetComponent<Animator>().Play("newSchool");
        }
    }
}
