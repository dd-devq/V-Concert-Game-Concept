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
    private GameObject _endZone = null;
    [SerializeField]
    private GameObject _startZone = null;
    [SerializeField]
    private GameObject _hitZone = null;

    private List<Double> _spawnedTimes = new(); //timestamp that note spawned (based on midi)
    private List<Note> notes = new();
    private List<NoteName> _pitches = new();
    private int spawnIndex = 0;
    private int inputIndex = 0;
    private int _zoneIndex = 0;

    private Vector3 _originalHitPos = new Vector3(0, 0, 0);
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
    public GameObject StartZone
    {
        get => _startZone;
    }
    public GameObject EndZone
    {
        get => _endZone;
    }

    void Update()
    {
        if (spawnIndex < _spawnedTimes.Count)
        {
            //spawn note truoc 1 khoang thoi gian NoteTime
            if (SongManager.GetAudioSourceTime() >= _spawnedTimes[spawnIndex] - SongManager.Instance.NoteTime)
            {
                var note = _noteManager.OnSpawnNotesToTarget(_startZone.transform.position, _endZone.transform.position, _hitZone.transform.position);
                notes.Add(note);
                spawnIndex++;
            }
        }

        if (Input.GetKeyDown(KeyInput))
        {
            OnClickDownKeyInput();
        }
        if (Input.GetKeyUp(KeyInput))
        {
            OnClickUpKeyInput();
        }
        //if (inputIndex < _spawnedTimes.Count)
        //{
        //    double timeStamp = _spawnedTimes[inputIndex];
        //    double marginOfError = SongManager.Instance.MarginOfError;
        //    double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.InputDelayInMilliseconds / 1000.0);

        //    if (Input.GetKeyDown(KeyInput))
        //    {
        //        if (Math.Abs(audioTime - timeStamp) < marginOfError)
        //        {
        //            //Hit();
        //            Debug.LogError(String.Format("Hit on {0} note", inputIndex + 1));
        //            var temp = notes[inputIndex];
        //            Destroy(temp.gameObject);
        //            inputIndex++;
        //        }
        //        else
        //        {
        //            //Debug.LogError(String.Format("Hit inaccurate on {0} note with {1} delay", inputIndex, Math.Abs(audioTime - timeStamp)));
        //            //Debug.LogError("tre");
        //        }
        //    }

        //    if (timeStamp + marginOfError <= audioTime)
        //    {
        //        ScoreManager.Miss();
        //        inputIndex++;
        //    }
        //}
    }

    private void OnClickDownKeyInput()
    {
        //move hitObject 1 little bit down
        _originalHitPos = _hitZone.transform.position;
        _hitZone.transform.position = new Vector3(_hitZone.transform.position.x, 
            _hitZone.transform.position.y - 0.4f, _hitZone.transform.position.z);
    }
    private void OnClickUpKeyInput()
    {
        //move hitObject back to original position
        _hitZone.transform.position = _originalHitPos;
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
}
