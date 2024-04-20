using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Song Data")]
public class SongData : ScriptableObject
{
    public string SongPath;
    public string CoverPath;
    public List<Song> ListSong;
}