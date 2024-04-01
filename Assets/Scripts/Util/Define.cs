using System;
using UnityEngine;


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
        UIVictory,
        None
    }
}

namespace EventData
{
    public struct RegisterInfo
    {
        public string Username;
        public string Password;
        public string Email;
        public Action RegisterFailCallback;
        public Action RegisterSuccessCallback;
    }

    public struct LoginInfo
    {
        public string Username;
        public string Password;
        public Action LoginFailCallback;
        public Action LoginSuccessCallback;
    }

    public struct ResetInfo
    {
        public string Username;
    }
}

public static class Define
{
    //the gap time between 2 notes
    public static int NoteInterval = 2;
    public static int NumOfActivators = 4;

    public static string MidiFilePath = "/Resources/Audio/MIDI/";
    public static string AudioFilePath = Application.streamingAssetsPath + "/Audio/";

    public static int BaseScore = 5;
    public static int NormalHit = 10;
    public static int GoodHit = 20;
    public static int PerfectHit = 100;

    public static float HitObjectInterval = 0.2f;

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
    
}