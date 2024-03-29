using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onNoteInActivator;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private double _timeInstantiated;
    private bool _inActivator = false;
    private bool _inPerfectHit = false;
    private bool _isHit = false;

    public Vector3 EndPos
    {
        get => _endPos;
        set => _endPos = value;
    }

    public Vector3 StartPos
    {
        get => _startPos;
        set => _startPos = value;
    }

    private void Start()
    {
        _timeInstantiated = SongManager.GetAudioSourceTime();
    }

    private void Update()
    {
        if (gameObject != null && gameObject.name != Define.PrefabName.NotePrefab.ToString())
        {
            double timeSinceInstantiated = SongManager.GetAudioSourceTime() - _timeInstantiated;
            float t = (float)(timeSinceInstantiated / SongManager.Instance.NoteTime);
            
            //means the timeSinceInstantiated is greater than the NoteTime
            if (t > 1)
            {
                OnFinishNotes();
            }
            else
            {
                transform.position = Vector3.Lerp(_startPos, _endPos, t);
            }
            if (Input.GetKeyDown(InputManager.KeyInput) && !_isHit)
            {
                if (_inActivator && !_inPerfectHit)
                {
                    Debug.LogError("Normal Hit");
                    NoteManager.Instance.OnNormalHit();
                    _isHit = true;
                    OnFinishNotes();
                }
                else if (_inPerfectHit)
                {
                    Debug.LogError("Perfect Hit");
                    NoteManager.Instance.OnPerfectHit();
                    _isHit = true;
                    OnFinishNotes();
                }
                else if (!_inActivator && !_inPerfectHit)
                {
                    NoteManager.Instance.OnMissHit();
                    Debug.LogError("Missed Click");
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnFinishNotes()
    {
        if (!_isHit)
        {
            Debug.LogError("Missed!");
        }
        Destroy(gameObject);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}