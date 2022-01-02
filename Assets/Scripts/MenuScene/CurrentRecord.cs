using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRecord : MonoBehaviour
{    
    private const string CurrentScoreKey = "CurrentScoreKey";
    private const string TimeToSetCurrentScoreKey = "TimeToSetCurrentScoreKey";
    private const string HighScoreKey = "HighScoreKey";
    // Start is called before the first frame update
    void Start()
    {
        int _cs = PlayerPrefs.GetInt(CurrentScoreKey, 0);
        float _t = PlayerPrefs.GetFloat(TimeToSetCurrentScoreKey, 0);
        
        GetComponent<Text>().text = "Your Score:\n" + _cs + "\nTime:\n" + (int)(_t / 60) + ":" + (int)(_t % 60);
        if (_cs >= PlayerPrefs.GetInt(HighScoreKey,0))
        {
            SoundSource.PlaySound("highscore");
            GetComponent<Text>().text += "\nHIGHSCORE!!!";
        }
        else
            SoundSource.PlaySound("end");
    }
}
