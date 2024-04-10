using System.Collections.Generic;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;
using PlayFab;

public class PlayfabPlayerDataController : PersistentManager<PlayfabPlayerDataController>
{
    public GameEvent onPlayerDataUpdated;

    public void OnPlayerDataUpdated()
    {
        onPlayerDataUpdated.Invoke(this, null);
    }
    
    public void GetAllData()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            foreach (var pair in result.VirtualCurrency)
            {
                Debug.Log(pair.Key);
                Debug.Log(pair.Value);
            }
        }, error => Debug.LogError(error.GenerateErrorReport()));
        
    }

    public void InitAllData()
    {
    }

    public void UpdateCurrency()
    {
    }

    public void GetCurrency()
    {
    }

    public void UpdateInventory()
    {
    }

    public void GetInventory()
    {
    }

    private void GetSongList()
    {
    }

    private void UpdateSongList()
    {
    }
}