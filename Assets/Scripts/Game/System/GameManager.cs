using System;
using EventData;
using UnityEngine;

public class GameManager : PersistentManager<GameManager>
{
    public GameEvent onSongStart;
    public GameEvent onPauseClick;

    private LevelData _levelData;
    private GameState _gameState;


    public override void Awake()
    {
        base.Awake();
        _gameState = GameState.UI;
    }

    public void InitGame()
    {
        // Invoke(nameof(StartGame), 3.0f);
    }

    public void StartGame()
    {
        // SongManager.Instance.ReadFromFile(_levelData.SongIndex);
        onSongStart.Invoke(this, null);
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.UI:
                break;
            case GameState.Play:
                ProcessGameplay();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ProcessGameplay()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPauseClick.Invoke(this, null);
            Time.timeScale = 0;
        }
    }

    public void StartGamePlay(Component sender, object data)
    {
        _levelData = (LevelData)data;
        // InitGame();
        StartGame();
    }
}

public enum GameState
{
    UI,
    Play
}