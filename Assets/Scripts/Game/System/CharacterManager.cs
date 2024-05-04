using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : PersistentManager<CharacterManager>
{
    public List<AnimationClip> ListAnimations;
    public AnimationClip MainDance;

    public string animatorPath;
    private string _currentCharacterPath;

    private GameObject _characterPrefab;
    private GameObject _character;

    private RuntimeAnimatorController _characterAnimator;

    private readonly Vector3 _characterPosition = new(0.15f, 2.5f, 0f);

    private void Start()
    {
        _characterAnimator = ResourceManager.LoadAnimator(animatorPath);
    }

    public void Dance(Component sender, object data)
    {
    }

    public void LoadCharacter(Component sender, object data)
    {
        _currentCharacterPath = PlayFabPlayerDataController.Instance.PlayerTitleData["Character Path"].Value;
        _characterPrefab = ResourceManager.LoadPrefabAsset(_currentCharacterPath);
        _character = Instantiate(_characterPrefab);
        _character.transform.position = _characterPosition;
        Debug.Log(_character.transform.position);
        _character.GetComponent<Animator>().runtimeAnimatorController = _characterAnimator;
    }

    public void ChangeCharacter(Component sender, object data)
    {
        var temp = (Dictionary<string, string>)data;
        _currentCharacterPath = temp["Character Path"];

        Destroy(_character);
        ResourceManager.UnloadPrefabAsset(_characterPrefab);

        _characterPrefab = ResourceManager.LoadPrefabAsset(_currentCharacterPath);
        _character = Instantiate(_characterPrefab);
        _character.transform.position = _characterPosition;
        _character.GetComponent<Animator>().runtimeAnimatorController = _characterAnimator;
    }

    public void SetAnimation(Component sender, object data)
    {
    }
}