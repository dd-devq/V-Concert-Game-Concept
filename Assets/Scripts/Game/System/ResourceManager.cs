using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : PersistentManager<ResourceManager>
{
    public static GameObject LoadPrefabAsset(AssetReference prefabReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(prefabReference);
        handle.WaitForCompletion();

        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }

    public static AudioClip LoadAudioClip(string audioPath)
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>(audioPath);
        handle.WaitForCompletion();
        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }
}