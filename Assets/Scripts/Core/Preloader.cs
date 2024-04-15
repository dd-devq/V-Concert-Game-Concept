using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Preloader : MonoBehaviour
{
    public PersistentManagers persistentManagers;
    public AssetReference sceneRef;

    public async void Start()
    {
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