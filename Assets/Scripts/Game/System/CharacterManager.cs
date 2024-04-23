using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterManager : PersistentManager<CharacterManager>
{
    public List<AnimationClip> ListAnimations;
    public AnimationClip MainDance;
    public Avatar Avatar;
    public AssetReference refChar;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var character = ResourceManager.LoadPrefabAsset(refChar);

            Instantiate(character);
        }
    }

    public void Dance()
    {
    }
}