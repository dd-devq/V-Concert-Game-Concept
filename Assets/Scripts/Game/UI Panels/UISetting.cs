using UnityEngine.UI;
using TMPro;
using UI;

public class UISetting : BaseUI
{
    public GameEvent onMusicVolumeChange;
    public GameEvent onSoundVolumeChange;
    public GameEvent onLanguageChange;
    public GameEvent onLogoutClick;

    public TextMeshProUGUI languageTxt;
    public Slider musicVolume;
    public Slider soundVolume;

    public void OnBackClick()
    {
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UINavigationTab);
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }

    public void OnSoundVolumeChange()
    {
        onSoundVolumeChange.Invoke(this, null);
    }

    public void OnMusicVolumeChange()
    {
        onMusicVolumeChange.Invoke(this, null);
    }

    public void OnLanguageChange()
    {
        onLanguageChange.Invoke(this, null);
    }

    public void OnLogoutClick()
    {
        onLogoutClick.Invoke(this, null);
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIAuthentication);
    }
}