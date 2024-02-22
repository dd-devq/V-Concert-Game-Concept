using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAuthenticationController : MonoBehaviour
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
            Email = tmp.Email,
            DisplayName = tmp.Username,
            Password = tmp.Password,
            RequireBothUsernameAndEmail = false
        }, succesResult => tmp.RegisterSuccessCallback(), RegisterFail);
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