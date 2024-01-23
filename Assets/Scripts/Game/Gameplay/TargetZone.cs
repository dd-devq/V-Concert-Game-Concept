using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public void OnResponseNoteEnd(Component component, object data)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Define.Tags.Note.ToString())
        {

        }
    }
}
