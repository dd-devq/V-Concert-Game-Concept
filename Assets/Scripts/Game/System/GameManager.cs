using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;

    private GameEvent startSong;
    private GameEvent pauseGame;
    private GameEvent endGame;

    [SerializeField]
    private GameObject playerModel;

    public GameObject GetPlayerModel()
    {
        return playerModel;
    }
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
            case GameState.Play:
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.End:
                break;
            default:
                //throw new Exception("Unknown Game State");
                break;
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