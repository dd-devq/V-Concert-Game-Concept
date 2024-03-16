using UnityEngine;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MidiNote = Melanchall.DryWetMidi.Interaction.Note;
using NoteName = Melanchall.DryWetMidi.MusicTheory.NoteName;

public class Activator : MonoBehaviour
{
    public KeyCode keyInput;
    public NoteName noteRestriction;

    private List<double> _timeStamps;
    private List<MidiNote> _listNotes;

    [SerializeField] private GameEvent _hitNote;

    [SerializeField] private double _resetTime;

    private int spawnIndex;

    private void Start()
    {
        spawnIndex = 0;
        _timeStamps = new List<double>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(keyInput) )
        {
            Compress();
        }

        if (Input.GetKeyUp(keyInput))
        {
            Decompress();
        }
    }

    public void SetTimeStamps(List<MidiNote> listNotes)
    {
        var tempoMap = SongManager.GetSongTempo();
        foreach (var note in listNotes)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan =
                    TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
                var timeStamp = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds +
                                (double)metricTimeSpan.Milliseconds / 1000f;
                _timeStamps.Add(timeStamp);
                Debug.Log(gameObject.name + ": " + timeStamp);
            }
        }
    }

    private void Compress()
    {
        var newPosition = transform.localPosition;
        newPosition.y /= 2;

        var newScale = transform.localScale;
        newScale.y /= 2;

        transform.localPosition = newPosition;
        transform.localScale = newScale;
    }

    private void Decompress()
    {
        var newPosition = transform.localPosition;
        newPosition.y *= 2;

        var newScale = transform.localScale;
        newScale.y *= 2;

        transform.localPosition = newPosition;
        transform.localScale = newScale;
    }

    public bool CheckHit()
    {
        return false;
    }
}