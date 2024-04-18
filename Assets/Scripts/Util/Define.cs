using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

namespace UI
{
    public class UIParam
    {
        public object Data;
    }

    public enum UIIndex
    {
        UIAuthentication,
        UIMainMenu,
        UIInventory,
        UIShop,
        UISetting,
        UIItemViewer,
        UICharacterSelection,
        UIModeSelection,
        UISongSelection,
        UIAvatarSelection,
        UILobby,
        UILoading,
        UINavigationTab,
        UIHUD,
        UIVictory,
        UIPause,
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
    
    public struct AutoLoginInfo
    {
        public Action AutoLoginFailCallback;
        public Action AutoLoginSuccessCallback;
    }

    public struct ResetInfo
    {
        public string Username;
    }

    public struct UserData
    {
        public Dictionary<string, int> virtualCurrency;
        public List<ItemInstance> playerInventory;
    }

    public struct GameData
    {
        public List<ShopItem> ListShopItems;
    }
}

public class CharacterData
{
    public string characterModel;
    public string playerAvatar;
    public string songData;
}

public class ShopItem
{
    public string name;
    public int price;
    public Currency type;
}

public enum HitType
{
    Hit,
    Good,
    Perfect,
    Miss
}

public enum SongState
{
    Play,
    Pause,
    End
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