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
    private Dictionary<NoteName, int> _pitchNameDict = new();
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
    private TargetZone GetTargetZoneByIndex(int index)
    {
        for (var i = 0; i < _targetZones.Count; i++)
        {
            if (_targetZones[i].ZoneIndex == index)
                return _targetZones[i];
        }
        return null;
    }

    private void GetPitchesName(List<Melanchall.DryWetMidi.Interaction.Note> listNotes)
    {
        double interval = 0;
        //Get Pitch Name that match condition
        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            if (i == 0 || i == listNotes.Count - 1)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                interval = spawnedTime;
                if (!_pitchNameDict.ContainsKey(note.NoteName))
                {
                    _pitchNameDict.Add(note.NoteName, 1);
                }
                else _pitchNameDict[note.NoteName] += 1;
            }
            else
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                if (spawnedTime - interval >= Define.NoteInterval)
                {
                    interval = spawnedTime;
                    if (!_pitchNameDict.ContainsKey(note.NoteName))
                    {
                        _pitchNameDict.Add(note.NoteName, 1);
                    }
                    else _pitchNameDict[note.NoteName] += 1;
                }
            }
        }
    }
    private void DividePitchesIntoZones()
    {
        //Divide pitches to zones
        int countOfPitch = _pitchNameDict.Count;
        int PitchPerZone = 0;
        int index = 0;
        switch (countOfPitch % Define.NumOfTargetZone)
        {
            case 0:
                PitchPerZone = countOfPitch / Define.NumOfTargetZone;
                for (var i = 0; i < _pitchNameDict.Count; i += PitchPerZone)
                {
                    foreach (var zone in _targetZones)
                    {
                        if (zone.ZoneIndex == i / PitchPerZone)
                        {
                            for (var j = 0; j < PitchPerZone; j++)
                            {
                                zone.Pitches.Add(_pitchNameDict.ElementAt(i + j).Key);
                            }
                        }
                    }
                }
                break;
            case 1:
                PitchPerZone = (countOfPitch - 1) / Define.NumOfTargetZone;
                index = 0;
                for (var i = 0; i < _pitchNameDict.Count; i++)
                {
                    if (i <= PitchPerZone)
                    {
                        index = 0;
                    }
                    if (i > PitchPerZone && index <= PitchPerZone * 2)
                    {
                        index = 1;
                    }
                    if (i > PitchPerZone * 2 && i <= PitchPerZone * 3)
                    {
                        index = 2;
                    }
                    if (i > PitchPerZone * 3 && i <= PitchPerZone * 4)
                    {
                        index = 3;
                    }
                    GetTargetZoneByIndex(index).Pitches.Add(_pitchNameDict.ElementAt(i).Key);
                }
                break;
            case 2:
                PitchPerZone = (countOfPitch - 2) / Define.NumOfTargetZone;
                index = 0;
                for (var i = 0; i < _pitchNameDict.Count; i++)
                {
                    if (i <= PitchPerZone)
                    {
                        index = 0;
                    }
                    if (i > PitchPerZone && index <= PitchPerZone * 2 + 1)
                    {
                        index = 1;
                    }
                    if (i > PitchPerZone * 2 + 1 && i <= PitchPerZone * 3 + 1)
                    {
                        index = 2;
                    }
                    if (i > PitchPerZone * 3 + 1 && i <= PitchPerZone * 4 + 1)
                    {
                        index = 3;
                    }
                    GetTargetZoneByIndex(index).Pitches.Add(_pitchNameDict.ElementAt(i).Key);
                }
                break;
            case 3:
                PitchPerZone = (countOfPitch - 3) / Define.NumOfTargetZone;
                index = 0;
                for (var i = 0; i < _pitchNameDict.Count; i++)
                {
                    if (i <= PitchPerZone)
                    {
                        index = 0;
                    }
                    if (i > PitchPerZone && index <= PitchPerZone * 2 + 1)
                    {
                        index = 1;
                    }
                    if (i > PitchPerZone * 2 + 1 && i <= PitchPerZone * 3 + 2)
                    {
                        index = 2;
                    }
                    if (i > PitchPerZone * 3 + 2 && i <= PitchPerZone * 4 + 2)
                    {
                        index = 3;
                    }
                    GetTargetZoneByIndex(index).Pitches.Add(_pitchNameDict.ElementAt(i).Key);
                }
                break;
        }
        //foreach (var zone in _targetZones)
        //{
        //    foreach (var pitch in zone.Pitches)
        //    {
        //        Debug.LogError(zone.ZoneIndex + " " + pitch);
        //    }
        //}
    }
    public void SetSpawnedTimes(List<Melanchall.DryWetMidi.Interaction.Note> listNotes)
    {
        GetPitchesName(listNotes);
        //sort dictionary
        List<KeyValuePair<NoteName, int>> sortedList = _pitchNameDict.ToList();
        sortedList.Sort((x, y) => x.Value.CompareTo(y.Value));
        _pitchNameDict = sortedList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        //foreach (var pair in _pitchNameDict)
        //{
        //    Debug.LogError("check: " + pair.Key + " " + pair.Value);
        //}
        DividePitchesIntoZones();

        double interval = 0;
        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            if (i == 0 || i == listNotes.Count - 1)
            {
                foreach (var zone in _targetZones)
                {
                    if (zone.Pitches.Contains(note.NoteName))
                    {
                        var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                        double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                        zone.SpawnedTimes.Add(spawnedTime);
                        interval = spawnedTime;
                    }
                }
            }
            else
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                if (spawnedTime - interval >= Define.NoteInterval)
                {
                    foreach (var zone in _targetZones)
                    {
                        if (zone.Pitches.Contains(note.NoteName))
                        {
                            zone.SpawnedTimes.Add(spawnedTime);
                            interval = spawnedTime;
                        }
                    }
                }
            }
        }
    }
}
