using System;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class Conductor : MonoBehaviour
{
    public float BPM;
    public float Crotchet;
    public float Offset;
    public float SongPosition;
    public float LastBeat;

    public AudioSource AudioSource;

    private MidiFile _midiFile;

    private void Start()
    {
        _midiFile = MidiFile.Read(Application.dataPath + "/Resources/Audio/MIDI/Never-Gonna-Give-You-Up.mid");
        Debug.Log(_midiFile);
        Debug.Log("Hello");

    }

    private void Update()
    {
    }
}