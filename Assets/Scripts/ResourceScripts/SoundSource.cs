using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    public static AudioClip click, failclick, score, jump, wind, highscore, end, fat, welcome;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Awake()
    {
        click = Resources.Load<AudioClip>("click");
        failclick = Resources.Load<AudioClip>("fail");
        score = Resources.Load<AudioClip>("score");
        jump = Resources.Load<AudioClip>("jump");
        end = Resources.Load<AudioClip>("end");
        wind = Resources.Load<AudioClip>("wind");
        highscore = Resources.Load<AudioClip>("endHighScore");
        fat = Resources.Load<AudioClip>("fatBall");
        welcome = Resources.Load<AudioClip>("welcome");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "click":
                audioSrc.PlayOneShot(click);
                break;
            case "failclick":
                audioSrc.PlayOneShot(failclick);
                break;
            case "score":
                audioSrc.PlayOneShot(score);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "fat":
                audioSrc.PlayOneShot(fat);
                break;
            case "highscore":
                audioSrc.PlayOneShot(highscore);
                break;
            case "wind":
                audioSrc.PlayOneShot(wind);
                break;
            case "end":
                audioSrc.PlayOneShot(end);
                break;
            case "welcome":
                audioSrc.PlayOneShot(welcome);
                break;
        }
    }
}
