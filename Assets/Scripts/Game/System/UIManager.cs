using UnityEngine;
using System.Collections.Generic;
using System;
using UI;
using UnityEngine.Serialization;

public class UIManager : ManualSingletonMono<UIManager>
{
    private readonly Dictionary<UIIndex, BaseUI> _uiDictionary = new();
    [SerializeField] private List<BaseUI> listUI = new();

    public UIIndex currentUIIndex;


    private void Start()
    {
        InitUI(null);
    }

    private void InitUI(Action callback)
    {
        foreach (var ui in listUI)
        {
            var goUI = ui.gameObject;
            goUI.transform.SetParent(transform, false);

            var baseUI = goUI.GetComponent<BaseUI>();
            baseUI.OnInit();

            _uiDictionary.Add(baseUI.index, baseUI);

            goUI.SetActive(false);
        }

        callback?.Invoke();
        ShowUI(UIIndex.UIMainMenu);
        ShowUI(UIIndex.UINavigationTab);
    }

    public void ShowUI(UIIndex uiIndex, UIParam param = null, Action callback = null)
    {
        var ui = GetUI(uiIndex);
        ui.ShowUI(param, callback);

        if (uiIndex != UIIndex.UINavigationTab)
        {
            currentUIIndex = uiIndex;
        }
    }

    public void HideUI(BaseUI baseUI, Action callback = null)
    {
        baseUI.HideUI(callback);
        if (baseUI.index != UIIndex.UINavigationTab)
        {
            currentUIIndex = UIIndex.None;
        }
    }

    public void HideUI(UIIndex uiIndex, Action callback = null)
    {
        var ui = GetUI(uiIndex);
        ui.HideUI(callback);
        if (uiIndex != UIIndex.UINavigationTab)
        {
            currentUIIndex = UIIndex.None;
        }
    }

    public void HideAllUI(Action callback = null)
    {
    }

    private BaseUI GetUI(UIIndex uiIndex)
    {
        return _uiDictionary[uiIndex];
    }
}