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
    public float NoteTime;
    public float noteSpawnY;
    public float noteTapY;
    public int InputDelayInMilliseconds;
    public List<TargetZone> TargetZones = new List<TargetZone>();

    private List<Vector3> _lstPosTargetZone = new List<Vector3>();
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
        foreach (var item in TargetZones)
        {
            _lstPosTargetZone.Add(item.gameObject.transform.position);
        }
    }

    private void ReadFromWeb()
    {
        Debug.LogError("Read From Web is being developped!");
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

        foreach (var zone in TargetZones)
        {
            zone.SetSpawnedTimes(listNote);
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
        return (double)Instance.AudioSource.timeSamples / Instance.AudioSource.clip.frequency;
    }
}