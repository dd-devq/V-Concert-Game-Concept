using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
using EventData;
public class PlayfabAuthenticationController : MonoBehaviour
{
    private const string EmailPattern =
        @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

    public void Login(Component sender, object data)
    {
        var tmp = (LoginInfo)data;
        if (ValidateEmail(tmp.Username))
        {
            Debug.Log("Email Login");
            LoginWithEmail(tmp);
        }
        else
        {
            LoginWithUsername(tmp);
        }
    }

    private void LoginWithEmail(LoginInfo data)
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email = data.Username,
            Password = data.Password,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, result => data.LoginSuccessCallback(),
            error => data.LoginFailCallback());
    }

    private void LoginWithUsername(LoginInfo data)
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = data.Username,
            Password = data.Password,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.LoginWithPlayFab(request, result => data.LoginSuccessCallback(),
            error => data.LoginFailCallback());
    }

    public void Logout()
    {
    }

    public void Register(Component sender, object data)
    {
        var tmp = (RegisterInfo)data;
        var request = new RegisterPlayFabUserRequest
        {
            Email = tmp.Email,
            Username = tmp.Username,
            Password = tmp.Password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, successResult => tmp.RegisterSuccessCallback(),
            failResult => tmp.RegisterFailCallback());
    }


    public void ResetPassword(Component sender, object data)
    {
    }

    private static bool ValidateEmail(string em)
    {
        return Regex.IsMatch(em, EmailPattern);
    }
}