using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISongSelection : BaseUI
{
    public GameEvent onPlayClick;
    public GameEvent onSongClick;

    public void OnPlayClick()
    {
        onPlayClick.Invoke(this, SceneIndex.Gameplay);
    }

    public void OnSongClick()
    {
        onSongClick.Invoke(this, null);
        LoadLeaderBoard();
    }

    public void LoadSongList()
    {
    }

    public void LoadLeaderBoard()
    {
    }
}