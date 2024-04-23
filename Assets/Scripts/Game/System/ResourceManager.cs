using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : PersistentManager<ResourceManager>
{
    private static readonly List<AsyncOperationHandle> _listHandle = new();

    private void Start()
    {
        Application.quitting += () =>
        {
            foreach (var handle in _listHandle)
            {
                Addressables.Release(handle);
            }
        };
    }

    public static GameObject LoadPrefabAsset(AssetReference prefabReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(prefabReference);
        handle.WaitForCompletion();
        _listHandle.Add(handle);
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }


    public static AudioClip LoadAudioClip(string audioPath)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(audioPath);
        handle.WaitForCompletion();
        _listHandle.Add(handle);
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }
}