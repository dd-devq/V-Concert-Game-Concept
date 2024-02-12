using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : BaseUI
{
    public GameEvent onSongClick;
    public GameEvent onHomeClick;
    public GameEvent onShopClick;
    public GameEvent onInventoryClick;
    public GameEvent onCharacterClick;
    public GameEvent onAvatarClick;
    public GameEvent onPlayClick;

    public void OnPlayClick()
    {
        onPlayClick.Invoke(this, SceneIndex.Gameplay);
    }
}