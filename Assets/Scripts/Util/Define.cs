using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static int NoteInterval = 4;

    public static string MidiFilePath = "/Resources/Audio/MIDI/";
    public static string AudioFilePath = Application.streamingAssetsPath + "/Audio/";
    public enum InputMode
    {
        SingleKey,
        MultiKey,
    }
    public enum PrefabName
    {
        NotePrefab
    }
}
