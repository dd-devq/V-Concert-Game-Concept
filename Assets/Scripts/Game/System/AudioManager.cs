using System;
using UnityEngine;

public class AudioManager : PersistentManager<AudioManager>
{
    public AudioSource musicChannel;
    public AudioSource soundFxChannel;

    private SongData _songData;
    private AudioData _audioData;

    public GameEvent onSongEnd;

    public override void Awake()
    {
        base.Awake();
        _songData = Resources.Load<SongData>("Scriptable Objects/Song Data");
        _audioData = Resources.Load<AudioData>("Scriptable Objects/Audio Data");
    }

    private void Start()
    {
        musicChannel.volume = PlayerPrefs.GetFloat("Music Volume");
        soundFxChannel.volume = PlayerPrefs.GetFloat("Sound Volume");
        PlaySong(this, 10);
        musicChannel.loop = true;
    }

    public void UpdateVolume(Component sender, object data)
    {
        musicChannel.volume = PlayerPrefs.GetFloat("Music Volume");
        soundFxChannel.volume = PlayerPrefs.GetFloat("Sound Volume");
    }


    public void PlaySong(Component sender, object data)
    {
        if (data is int songIndex)
        {
            //debugerror songidx
            Debug.LogError("Play song index: " + songIndex);
            var audioClip = ResourceManager.LoadAudioClip(_songData.SongPath + _songData.ListSong[songIndex].Title);
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
        // if (musicChannel.isPlaying)
        // {
        //     onSongEnd.Invoke(this, null);
        // }
    }
}