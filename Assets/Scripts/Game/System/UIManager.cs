using UnityEngine;
using System.Collections.Generic;
using System;
using UI;
using UnityEngine.Serialization;

public class UIManager : ManualSingletonMono<UIManager>
{
    private Dictionary<UIIndex, BaseUI> _uiDictionary = new();
    [SerializeField] private List<BaseUI> listUI = new();

    public UIIndex CurrentUIIndex;
    

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

            var rectTransform = goUI.transform.GetComponent<RectTransform>();
            rectTransform.offsetMax = rectTransform.offsetMin = Vector2.zero;

            var baseUI = goUI.GetComponent<BaseUI>();
            baseUI.OnInit();

            _uiDictionary.Add(baseUI.index, baseUI);

            goUI.SetActive(false);
        }

        callback?.Invoke();
        ShowUI(UIIndex.UIAuthentication);
    }

    public void ShowUI(UIIndex uiIndex, UIParam param = null, Action callback = null)
    {
        var ui = GetUI(uiIndex);
        ui.ShowUI(param, callback);
        Debug.Log(ui.index);
    }

    public void HideUI(BaseUI baseUI, Action callback = null)
    {
        baseUI.HideUI(callback);
        Debug.Log(baseUI.index);
    }

    public void HideUI(UIIndex uiIndex, Action callback = null)
    {
    }

    public void HideAllUI(Action callback = null)
    {
    }

    private BaseUI GetUI(UIIndex uiIndex)
    {
        return _uiDictionary[uiIndex];
    }
}