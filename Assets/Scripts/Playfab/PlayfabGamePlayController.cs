using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabGamePlayController : PersistentManager<PlayfabGamePlayController>
{
    public void StartPurchaseItem()
    {
    }

    public void PurchaseItemSuccess()
    {
    }

    public void ConsumeItems()
    {
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
        {
        }, _ => { }, PlayfabErrorHandler.HandleError);
    }
}