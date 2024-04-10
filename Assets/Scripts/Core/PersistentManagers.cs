using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Persistent Managers")]
public class PersistentManagers : ScriptableObject
{
    public List<AssetReference> listManagers;
}