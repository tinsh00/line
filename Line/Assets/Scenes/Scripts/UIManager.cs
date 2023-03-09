using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text score, highScoreTxt;
    const string HIGHSCORE = "HighScore";
    float highScore;
    float currentScore = 0;
    private void Start()
    {
        if (PlayerPrefs.HasKey(HIGHSCORE))
        {
            highScore = PlayerPrefs.GetFloat(HIGHSCORE,0);
            UpdateHighScore(highScore);
        }
        UpdateYourScore(currentScore);
    }
    public void UpdateYourScore(float _score)
    {
        print(_score);
        score.text = String.Format("{0,6:000000}", _score) ;
    }
    public void UpdateHighScore(float _highScore)
    {
        highScoreTxt.text = String.Format("{0,6:000000}", _highScore);
    }
    public void SetSaveHighScore(float value)
    {
        if (value <= highScore) return;
        PlayerPrefs.SetFloat(HIGHSCORE, value);
    }
}
