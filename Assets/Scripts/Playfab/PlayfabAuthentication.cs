using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAuthentication : MonoBehaviour
{
    public void Login(Component sender, object data)
    {
        var tmp = (Define.LoginInfo)data;
    }

    public void Register(Component sender, object data)
    {
        var tmp = (Define.RegisterInfo)data;
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            Email = tmp.email,
            DisplayName = tmp.username,
            Password = tmp.password,
            RequireBothUsernameAndEmail = false
        }, succesResult => tmp.onRegisterSuccess(), RegisterFail);
    }

    public void ResetPassword(Component sender, object data)
    {
    }

    private void RegisterFail(PlayFabError error)
    {
        Debug.Log("[PLAYFAB ERROR]: " + error);
    }

    private void LoginFail(PlayFabError error)
    {
    }
}