using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabErrorHandler : PersistentManager<PlayfabErrorHandler>
{
    public GameEvent playfabError;
    
    public void PlayfabError(string errorMessage)
    {
        playfabError.Invoke(this, null);
    }

    public static void HandleError(PlayFabError error)
    {
    }
}