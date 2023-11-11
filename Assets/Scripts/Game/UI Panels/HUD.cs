using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : BaseUI
{
    [SerializeField] private TextMeshProUGUI score;

    public GameEvent onPauseClick;

    public void UpdateScore(Component sender, object data)
    {
        if (data is int)
        {
            var amount = (int)data;
            SetScore(amount.ToString());
        }
    }

    private void SetScore(string newScore)
    {
        score.SetText(newScore.ToString());
    }
}