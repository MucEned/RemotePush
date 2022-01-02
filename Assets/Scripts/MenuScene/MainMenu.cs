using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        checkKey();
    }
    void checkKey()
    {
        if(!PlayerPrefs.HasKey("GameMode"))
            {
                PlayerPrefs.SetString("GameMode", "Classic");
            }
        if(!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
        }
        if(!PlayerPrefs.HasKey("HighScoreKey"))
        {
            PlayerPrefs.SetInt("HighScoreKey", 0);
        }
        if(!PlayerPrefs.HasKey("TimeToSetHScoreKey"))
        {
            PlayerPrefs.SetFloat("TimeToSetHScoreKey", 0f);
        }
        if(!PlayerPrefs.HasKey("CurrentScoreKey"))
        {
            PlayerPrefs.SetInt("CurrentScoreKey", 0);
        }
        if(!PlayerPrefs.HasKey("TimeToSetCurrentScoreKey"))
        {
            PlayerPrefs.SetFloat("TimeToSetCurrentScoreKey", 0f);
        }
    }
    void Start()
    {
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void LoadSceneIndex(int i)
    {
        // SceneManager.LoadScene(i);
        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene(i));
    }
    IEnumerator LoadYourAsyncScene(int i)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(i);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void isClassic(bool isClassic)
    {
        if(isClassic)
            PlayerPrefs.SetString("GameMode","Classic");
        else
            PlayerPrefs.SetString("GameMode","New School");
    }
}
