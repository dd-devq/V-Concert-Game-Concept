using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject Note;


    void Update()
    {
//        Invoke(nameof(SpawnNote), 3);
    }

    private void SpawnNote()
    {
        Instantiate(Note, this.transform);
    }
}