using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    public PersistentManagers persistentManagers;
    public AssetReference sceneRef;

    public async void Start()
    {
        var handleSo = Addressables.LoadAssetAsync<ScriptableObject>("auth");
        await handleSo.Task;

        var handleScene = Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Additive);

        await handleScene.Task;

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

        if (handleScene.Status == AsyncOperationStatus.Succeeded)
        {
            handleScene.Result.ActivateAsync();
        }
    }
}