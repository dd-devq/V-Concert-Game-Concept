using UnityEngine;

public class GameManager : PersistentManager<GameManager>
{

    public GameEvent onSongStart;
    public GameEvent onPauseClick;
    public GameEvent onSongEnd;
    
    public GameEvent consumeItem;

    private void Start()
    {
        InitGame(this, SceneManager.Instance.LoadSceneData);
    }
    
    public void InitGame(Component sender, object data)
    {
        if (data is int songIndex)
        {
            SongManager.Instance.ReadFromFile(songIndex);
            AudioManager.Instance.PlaySong(this, songIndex);
        }
        else
        {
            Debug.LogError("Invalid song index");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
    }


}
