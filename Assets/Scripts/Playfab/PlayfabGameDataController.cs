using System.Collections.Generic;
using EventData;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabGameDataController : PersistentManager<PlayfabGameDataController>
{
    private readonly List<CatalogItem> _catalogItems = new();
    public IEnumerable<CatalogItem> CatalogItems => _catalogItems;


    private List<PlayerLeaderboardEntry> _currentLeaderBoard = new();
    public List<PlayerLeaderboardEntry> CurrentLeaderBoard => _currentLeaderBoard;


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

    public void GetLeaderBoard(Component sender, object data)
    {
        if (data is LeaderBoardReqInfo tmp)
        {
            var req = new GetLeaderboardRequest
            {
                MaxResultsCount = 10,
                StartPosition = 0,
                StatisticName = tmp.Name
            };

            PlayFabClientAPI.GetLeaderboard(req, result =>
                {
                    _currentLeaderBoard = result.Leaderboard;
                    tmp.SuccessCallback();
                },
                PlayfabErrorHandler.HandleError
            );
        }
    }

    public static void SendLeaderBoard(string leaderBoardName, int score)
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