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
    public List<double> SpawnedTimes = new List<double>(); //thoi gian note xuat hien (theo midi)

    [SerializeField]
    private NoteManager _noteManager = null;
    private List<Note> notes = new();
    private List<NoteName> _pitches = new();
    private int spawnIndex = 0;
    private int inputIndex = 0;
    private int _zoneIndex = 0;

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
        Dictionary<NoteName, int> PitchNameDict = new();

        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            //Debug.LogError("check note Name: " + note.NoteName.ToString());
            if (i == 0 || i == listNotes.Count - 1)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                //Debug.LogError("spawnedtime: " + spawnedTime);
                SpawnedTimes.Add(spawnedTime);
                interval = spawnedTime;

                if (!PitchNameDict.ContainsKey(note.NoteName))
                {
                    PitchNameDict.Add(note.NoteName, 1);
                }
                else PitchNameDict[note.NoteName] += 1;
            }
            else
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                if (spawnedTime - interval >= Define.NoteInterval)
                {
                    //Debug.LogError("spawnedtime: " + spawnedTime);
                    SpawnedTimes.Add(spawnedTime);
                    interval = spawnedTime;

                    if (!PitchNameDict.ContainsKey(note.NoteName))
                    {
                        PitchNameDict.Add(note.NoteName, 1);
                    }
                    else PitchNameDict[note.NoteName] += 1;
                }
            }
        }
        List<KeyValuePair<NoteName, int>> sortedList = PitchNameDict.ToList();
        sortedList.Sort((x, y) => x.Value.CompareTo(y.Value));
        PitchNameDict = sortedList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var pair in PitchNameDict)
        {
            Debug.LogError(pair.Key + " " + pair.Value);
        }
        int countOfPitch = PitchNameDict.Count;
        switch (countOfPitch % Define.NumOfTargetZone)
        {
            case 0:
                foreach (var pair in PitchNameDict)
                {

                }
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }
}
