using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [SerializeField] private bool inTargetZone;
    [SerializeField] private Vector3 _endPos;
    private Vector3 _spawnPos;
    [Header("Events")] public GameEvent onNoteInTargetZone;

    public bool InTargetZone => inTargetZone;

    public void Init(Transform endPos)
    {
    }

    private void Start()
    {
        this._spawnPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_spawnPos, _endPos, Time.time * 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;

        inTargetZone = true;
        onNoteInTargetZone.Invoke(this, 100);
        Debug.Log("In");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;
        inTargetZone = true;
        Debug.Log("Stay");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Target Zone")) return;
        inTargetZone = false;
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