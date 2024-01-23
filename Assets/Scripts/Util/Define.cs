using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static int NoteInterval = 2;
    public static int NumOfActivators = 4;

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
    public enum Tags
    {
        Activator,
        PerfectHit,
        Note,
    }

    [Serializable]
    public struct GameplayData
    {
        public int SongIndex;

        public SceneIndex SceneIndex;
        //character
    }

    public struct EndGameData
    {
        public int Score;
        public int Coins;
        public int Gems;
    }

    public struct CharacterData
    {
        public string ModelName;
    }

    public struct ItemData
    {
        public string ItemName;
        public int Price;
        public Currency Type;
    }

    public enum Currency
    {
        Coin,
        Gem,
    }

    public struct RegisterInfo
    {
        private string username;
        private string password;
    }
    
}