using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    private int score = 0;
    private int hScore = 0;
    private float timeToSetHScore = 0;
    private float thisTime = 0f;
    private Text scoreTxt;
    private Text hScoreTxt;
    private Text timeCounterTxt;
    private const string HighScoreKey = "HighScoreKey";
    private const string TimeToSetHScoreKey = "TimeToSetHScoreKey";
    private const string CurrentScoreKey = "CurrentScoreKey";
    private const string TimeToSetCurrentScoreKey = "TimeToSetCurrentScoreKey";
    public static DataController datacontroller;
    void Start()
    {
        datacontroller = this.GetComponent<DataController>();
        scoreTxt = transform.GetChild(0).gameObject.GetComponent<Text>();
        hScoreTxt = transform.GetChild(1).gameObject.GetComponent<Text>();
        timeCounterTxt = transform.GetChild(2).gameObject.GetComponent<Text>();

        hScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        timeToSetHScore = PlayerPrefs.GetFloat(TimeToSetHScoreKey, 0);
        hScoreTxt.text = hScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        thisTime += Time.deltaTime;
        timeCounterTxt.text = (int)(thisTime / 60) + ":" + (int)(thisTime % 60);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreTxt.text = score.ToString();
    }
    public void CheckScore()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, score);
        PlayerPrefs.SetFloat(TimeToSetCurrentScoreKey, thisTime);

        if (score > hScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.SetFloat(TimeToSetHScoreKey, thisTime);
        }
        if (score == hScore && thisTime < timeToSetHScore)
        {
            PlayerPrefs.SetFloat(TimeToSetHScoreKey, thisTime);
        }
        DeleteLoadState();
    }
    private void DeleteLoadState()
    {
        return;
    }
}
