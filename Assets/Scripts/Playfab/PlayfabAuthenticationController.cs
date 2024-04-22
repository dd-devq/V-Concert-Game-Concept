using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
using EventData;

public class PlayfabAuthenticationController : PersistentManager<PlayfabAuthenticationController>
{
    private const string PlayfabRememberMeId = "PlayfabRememberMeId";
    private const string PlayfabRememberMe = "PlayfabRememberMe";

    #region Login

    public void Login(Component sender, object data)
    {
        var tmp = (LoginInfo)data;
        if (ValidateEmail(tmp.Username))
        {
            LoginWithEmail(tmp);
        }
        else
        {
            LoginWithUsername(tmp);
        }
    }

    public void AutoLogin(Component sender, object data)
    {
        var tmp = (AutoLoginInfo)data;
        var rememberMeId = PlayerPrefs.GetString(PlayfabRememberMeId);
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = rememberMeId,
            CreateAccount = true
        }, _ =>
        {
            PlayfabGameDataController.Instance.GetAllData();
            PlayfabPlayerDataController.Instance.GetAllData();
            tmp.AutoLoginSuccessCallback();
            PlayfabGameDataController.SendLeaderBoard("001", 2540);
        }, error =>
        {
            PlayfabErrorHandler.HandleError(error);
            Logout(null, null);
        });
    }

    private static void LoginWithEmail(LoginInfo data)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = data.Username,
            Password = data.Password,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, _ =>
            {
                PlayfabGameDataController.Instance.GetAllData();
                PlayfabPlayerDataController.Instance.GetAllData();
                data.LoginSuccessCallback();
                RememberMe();
            },
            PlayfabErrorHandler.HandleError);
    }

    private static void LoginWithUsername(LoginInfo data)
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = data.Username,
            Password = data.Password,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.LoginWithPlayFab(request, _ =>
            {
                PlayfabGameDataController.Instance.GetAllData();
                PlayfabPlayerDataController.Instance.GetAllData();
                data.LoginSuccessCallback();
                RememberMe();
            },
            PlayfabErrorHandler.HandleError);
    }

    #endregion

    #region Register

    public void Register(Component sender, object data)
    {
        var tmp = (RegisterInfo)data;
        var request = new RegisterPlayFabUserRequest
        {
            Email = tmp.Email,
            Username = tmp.Username,
            DisplayName = tmp.Username,
            Password = tmp.Password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, successResult => tmp.RegisterSuccessCallback(),
            failResult => tmp.RegisterFailCallback());
    }

    #endregion

    #region Logout

    public void Logout(Component sender, object data)
    {
        PlayFabClientAPI.ForgetAllCredentials();
        ClearRememberMe();
    }

    #endregion

    #region ResetPassword

    public void ResetPassword(Component sender, object data)
    {
    }

    #endregion

    #region Utils

    private static bool ValidateEmail(string em)
    {
        const string emailPattern =
            @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

        return Regex.IsMatch(em, emailPattern);
    }

    private static void ClearRememberMe()
    {
        PlayerPrefs.DeleteKey(PlayfabRememberMeId);
        PlayerPrefs.DeleteKey(PlayfabRememberMe);
    }

    private static void RememberMe()
    {
        var rememberMeId = Guid.NewGuid().ToString();
        PlayerPrefs.SetString(PlayfabRememberMeId, rememberMeId);
        PlayerPrefs.SetInt(PlayfabRememberMe, 1);

        PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest
        {
            CustomId = rememberMeId,
            ForceLink = false
        }, null, null);
    }

    #endregion
}