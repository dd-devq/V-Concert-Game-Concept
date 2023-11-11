using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public void LoadScene(Component sender, object data)
    {
        if (data is SceneIndex)
        {
            SceneManager.LoadScene(data.ToString());
        }
    }
}

public enum SceneIndex
{
    Gameplay,
    Networking,
}