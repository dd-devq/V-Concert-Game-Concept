using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //expect to contain target zone data (or get target zone data) and spawn note due to that.
    [SerializeField]
    private SongManager _songManager = null;
    [SerializeField]
    private Note _notePrefab = null;

    [SerializeField]
    private GameObject _spawnObj = null;
    [SerializeField]
    private GameObject _endObj = null;
    [SerializeField]
    private GameObject _noteContainer = null;
    public Note OnSpawnNotesToTarget(Vector3 endPos)
    {
        var note = GCUtils.InstantiateObject<Note>(_notePrefab, _noteContainer.transform);
        note.SpawnPos = _spawnObj.transform.position;
        note.EndPos = endPos;
        note.transform.rotation = _notePrefab.transform.rotation;
        note.transform.localScale = _notePrefab.transform.localScale;
        note.gameObject.SetActive(true);
        return note;
    }
}
