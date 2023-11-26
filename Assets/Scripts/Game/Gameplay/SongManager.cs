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
    public static MidiFile Midifile;

    public float SongDelayInSeconds;
    public double MarginOfError; //In Seconds
    public float NoteTime;
    public float noteSpawnY;
    public float noteTapY;
    public int InputDelayInMilliseconds;
    public List<TargetZone> _listTargetZones = new List<TargetZone>();

    private List<Vector3> _lstPosTargetZone = new List<Vector3>();
    private string _songName = "take-me-to-your-heart";
    public List<TargetZone> ListTargetZones
    {
        get => _listTargetZones;
        set => _listTargetZones = value;
    }
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
        foreach (var item in ListTargetZones)
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
        string midiFileName = _songName + ".mid";
        string audioFileName = _songName + ".ogg";
        string midiPath = Path.Combine(Define.MidiFilePath, midiFileName);
        string audioPath = Path.Combine(Define.AudioFilePath, audioFileName);
        Midifile = MidiFile.Read(Application.dataPath + midiPath);
        AudioClip AudioClip = GCUtils.LoadAudioClip(audioPath);
        AudioSource.clip = AudioClip;
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var notes = Midifile.GetNotes();
        List<Melanchall.DryWetMidi.Interaction.Note> listNote = new();
        listNote.AddRange(notes);

        foreach (var zone in ListTargetZones)
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