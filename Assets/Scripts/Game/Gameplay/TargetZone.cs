using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine;
using System.Linq;

public class TargetZone : MonoBehaviour
{
    public KeyCode KeyInput;

    [SerializeField]
    private NoteManager _noteManager = null;

    private List<Double> _spawnedTimes = new(); //timestamp that note spawned (based on midi)
    private List<Note> notes = new();
    private List<NoteName> _pitches = new();
    private int spawnIndex = 0;
    private int inputIndex = 0;
    private int _zoneIndex = 0;

    /// <summary>
    /// from 0 to 3
    /// </summary>
    public int ZoneIndex
    {
        get => _zoneIndex;
        set => _zoneIndex = value;
    }
    public List<NoteName> Pitches
    {
        get => _pitches;
        set => _pitches = value;
    }
    public List<Double> SpawnedTimes
    {
        get => _spawnedTimes;
        set => _spawnedTimes = value;
    }

    void Update()
    {
        if (spawnIndex < _spawnedTimes.Count)
        {
            if (SongManager.GetAudioSourceTime() >= _spawnedTimes[spawnIndex] - SongManager.Instance.NoteTime)
            {
                var note = _noteManager.OnSpawnNotesToTarget(transform.position);
                notes.Add(note);
                spawnIndex++;
            }
        }

        if (inputIndex < _spawnedTimes.Count)
        {
            double timeStamp = _spawnedTimes[inputIndex];
            double marginOfError = SongManager.Instance.MarginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.InputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(KeyInput))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    //Hit();
                    Debug.LogError(String.Format("Hit on {0} note", inputIndex + 1));
                    var temp = notes[inputIndex];
                    //notes.RemoveAt(inputIndex);
                    Destroy(temp.gameObject);
                    inputIndex++;
                }
                else
                {
                    //Debug.LogError(String.Format("Hit inaccurate on {0} note with {1} delay", inputIndex, Math.Abs(audioTime - timeStamp)));
                    //Debug.LogError("tre");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                //Miss();
                Debug.LogError(String.Format("Missed {0} note", inputIndex));
                inputIndex++;
            }
        }
    }
    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }
}
