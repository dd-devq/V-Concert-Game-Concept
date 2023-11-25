using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static int NoteInterval = 4;

    public enum InputMode
    {
        SingleKey,
        MultiKey,
    }

    public struct GameplayData
    {
        public int SongIndex;
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
    
}