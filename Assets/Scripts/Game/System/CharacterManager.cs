using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterManager : PersistentManager<CharacterManager>
{
    public List<AnimationClip> ListAnimations;
    public AnimationClip MainDance;
    public Avatar Avatar;
    public AssetReference refChar;
    private GameObject _characterPrefab;
    private GameObject _character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _characterPrefab = ResourceManager.LoadPrefabAsset(refChar);
            _character = Instantiate(_characterPrefab);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(_character);
            ResourceManager.UnloadPrefabAsset(_characterPrefab);
        }
    }

    public void Dance()
    {
    }
}