using System.Collections;
using System.Collections.Generic;
using EventData;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabGameDataController : MonoBehaviour
{
    public GameEvent onGameDataUpdated;

    private void OnPlayerDataUpdated(List<ShopItem> listItem)
    {
        onGameDataUpdated.Invoke(this, new GameData
        {
            ListShopItems = listItem
        });
    }

    void GetSongs()
    {
    }

    private void GetCatalogItems(string catalogVersion = "1")
    {
        var req = new GetCatalogItemsRequest
        {
            CatalogVersion = catalogVersion
        };
        PlayFabClientAPI.GetCatalogItems(req, result =>
        {
            var listItems = new List<ShopItem>();
            foreach (var item in result.Catalog)
            {
                var tmp = new ShopItem
                {
                    name = item.DisplayName
                };
                listItems.Add(tmp);
                Debug.Log(item.DisplayName);
            }
            OnPlayerDataUpdated(listItems);
        }, Debug.Log);
    }

    void GetLeaderBoard()
    {
    }

    public void GetAllData()
    {
        GetCatalogItems();
    }
}