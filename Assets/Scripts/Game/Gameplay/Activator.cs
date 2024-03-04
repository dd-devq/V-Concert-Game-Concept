using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Activator : MonoBehaviour
{
    public KeyCode KeyInput;

    [SerializeField]
    private NoteManager _noteManager = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private ButtonDetection _detectButton = null;
    [SerializeField]
    private GameObject _endZone = null;

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
    public GameObject EndZone
    {
        get => _endZone;
    }

    void Update()
    {
        if (spawnIndex < _spawnedTimes.Count)
        {
            if (SongManager.GetAudioSourceTime() >= _spawnedTimes[spawnIndex] - SongManager.Instance.NoteTime)
            {
                var note = _noteManager.OnSpawnNotesToTarget(_endZone.transform.position);
                notes.Add(note);
                spawnIndex++;
            }
        }

        if (inputIndex < _spawnedTimes.Count)
        {
            double timeStamp = _spawnedTimes[inputIndex];
            double marginOfError = SongManager.Instance.MarginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.InputDelayInMilliseconds / 1000.0);

            //if (Input.GetKeyDown(KeyInput))
            //{
            //    if (Math.Abs(audioTime - timeStamp) < marginOfError)
            //    {
            //        //Hit();
            //        Debug.LogError(String.Format("Hit on {0} note", inputIndex + 1));
            //        var temp = notes[inputIndex];
            //        Destroy(temp.gameObject);
            //        inputIndex++;
            //    }
            //    else
            //    {
            //        //Debug.LogError(String.Format("Hit inaccurate on {0} note with {1} delay", inputIndex, Math.Abs(audioTime - timeStamp)));
            //        //Debug.LogError("tre");
            //    }
            //}
            //if (_detectButton.IsButtonClicked())
            //{
            //    if (_isInCollision)
            //    {
            //        Debug.LogError(String.Format("Hit on {0} note", inputIndex + 1));
            //        var temp = notes[inputIndex];
            //        Destroy(temp.gameObject);
            //        inputIndex++;
            //    }
            //    else
            //    {
            //        Debug.LogError("you missed");
            //    }
            //}
            //if (_detectButton.IsButtonClicked())
            //{
            //    if (_isInCollision)
            //    {
            //        GamePlayManager.Instance.OnTriggerNoteHit(inputIndex);
            //    }
            //    else
            //    {
            //        GamePlayManager.Instance.OnTriggerNoteMiss(inputIndex);
            //    }
            //    if (Math.Abs(audioTime - timeStamp) < marginOfError)
            //    {
            //        Hit();
            //        var temp = notes[inputIndex];
            //        Destroy(temp.gameObject);
            //        inputIndex++;
            //    }
            //}
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                inputIndex++;
            }
        }
    }
    public void OnResponseNoteMiss(Component component, object data)
    {
        int index;
        if (data is int)
        {
            index = (int)data;
            var temp = notes[index];
            Destroy(temp.gameObject);
            ScoreManager.Miss();
        }
        else
        {
            Debug.LogError("Wront data pack in OnResponseNoteMiss");
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
