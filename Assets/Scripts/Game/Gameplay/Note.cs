using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [SerializeField] private bool inTargetZone;

    public bool InTargetZone
    {
        get => inTargetZone;
        set => inTargetZone = value;
    }

    private void Update()
    {
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