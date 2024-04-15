using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab;

public class PlayfabPlayerDataController : PersistentManager<PlayfabPlayerDataController>
{
    private readonly Dictionary<string, int> _currencies = new();
    public Dictionary<string, int> Currencies => _currencies;

    private readonly List<ItemInstance> _inventory = new();
    public List<ItemInstance> Inventory => _inventory;


    public void GetAllData()
    {
        GetInventory();
    }

    public void UpdateCurrency()
    {
    }


    public void UpdateInventory()
    {
    }

    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            _currencies.Clear();
            _inventory.Clear();

            foreach (var pair in result.VirtualCurrency)
            {
                _currencies.Add(pair.Key, pair.Value);
            }

            foreach (var item in result.Inventory)
            {
                _inventory.Add(item);
            }
        }, PlayfabErrorHandler.HandleError);
    }


    public void AddCurrency()
    {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
        {
        }, _ => { }, PlayfabErrorHandler.HandleError);
    }

    public void SubtractCurrency()
    {
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest
        {
        }, _ => { }, PlayfabErrorHandler.HandleError);
    }
}