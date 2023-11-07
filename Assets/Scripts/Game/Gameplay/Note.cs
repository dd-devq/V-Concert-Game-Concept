using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [SerializeField]
    private bool inTargetZone;
    public float AssignedTime;
    private double _timeInstantiated;
    

    public bool InTargetZone
    {
        get => inTargetZone;
        set => inTargetZone = value;
    }
    private void Start()
    {
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
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("In");
        if (other.CompareTag("Target Zone"))
        {
            inTargetZone = true;
            Debug.Log("In");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target Zone"))
        {
            inTargetZone = false;
            Debug.Log("Out");
        }
    }
}