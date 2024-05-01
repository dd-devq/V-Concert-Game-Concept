using TMPro;
using UI;
using UnityEngine;

public class UIHud : BaseUI
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;
    public TextMeshProUGUI streak;

    public GameEvent onPauseClick;

    public void OnPauseClick()
    {
        onPauseClick.Invoke(this, null);
        UIManager.Instance.HideUI(index);
        UIManager.Instance.ShowUI(UIIndex.UIPause);
    }
    
    public void UpdateScore(Component sender, object data)
    {
        if (data is int amount)
        {
            SetScore(amount.ToString());
        }
    }
    
    public void UpdateStreak(Component sender, object data)
    {
        if (data is int streak)
        {
            SetStreak(streak.ToString());
        }
    }
    
    public void UpdateCombo(Component sender, object data)
    {
        if (data is int combo)
        {
            SetCombo(combo.ToString());
        }
    }

    private void SetScore(string newScore)
    {
        score.SetText(newScore);
    }
    
    private void SetStreak(string newStreak)
    {
        streak.SetText(newStreak);
    }
    
    private void SetCombo(string newCombo)
    {
        combo.SetText(newCombo);
    }
    
}