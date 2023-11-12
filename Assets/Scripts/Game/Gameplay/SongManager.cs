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
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in Lanes)
        {
            lane.SetTimeStamps(array);
        }
        Invoke(nameof(StartSong), SongDelayInSeconds);
    }

    public void StartSong()
    {
        AudioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.AudioSource.timeSamples / Instance.AudioSource.clip.frequency;
    }
}