using TMPro;
using UI;

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
        UIManager.Instance.HideUI(this);
        UIManager.Instance.HideUI(UIManager.Instance.currentUIIndex);
        UIManager.Instance.ShowUI(UIIndex.UIAvatarSelection);
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