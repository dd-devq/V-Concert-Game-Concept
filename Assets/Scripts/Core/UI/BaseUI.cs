using System;
using Game.System;
using UnityEngine;
using UI;
public abstract class BaseUI : MonoBehaviour
{
    public UIIndex index;
    public GameEvent playSoundOnClick;
    private RectTransform _transform;

    protected virtual void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public virtual void OnInit()
    {
    }

    protected virtual void OnSetup(UIParam param = null)
    {
    }

    protected virtual void OnShow(UIParam param = null)
    {
    }

    protected virtual void OnHide()
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

    protected virtual void PlaySoundOnClick()
    {
        playSoundOnClick.Invoke(this, null);
    }
}