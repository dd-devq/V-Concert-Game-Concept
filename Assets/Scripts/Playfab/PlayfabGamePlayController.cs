using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayFabGamePlayController : PersistentManager<PlayFabGamePlayController>
{
    public void StartPurchaseItem()
    {
    }

    public void PurchaseItemSuccess()
    {
    }

    public void ConsumeItems(Component sender, object data)
    {
        var req = new ConsumeItemRequest
        {
            ConsumeCount = 1,
            // ItemInstanceId = id
        };
        PlayFabClientAPI.ConsumeItem(req, null, PlayFabErrorHandler.HandleError);
    }
}