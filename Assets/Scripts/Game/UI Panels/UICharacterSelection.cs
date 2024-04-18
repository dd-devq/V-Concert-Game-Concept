using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelection : BaseUI
{
    public GameEvent onCharacterClick;

    public void OnCharacterClick()
    {
        onCharacterClick.Invoke(this, null);
    }

    public void LoadCharacterList()
    {
    }
}