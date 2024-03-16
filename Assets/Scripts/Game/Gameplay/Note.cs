using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [Header("Events")] private Vector3 _spawnPos;
    private Vector3 _targetPos;

    private double _instantiatedTime;
    private double _assignedTime;

    public void SetSpawnPosition(Vector3 spawnPos)
    {
        _spawnPos = spawnPos;
        transform.position = _spawnPos;
    }


    public void SetTargetPosition(Vector3 targetPos)
    {
        _targetPos = targetPos;
    }

    public void Move()
    {
    }
    
    private void Update()
    {
    }
}