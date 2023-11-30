using System;
using UnityEngine;

public class InputManager : ManualSingletonMono<InputManager>
{
    public GameEvent OnPlayerHit;
    public GameEvent OnPlayerMiss;
    public GameEvent OnPlayerPause;

    public static KeyCode KeyInput = KeyCode.Space;

    public override void Awake()
    {
        base.Awake();
    }
}