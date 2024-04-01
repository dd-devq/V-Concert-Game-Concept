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
    public AudioSource AudioSource = null;
    public static MidiFile Midifile = null;
    [SerializeField]
    private ActivatorManager _ActivatorManager = null;
    [SerializeField]
    private float _songDelayInSeconds = 0;
    [SerializeField]
    private double _marginOfError = 0; //In Seconds
    [SerializeField]
    private float _noteTime = 1;
    [SerializeField]
    private int _inputDelayInMilliseconds = 0;
    //public float noteSpawnY = 0;
    //public float noteTapY = 0;

    private List<Vector3> _lstPosActivator = new List<Vector3>();
    public string _songName = "take-me-to-your-heart";

    public float NoteTime
    {
        get => _noteTime;
        set => _noteTime = value;
    }
    public double MarginOfError
    {
        get => _marginOfError;
        set => _marginOfError = value;
    }
    public float SongDelayInSeconds
    {
        get => _songDelayInSeconds;
        set => _songDelayInSeconds = value;
    }
    public int InputDelayInMilliseconds
    {
        get => _inputDelayInMilliseconds;
        set => _inputDelayInMilliseconds = value;
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
        foreach (var item in _ActivatorManager.Activators)
        {
            _lstPosActivator.Add(item.gameObject.transform.position);
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
        _ActivatorManager.SetSpawnedTimes(listNote);
        //foreach (var zone in _ActivatorManager.Activators)
        //{
        //    zone.SetSpawnedTimes(listNote);
        //}
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