using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : PersistentManager<CharacterManager>
{
    public List<AnimationClip> ListAnimations;
    public AnimationClip MainDance;
    public Avatar Avatar;
    private GameObject _characterPrefab;
    private GameObject _character;
    private string _currentCharacterPath;


    public void Dance()
    {
    }

    public void LoadCharacter(Component sender, object data)
    {
        _currentCharacterPath = PlayFabPlayerDataController.Instance.PlayerTitleData["Character Path"].Value;
        _characterPrefab = ResourceManager.LoadPrefabAsset(_currentCharacterPath);
        _character = Instantiate(_characterPrefab);
    }

    public void ChangeCharacter(Component sender, object data)
    {
        var temp = (Dictionary<string, string>)data;
        _currentCharacterPath = temp["Character Path"];
        
        Destroy(_character);
        ResourceManager.UnloadPrefabAsset(_characterPrefab);
        
        _characterPrefab = ResourceManager.LoadPrefabAsset(_currentCharacterPath);
        _character = Instantiate(_characterPrefab);
    }
}