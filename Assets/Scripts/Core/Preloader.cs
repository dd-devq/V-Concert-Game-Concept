using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Preloader : MonoBehaviour
{
    public PersistentManagers persistentManagers;
    public AssetReference sceneRef;

    // public async void Start()
    // {
    //     var handleSo = Addressables.LoadAssetAsync<ScriptableObject>("auth");
    //     await handleSo.Task;
    //     
    //
    //     foreach (var manager in persistentManagers.listManagers)
    //     {
    //         var handle = Addressables.LoadAssetAsync<GameObject>(manager);
    //         await handle.Task;
    //
    //
    //         if (handle.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             var managerPrefab = handle.Result;
    //             Instantiate(managerPrefab);
    //         }
    //         else
    //         {
    //             Debug.LogError("Failed to load manager: " + manager);
    //         }
    //
    //         Addressables.Release(handle);
    //     }
    //
    //     var handleScene = Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Additive);
    //
    //     await handleScene.Task;
    //
    //
    //     if (handleScene.Status == AsyncOperationStatus.Succeeded)
    //     {
    //         handleScene.Result.ActivateAsync();
    //     }
    //
    // }
    private async void Start()
    {
        var resourceLocator = await Addressables.InitializeAsync().Task;
        var allKeys = resourceLocator.Keys.ToList();




        foreach (var key in allKeys)
        {
            var keyDownloadSizeKb = await Addressables.GetDownloadSizeAsync(key).Task;
            if (keyDownloadSizeKb <= 0) continue;

            var keyDownloadOperation = Addressables.DownloadDependenciesAsync(key);
            while (!keyDownloadOperation.IsDone)
            {
                await Task.Yield();
            }
        }

        var handleSo = Addressables.LoadAssetAsync<ScriptableObject>("auth");
        await handleSo.Task;

        foreach (var manager in persistentManagers.listManagers)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(manager);
            await handle.Task;


            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var managerPrefab = handle.Result;
                Instantiate(managerPrefab);
            }
            else
            {
                Debug.LogError("Failed to load manager: " + manager);
            }

            Addressables.Release(handle);
        }

        var handleScene = Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Additive);

        await handleScene.Task;


        if (handleScene.Status == AsyncOperationStatus.Succeeded)
        {
            handleScene.Result.ActivateAsync();
        }
    }
}