using EventData;
using UnityEngine;

public class AudioManager : PersistentManager<AudioManager>
{
    public AudioSource musicChannel;
    public AudioSource soundFxChannel;

    private SongData _songData;
    private AudioData _audioData;

    public GameEvent onSongEnd;
    
    private bool _isSongEndInvoke;
    private int _songIndex;

    public override void Awake()
    {
        base.Awake();
        _songData = Resources.Load<SongData>("Scriptable Objects/Song Data");
        _audioData = Resources.Load<AudioData>("Scriptable Objects/Audio Data");
        _songIndex = -1;
        _isSongEndInvoke = false;
    }

    private void Start()
    {
        musicChannel.volume = PlayerPrefs.GetFloat("Music Volume");
        soundFxChannel.volume = PlayerPrefs.GetFloat("Sound Volume");
    }

    public void UpdateVolume(Component sender, object data)
    {
        musicChannel.volume = PlayerPrefs.GetFloat("Music Volume");
        soundFxChannel.volume = PlayerPrefs.GetFloat("Sound Volume");
    }

    public void SetSong(Component sender, object data)
    {
        var temp = (LevelData)data;
        _songIndex = temp.SongIndex;
    }


    public void PlaySong(Component sender, object data)
    {
        if (_songIndex != -1)
        {
            var audioClip = ResourceManager.LoadAudioClip(_songData.SongPath + _songData.ListSong[_songIndex].Title);
            musicChannel.clip = audioClip;
            musicChannel.Play();
        }
    }

    public void PauseSong()
    {
        if (!musicChannel) return;
        musicChannel.Pause();
    }

    public void RemoveSong()
    {
        if (!musicChannel) return;
        musicChannel.clip = null;
    }

    public void PlaySoundFx(Component sender, object data)
    {
        if (data is int audioIndex)
        {
            var audioClip = ResourceManager.LoadAudioClip(_audioData.AudioPath + _audioData.ListAudio[audioIndex]);
            soundFxChannel.clip = audioClip;
            soundFxChannel.Play();
        }
    }

    public void PauseSoundFx()
    {
        if (!soundFxChannel) return;
        soundFxChannel.Pause();
    }

    public void RemoveSoundFx()
    {
        if (!soundFxChannel) return;
        soundFxChannel.clip = null;
    }

    private void Update()
    {
        if (!musicChannel.isPlaying && !_isSongEndInvoke)
        {
            onSongEnd.Invoke(this, null);
            _isSongEndInvoke = true;
        }
    }
}