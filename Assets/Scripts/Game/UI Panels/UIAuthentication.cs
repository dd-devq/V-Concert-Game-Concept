using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIAuthentication : BaseUI
{
    public GameEvent onLoginClick;
    public GameEvent onForgotPasswordClick;
    public GameEvent onRegisterClick;

    private bool _staySignedIn;

    public void onLogin()
    {
        var loginInfo = new Define.LoginInfo
        {
            username = "test",
            password = "123456"
        };
        onLoginClick.Invoke(this, loginInfo);   
    }
}