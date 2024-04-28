using System.Collections.Generic;
using EventData;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab;

public class PlayFabPlayerDataController : PersistentManager<PlayFabPlayerDataController>
{
    public string playerId;
    private Dictionary<string, int> Currencies { get; } = new();
    public List<ItemInstance> Inventory { get; } = new();
    public GameEvent onPlayerDataRetrieve;
    public Dictionary<string, UserDataRecord> PlayerTitleData;

    public void GetAllData()
    {
        GetInventory(null, null);
        GetPlayerData();
    }

    public void GetInventory(Component sender, object data)
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            Currencies.Clear();
            foreach (var pair in result.VirtualCurrency)
            {
                Currencies.Add(pair.Key, pair.Value);
            }

            var playerData = new UserData
            {
                Coin = Currencies["CN"],
                Gem = Currencies["GM"],
                Username = "User"
            };
            onPlayerDataRetrieve.Invoke(this, playerData);

            Inventory.Clear();
            foreach (var item in result.Inventory)
            {
                Inventory.Add(item);
            }
        }, PlayFabErrorHandler.HandleError);
    }

    public void SetPlayerData(Component sender, object data)
    {
        var tmp = (Dictionary<string, string>)data;

        var req = new UpdateUserDataRequest
        {
            Data = tmp
        };

        PlayFabClientAPI.UpdateUserData(req, _ =>
        {
            if (PlayerTitleData != null)
            {
                foreach (var key in tmp.Keys)
                {
                    UserDataRecord value = new() { Value = tmp[key] };
                    PlayerTitleData[key] = value;
                }
            }
        }, PlayFabErrorHandler.HandleError);
    }


    private void GetPlayerData()
    {
        var listDataKeys = Resources.Load<PlayerData>("Scriptable Objects/Player Data Key").PlayerDataKeys;
        var req = new GetUserDataRequest
        {
            Keys = listDataKeys,
            PlayFabId = playerId
        };
        PlayFabClientAPI.GetUserData(req, result => { PlayerTitleData = result.Data; },
            PlayFabErrorHandler.HandleError);
    }

    public void AddCurrency(Component sender, object data)
    {
        var temp = (RewardData)data;
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = temp.Key,
            Amount = temp.Amount
        }, null, PlayFabErrorHandler.HandleError);
    }

    public void SubtractCurrency(string key, int amount)
    {
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = key,
            Amount = amount
        }, null, PlayFabErrorHandler.HandleError);
    }
}