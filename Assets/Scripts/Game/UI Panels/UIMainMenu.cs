using System;
using TMPro;
using UI;
using UnityEngine;

public class UIMainMenu : BaseUI
{
    public TextMeshProUGUI txt;
    public GameObject stage;
    public GameObject smpl;

    private void Update()
    {
        txt.SetText(stage.transform.localPosition.ToString() + '\n' + stage.transform.localRotation.ToString());
        txt.SetText(smpl.transform.localPosition.ToString() + '\n' + smpl.transform.localRotation.ToString());
    }

    public void OnShopClick()
    {
        UIManager.Instance.HideUI(index);
        UIManager.Instance.ShowUI(UIIndex.UIShop);
    }

    public void OnInventoryClick()
    {
        UIManager.Instance.HideUI(index);
        UIManager.Instance.ShowUI(UIIndex.UIInventory);
    }

    public void OnSettingClick()
    {
        UIManager.Instance.HideUI(index);
        UIManager.Instance.HideUI(UIIndex.UINavigationTab);
        UIManager.Instance.ShowUI(UIIndex.UISetting);
    }


    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnCharacterSelectionClick()
    {
        UIManager.Instance.HideUI(index);
        UIManager.Instance.ShowUI(UIIndex.UICharacterSelection);
    }
}