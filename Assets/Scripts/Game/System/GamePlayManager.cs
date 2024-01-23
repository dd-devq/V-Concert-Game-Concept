using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : ManualSingletonMono<GamePlayManager>
{
    public GameEvent OnNoteHit;
    public GameEvent OnNoteMiss;
    public void OnTriggerNoteMiss(object data)
    {
        OnNoteMiss.Invoke(null, data);
    }
    public void OnTriggerNoteHit(object data)
    {
        OnNoteHit.Invoke(null, data);
    }
}
