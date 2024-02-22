using System;
using UnityEngine;

public class EventData
{
}

namespace EventType
{
}

namespace UI
{
    public class UIParam
    {
    }

    public enum UIIndex
    {
        UIAuthentication,
        UICharacter,
        UIInventory,
        UIItem,
        UILoading,
        UILobby,
        UIMainMenu,
        UIPause,
        UISetting,
        UIShop,
        UISongSelection,
        UIVictory
    }
}

public static class Define
{
    public static int NoteInterval = 2;
    public static int NumOfActivators = 4;

    public static string MidiFilePath = "/Resources/Audio/MIDI/";
    public static string AudioFilePath = Application.streamingAssetsPath + "/Audio/";

    public static int NormalHit = 10;
    public static int GoodHit = 20;
    public static int PerfectHit = 100;

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
    public class GameplayData : EventData
    {
        public int SongIndex;

        public SceneIndex SceneIndex;
        //character
    }

    public class EndGameData : EventData
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
    }

    public struct RegisterInfo
    {
        public string Username;
        public string Password;
        public string Email;
        public Action RegisterFailCallback;
        public Action RegisterSuccessCallback;
    }

    public class LoginInfo
    {
        public string username;
        public string password;
        public Action onLoginFail;
        public Action onLoginSuccess;
    }
}