using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : PersistentManager<ResourceManager>
{
    public static GameObject LoadPrefabAsset(AssetReference assetReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        handle.WaitForCompletion();

        return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
    }
}