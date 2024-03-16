using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MidiNote = Melanchall.DryWetMidi.Interaction.Note;

public class SongManager : ManualSingletonMono<SongManager>
{
    private static MidiFile _songMidiFile;
    private float _songDelayInSeconds;

    public GameEvent startSong;

    [SerializeField] private string _songName;

    private void Start()
    {
        ReadFromFile();
    }

    private void ReadFromFile()
    {
        var midiFileName = _songName + ".mid";
        var midiFilePath = Path.Combine(Define.MidiFilePath, midiFileName);
        _songMidiFile = MidiFile.Read(Application.dataPath + midiFilePath);
        if (_songMidiFile == null)
        {
            Debug.LogError("Unknown Midi File: " + midiFileName);
            Debug.LogError("Unknown Resources: " + midiFilePath);
        }
        else
        {
            GetDataFromMidi();
        }
    }

    private void GetDataFromMidi()
    {
        var notes = _songMidiFile.GetNotes();

        List<MidiNote> listNote = new();
        listNote.AddRange(notes);
        Debug.Log(listNote.Count);
        foreach (var activator in ActivatorManager.Instance.activators)
        {
            activator.SetTimeStamps(listNote);
        }

        Invoke(nameof(StartSong), _songDelayInSeconds);
    }

    private void StartSong()
    {
        // startSong.Invoke(this, true);
    }

    public static TempoMap GetSongTempo()
    {
        return _songMidiFile.GetTempoMap();
    }
}