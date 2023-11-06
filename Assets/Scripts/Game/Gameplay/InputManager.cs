using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameEvent OnPlayerHit;
    public GameEvent OnPlayerMiss;
    public GameEvent OnPlayerPause;
    public InputMode InputMode;

    private void Awake()
    {
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public enum InputMode
{
    SingleKey,
    MultiKey,
}