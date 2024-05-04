using UnityEngine;

public class SceneManager : PersistentManager<SceneManager>
{
    public object LoadSceneData;
    public void LoadUIScene(Component sender, object data)
    {
        ResourceManager.Instance.UnloadScene();
        ResourceManager.Instance.LoadScene(0);
    }

    public void LoadPlayScene(Component sender, object data)
    {
        ResourceManager.Instance.UnloadScene();
        ResourceManager.Instance.LoadScene(1);
        LoadSceneData = data;
        if (data is not int)
        {
            Debug.LogError("Invalid song index");
        }
    }
       
}
