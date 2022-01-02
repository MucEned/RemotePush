using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScoreView : MonoBehaviour
{
    private const string HighScoreKey = "HighScoreKey";
    private const string TimeToSetHScoreKey = "TimeToSetHScoreKey";
    void Awake()
    {
        int _hs = PlayerPrefs.GetInt(HighScoreKey, 0);
        float _t = PlayerPrefs.GetFloat(TimeToSetHScoreKey, 0);
        GetComponent<Text>().text = "Highest Score:\n" + _hs + "\nTime:\n" + (int)(_t / 60) + ":" + (int)(_t % 60);
    }
}
