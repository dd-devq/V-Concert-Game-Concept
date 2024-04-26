using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : PersistentManager<ResourceManager>
{
    private static readonly List<AsyncOperationHandle> ListHandle = new();

    private void Start()
    {
        Application.quitting += ClearHandle;
    }

    public static GameObject LoadPrefabAsset(AssetReference prefabReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(prefabReference);
        handle.WaitForCompletion();
        ListHandle.Add(handle);
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }

    public static void UnloadPrefabAsset(GameObject gameObject)
    {
        Addressables.ReleaseInstance(gameObject);
    }

    public static void UnloadSpriteAsset(Sprite sprite)
    {
        Addressables.Release(sprite);
    }

    public static AudioClip LoadAudioClip(string audioPath)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(audioPath);
        handle.WaitForCompletion();
        ListHandle.Add(handle);
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }

    public static Sprite LoadSprite(string spritePath)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(spritePath);
        handle.WaitForCompletion();
        ListHandle.Add(handle);
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }

    private static void ClearHandle()
    {
        foreach (var handle in ListHandle)
        {
            Addressables.Release(handle);
        }
    }
}