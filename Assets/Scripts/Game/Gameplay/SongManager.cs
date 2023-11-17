using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.Networking;
using System;

public class SongManager : ManualSingletonMono<SongManager>
{
    public AudioSource AudioSource;
    public string fileLocation;
    public static MidiFile Midifile;

    public float SongDelayInSeconds;
    public double MarginOfError; //In Seconds
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    public int InputDelayInMilliseconds;
    public Lane[] Lanes;
    public float noteDespawnY
    {
        get { return noteTapY - (noteSpawnY - noteTapY); }
    }

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Debug.LogError(Application.streamingAssetsPath);
        if (Application.streamingAssetsPath.StartsWith("http://") ||
            Application.streamingAssetsPath.StartsWith("https://"))
        {
            ReadFromWeb();
        }
        else
        {
            ReadFromFile();
        }
    }

    private void ReadFromWeb()
    {
    }

    private void ReadFromFile()
    {
        Midifile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var notes = Midifile.GetNotes();
        List<Melanchall.DryWetMidi.Interaction.Note> listNote = new();
        listNote.AddRange(notes);

        foreach (var lane in Lanes)
        {
            lane.SetTimeStamps(listNote);
        }
        Invoke(nameof(StartSong), SongDelayInSeconds);
    }

    public void StartSong()
    {
        AudioSource.Play();
    }

    /// <summary>
    /// return current real-time in audio clip played.
    /// </summary>
    public static double GetAudioSourceTime()
    {
        //Debug.LogError("GetAudioSourceTime " + (double)Instance.AudioSource.timeSamples / Instance.AudioSource.clip.frequency);
        return (double)Instance.AudioSource.timeSamples / Instance.AudioSource.clip.frequency;
    }
}