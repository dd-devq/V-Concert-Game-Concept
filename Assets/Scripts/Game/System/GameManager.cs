using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameManager Instance => _instance;

    private GameState _gameState;

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
                break;
            case GameState.End:
                break;
            default:
                throw new Exception("Unknown Game State");
        }
    }
}

public enum GameState
{
    Load,
    Play,
    Pause,
    End
}