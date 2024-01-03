using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : ManualSingletonMono<ScoreManager>
{
    public TextMeshPro ScoreText = null;
    
    private static int _comboScore;

    void Start()
    {
        _comboScore = 0;
    }
    public static void Hit()
    {
        _comboScore += 1;
        AudioManager.Instance.PlayHitSFX();
    }
    public static void Miss()
    {
        _comboScore -= 1;
        AudioManager.Instance.PlayMissSFX();
    }
    public static void SetScore(int score)
    {
        _comboScore = score;
    }
    public static int GetScore()
    {
        return _comboScore;
    }
    private void Update()
    {
        ScoreText.text = _comboScore.ToString();
    }
}
