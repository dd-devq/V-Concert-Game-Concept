using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [SerializeField]
    private bool _inTargetZone;
    [SerializeField]
    private Vector3 _endPos;
    public float AssignedTime;
    [Header("Events")]
    public GameEvent onNoteInTargetZone;

    private Vector3 _spawnPos;
    private double _timeInstantiated;
    
    public bool InTargetZone => _inTargetZone;

    public void Init(Transform endPos)
    {

    }

    private void Start()
    {
        this._spawnPos = transform.position;
        _timeInstantiated = SongManager.GetAudioSourceTime();
    }

    private void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - _timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
            //transform.position = Vector3.Lerp(_spawnPos, _endPos, t);
            GetComponent<SpriteRenderer>().enabled = true;  //?
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;

        _inTargetZone = true;
        onNoteInTargetZone.Invoke(this, 100);
        Debug.Log("In");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;
        _inTargetZone = true;
        Debug.Log("Stay");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;
        _inTargetZone = false;
        Invoke(nameof(Kill), 1);
        Debug.Log("Out");
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void Reset()
    {
        // Reset for reuse object pool
    }
}