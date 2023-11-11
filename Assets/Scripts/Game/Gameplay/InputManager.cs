using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameEvent OnPlayerHit;
    public GameEvent OnPlayerMiss;
    public GameEvent OnPlayerPause;

    private void Awake()
    {
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}