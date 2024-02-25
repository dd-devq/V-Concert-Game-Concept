using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : ManualSingletonMono<NoteManager>
{
    //expect to contain target zone data (or get target zone data) and spawn note due to that.
    [SerializeField]
    private SongManager _songManager = null;
    [SerializeField]
    private List<Note> _listNotePrefabs = null;

    [SerializeField]
    private GameObject _spawnObj = null;
    [SerializeField]
    private GameObject _noteContainer = null;

    public override void Awake()
    {
        base.Awake();
    }
    public Note OnSpawnNotesToTarget(Vector3 endPos)
    {
        var idx = Random.Range(0, _listNotePrefabs.Count);
        var note = GCUtils.InstantiateObject<Note>(_listNotePrefabs[idx], _noteContainer.transform);
        note.SetSpawnPosition(_spawnObj.transform.position);
        note.EndPos = endPos;
        note.transform.rotation = _listNotePrefabs[idx].transform.rotation;
        note.transform.localScale = _listNotePrefabs[idx].transform.localScale;
        note.gameObject.SetActive(true);
        return note;
    }
    public void OnNormalHit()
    {
        ScoreManager.Hit();
    }
    public void OnPerfectHit()
    {
        ScoreManager.Hit();
        ScoreManager.Hit();
    }
    public void OnMissHit()
    {
        ScoreManager.Miss();
    }
}
