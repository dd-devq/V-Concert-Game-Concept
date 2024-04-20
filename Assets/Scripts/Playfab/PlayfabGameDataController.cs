using System;
using System.Collections;
using System.Collections.Generic;
using EventData;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabGameDataController : PersistentManager<PlayfabGameDataController>
{
    private readonly List<CatalogItem> _catalogItems = new();
    public IEnumerable<CatalogItem> CatalogItems => _catalogItems;

    public GameEvent onGameDataRetrieve;

    private void GetCatalogItems(string catalogVersion = "1")
    {
        var req = new GetCatalogItemsRequest
        {
            CatalogVersion = catalogVersion
        };

        PlayFabClientAPI.GetCatalogItems(req, result =>
        {
            foreach (var item in result.Catalog)
            {
                _catalogItems.Add(item);
            }
        }, PlayfabErrorHandler.HandleError);
    }

    void GetLeaderBoard(string leaderBoardName)
    {
    }

    void SendLeaderBoard(string leaderBoardName, int score)
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new()
                {
                    StatisticName = leaderBoardName,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(req, null, PlayfabErrorHandler.HandleError);
    }

    public void GetAllData()
    {
        GetCatalogItems();
    }
}