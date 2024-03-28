using PlayFab.ClientModels;
using TMPro;
using UI;
using UnityEngine;

public class UINavigationTab : BaseUI
{
    public TextMeshProUGUI usernameTxt;
    public TextMeshProUGUI coinAmountTxt;
    public TextMeshProUGUI gemAmountTxt;

    public void OnPlayClick()
    {
        UIManager.Instance.HideUI(UIManager.Instance.currentUIIndex);
        UIManager.Instance.ShowUI(UIIndex.UIModeSelection);
    }

    public void OnHomeClick()
    {
        UIManager.Instance.HideUI(UIManager.Instance.currentUIIndex);
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }

    public void OnAvatarClick()
    {
        var uiParam = new UIParam
        {
            Data = UIManager.Instance.currentUIIndex
        };
        UIManager.Instance.HideUI(UIManager.Instance.currentUIIndex);
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIAvatarSelection, uiParam);
    }

    private void UpdateCoin(string amount)
    {
        coinAmountTxt.SetText(amount);
    }

    private void UpdateGem(string amount)
    {
        gemAmountTxt.SetText(amount);
    }

    private void UpdateUsername(string username)
    {
        usernameTxt.SetText(username);
    }
}