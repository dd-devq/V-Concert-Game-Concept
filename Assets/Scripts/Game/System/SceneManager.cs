using UnityEngine;

public class SceneManager : PersistentManager<SceneManager>
{
    public void LoadUIScene(Component sender, object data)
    {
        ResourceManager.Instance.UnloadScene();
        ResourceManager.Instance.LoadScene(0);
    }

    public void LoadPlayScene(Component sender, object data)
    {
        Debug.Log("Hello Scene");
        ResourceManager.Instance.UnloadScene();
        ResourceManager.Instance.LoadScene(1);
    }
       
}
