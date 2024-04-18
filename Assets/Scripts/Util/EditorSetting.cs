using UnityEngine;

public class EditorSetting : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#endif
    }
}