using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISongSelection : BaseUI
{
    public GameEvent onPlayClick;

    public void OnPlayClick()
    {
        onPlayClick.Invoke(this, SceneIndex.Gameplay);
    }
}
