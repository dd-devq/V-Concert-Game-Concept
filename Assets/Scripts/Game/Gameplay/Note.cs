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

    private Vector3 _spawnPos;
    private Vector3 _endPos;
    private double _timeInstantiated;
    private bool _inActivator = false;
    private bool _inPerfectHit = false;
    private bool _isHit = false;

    public Vector3 SpawnPos
    {
        get => _spawnPos;
        set => _spawnPos = value;
    }
    public Vector3 EndPos
    {
        get => _endPos;
        set => _endPos = value;
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

            if (t > 1.1)
            {
                OnFinishNotes();
            }
            else
            {
                transform.position = Vector3.Lerp(_spawnPos, _endPos, t);
            }
            if (Input.GetKeyDown(InputManager.KeyInput) && !_isHit)
            {
                if (_inActivator && !_inPerfectHit)
                {
                    Debug.LogError("Normal Hit");
                    NoteManager.Instance.OnNormalHit();
                    _isHit = true;
                }
                else if (_inPerfectHit)
                {
                    Debug.LogError("Perfect Hit");
                    NoteManager.Instance.OnPerfectHit();
                    _isHit = true;
                }
                else if (!_inActivator && !_inPerfectHit)
                {
                    NoteManager.Instance.OnMissHit();
                    Debug.LogError("Missed Click");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Define.Tags.Activator.ToString()))
        {
            _inActivator = true;
            //Debug.LogError("Trigger Normal");
        }
        if (other.CompareTag(Define.Tags.PerfectHit.ToString()))
        {
            _inPerfectHit = true;
            //Debug.LogError("Trigger Perfect");
        } 
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}