using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;

    public GameEvent OnGameplayLoad;
    public GameEvent OnGameplayEnd;
    public GameEvent OnGameplayPause;
    public GameEvent OnGameplay;

    public void InitGame(Component sender, object data)
    {
    }

    public void StartGame()
    {
    }

    public void TogglePauseGame()
    {
    }

    public void EndGame()
    {
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.Load:
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                OnGameplayPause.Invoke(this, null);
                Time.timeScale = 0;
                break;
            case GameState.End:
                var data = new Define.EndGameData { Gems = 10, Coins = 200, Score = 200000 };
                OnGameplayPause.Invoke(this, data);
                break;
            default:
                throw new Exception("Unknown Game State");
        }
    }

    public void OnGameplayEndEvent(Component sender, object data)
    {
        _gameState = GameState.End;
    }

    public void OnGameplayPauseEvent(Component sender, object data)
    {
        _gameState = GameState.Pause;
    }

    public void OnGameplayLoadEvent(Component sender, object data)
    {
        _gameState = GameState.Load;
    }

    public void OnGameplayEvent(Component sender, object data)
    {
        _gameState = GameState.Play;
    }
}

public enum GameState
{
    Load,
    Play,
    Pause,
    End
}