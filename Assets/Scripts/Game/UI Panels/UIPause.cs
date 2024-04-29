using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : BaseUI
{
    public GameEvent onVolumeChange;
    public GameEvent onResumeClick;
    public GameEvent onRetryClick;
    public GameEvent onQuitClick;

    public void OnVolumeChange()
    {
        onVolumeChange.Invoke(this, null);
    }

    public void OnResumeClick()
    {
        onResumeClick.Invoke(this, null);
    }

    public void OnRetryClick()
    {
        onRetryClick.Invoke(this, null);
    }

    public void OnQuitClick()
    {
        onQuitClick.Invoke(this, null);
    }
}