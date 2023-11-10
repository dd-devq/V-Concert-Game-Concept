using System;
using Game.System;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public UIIndex index;
    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void OnInit()
    {
    }

    private void OnSetup(UIParam param = null)
    {
    }

    private void OnShow(UIParam param = null)
    {
    }

    private void OnHide()
    {
    }

    public void ShowUI(UIParam param = null, Action callback = null)
    {
        gameObject.SetActive(true);
        _transform.SetAsLastSibling();
        OnSetup(param);
        OnShow(param);
        callback?.Invoke();
    }

    public void HideUI(Action callback = null)
    {
        OnHide();
        gameObject.SetActive(false);
        callback?.Invoke();
    }
}

public abstract class UIParam
{
}