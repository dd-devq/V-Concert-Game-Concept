using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName NoteRestriction;
    public KeyCode KeyInput;
    [SerializeField]
    private Note _notePrefab;
    public List<double> TimeStamps = new List<double>(); //thoi gian note xuat hien (theo midi)
    public GameObject NoteContainer = null;
    private List<Note> notes = new List<Note>();

    private int spawnIndex = 0;
    private int inputIndex = 0;

    public GameObject SpawnObj = null;
    public GameObject EndObj = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < TimeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= TimeStamps[spawnIndex] - SongManager.Instance.NoteTime)
            {
                var note = GCUtils.InstantiateObject<Note>(_notePrefab, NoteContainer.transform);
                note.SpawnPos = SpawnObj.transform.position;
                note.EndPos = EndObj.transform.position;
                note.transform.rotation = _notePrefab.transform.rotation;
                note.transform.localScale = _notePrefab.transform.localScale;
                note.gameObject.SetActive(true);
                notes.Add(note);
                note.AssignedTime = (float)TimeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < TimeStamps.Count)
        {
            double timeStamp = TimeStamps[inputIndex];
            double marginOfError = SongManager.Instance.MarginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.InputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(KeyInput))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    //Hit();
                    Debug.LogError(String.Format("Hit on {0} note", inputIndex));
                    var temp = notes[inputIndex];
                    notes.RemoveAt(inputIndex);
                    Destroy(temp.gameObject);
                    inputIndex++;
                }
                else
                {
                    Debug.LogError(String.Format("Hit inaccurate on {0} note with {1} delay", inputIndex, Math.Abs(audioTime - timeStamp)));
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
    public void SetTimeStamps(List<Melanchall.DryWetMidi.Interaction.Note> listNotes)
    {
        //double result = SongManager.Instance.AudioSource.clip.length / (float)Define.NoteInterval;
        //int numOfNote = (int)Math.Round(result);
        double interval = 0;
        for (var i = 0; i < listNotes.Count; i++)
        {
            var note = listNotes[i];
            if (i == 0 || i == listNotes.Count - 1)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                //Debug.LogError("spawnedtime: " + spawnedTime);
                TimeStamps.Add(spawnedTime);
                interval = spawnedTime;
            }
            else
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.Midifile.GetTempoMap());
                double spawnedTime = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                if (spawnedTime - interval >= 4)
                {
                    //Debug.LogError("spawnedtime: " + spawnedTime);
                    TimeStamps.Add(spawnedTime);
                    interval = spawnedTime;
                }
            }

        }
    }
}
