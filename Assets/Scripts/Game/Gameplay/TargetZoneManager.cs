using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Interaction;

public class TargetZoneManager : ManualSingletonMono<TargetZoneManager>
{
    private List<TargetZone> _targetZones = new();

    public List<TargetZone> TargetZones
    {
        get => _targetZones;
        set => _targetZones = value;
    }
    public override void Awake()
    {
        base.Awake();
        GetListTargetZones();
    }
    private void Start()
    {
        
    }

    private void GetListTargetZones()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            _targetZones.Add(child.GetComponent<TargetZone>());
        }
        for (var i = 0; i < _targetZones.Count; i++)
        {
            _targetZones[i].ZoneIndex = i;
        }
    }

    public void SetSpawnedTimes(List<Melanchall.DryWetMidi.Interaction.Note> listNotes)
    {
        double interval = 0;
        Dictionary<NoteName, int> PitchNameDict = new();
        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            if (i == 0 || i == listNotes.Count - 1)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
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
                    interval = spawnedTime;
                    if (!PitchNameDict.ContainsKey(note.NoteName))
                    {
                        PitchNameDict.Add(note.NoteName, 1);
                    }
                    else PitchNameDict[note.NoteName] += 1;
                }
            }
        }
        //sort dictionary
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
                int PitchPerZone = countOfPitch / Define.NumOfTargetZone;
                for (var i = 0; i < PitchNameDict.Count; i+=PitchPerZone)
                {
                    foreach (var zone in _targetZones)
                    {
                        if (zone.ZoneIndex == i/PitchPerZone)
                        {
                            zone.Pitches.Add(PitchNameDict.ElementAt(i).Key);
                            zone.Pitches.Add(PitchNameDict.ElementAt(i + 1).Key);
                        }
                    }
                }
                break;
            case 1:
                for (var i = 0; i < _targetZones.Count; i++)
                {

                }
                break;
            case 2:
                for (var i = 0; i < _targetZones.Count; i++)
                {

                }
                break;
            case 3:
                for (var i = 0; i < _targetZones.Count; i++)
                {

                }
                break;
        }
    }
}
