using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabErrorHandler : PersistentManager<PlayfabErrorHandler>
{
    public GameEvent informPlayfabError;


    public void InformPlayfabError(string errorMessage)
    {
        informPlayfabError.Invoke(this, null);
    }

    public static void HandleError(PlayFabError error)
    {
    }
}