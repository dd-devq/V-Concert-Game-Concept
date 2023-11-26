using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName NoteRestriction;
    public KeyCode KeyInput;
    public List<double> SpawnedTimes = new List<double>(); //thoi gian note xuat hien (theo midi)

    [SerializeField]
    private NoteManager _noteManager = null;
    private List<Note> notes = new List<Note>();
    private int spawnIndex = 0;
    private int inputIndex = 0;

    void Update()
    {
        if (spawnIndex < SpawnedTimes.Count)
        {
            if (SongManager.GetAudioSourceTime() >= SpawnedTimes[spawnIndex] - SongManager.Instance.NoteTime)
            {
                var note = _noteManager.OnSpawnNotes();
                notes.Add(note);
                spawnIndex++;
            }
        }

        if (inputIndex < SpawnedTimes.Count)
        {
            double timeStamp = SpawnedTimes[inputIndex];
            double marginOfError = SongManager.Instance.MarginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.InputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(KeyInput))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    //Hit();
                    Debug.LogError(String.Format("Hit on {0} note", inputIndex));
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
    public void SetSpawnedTimes(List<Melanchall.DryWetMidi.Interaction.Note> listNotes)
    {
        double interval = 0;
        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            if (i == 0 || i == listNotes.Count - 1)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                //Debug.LogError("spawnedtime: " + spawnedTime);
                SpawnedTimes.Add(spawnedTime);
                interval = spawnedTime;
            }
            else
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                if (spawnedTime - interval >= 4)
                {
                    //Debug.LogError("spawnedtime: " + spawnedTime);
                    SpawnedTimes.Add(spawnedTime);
                    interval = spawnedTime;
                }
            }

        }
    }
}
