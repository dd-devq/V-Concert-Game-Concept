using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : PersistentManager<AudioManager>
{
    public AudioSource musicSource;
    public AudioSource soundFxSource;
    public SongData songData;

    public override void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        songData = Resources.Load<SongData>("Scriptable Objects/Song Data");
    }

    private void Start()
    {
        PlaySong(100);
    }

    public void PlaySong(int songIndex)
    {
        var audioClip = ResourceManager.LoadAudioClip(songData.SongPath + songData.ListSong[songIndex].Title);
        musicSource.clip = audioClip;
        musicSource.Play();
    }

    public void PauseSong()
    {
        if (!musicSource) return;
        musicSource.Pause();
    }

    public void RemoveSong()
    {
        if (!musicSource) return;
        musicSource.clip = null;
    }

    public void PlaySoundFx(string soundFx)
    {
    }
}