using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MidiNote = Melanchall.DryWetMidi.Interaction.Note;

public class Conductor : ManualSingletonMono<Conductor>
{
    public float bpm;
    public float crotchet;
    public float firstBeatOffset;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float songPitch;

    public SongState songState;
    public float errorThreshold;
    private static MidiFile _songMidiFile;


    [SerializeField] private string _songName;


    private void Start()
    {
        songState = SongState.Play;
        songPosition = 0f;
        crotchet = 60f / bpm;
        ReadFromFile();
    }

    private void Update()
    {
        if (songState == SongState.Play)
        {
            songPosition += (float)((AudioSettings.dspTime - dspSongTime) * songPitch - firstBeatOffset);
            songPositionInBeats = songPosition / crotchet;
        }
    }


    #region Midi

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
    }

    public static TempoMap GetSongTempo()
    {
        return _songMidiFile.GetTempoMap();
    }

    #endregion


    #region Event

    public void StartSong(Component sender, object data)
    {
        dspSongTime = (float)AudioSettings.dspTime;
        songState = SongState.Play;
    }

    public void PauseSong(Component sender, object data)
    {
        songState = SongState.Pause;
    }

    #endregion
}