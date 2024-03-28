using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class UIAvatarSelection : BaseUI
{
    public GameEvent onNewAvatarClick;
    // Current Avatar

    private UIIndex _prevUIIndex;

    public void OnBackClick()
    {
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(_prevUIIndex);
        UIManager.Instance.ShowUI(UIIndex.UINavigationTab);
    }

    public void OnNewAvatarClick()
    {
    }

    protected override void OnShow(UIParam param = null)
    {
        base.OnShow(param);
        if (param != null)
        {
            _prevUIIndex = (UIIndex)param.Data;
        }
    }
}