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

    public void OnLogin()
    {
        var loginInfo = new Define.LoginInfo
        {
            username = "test",
            password = "123456"
        };
        onLoginClick.Invoke(this, loginInfo);   
    }

    public void OnRegisterClick()
    {
        
    }

    public void OnForgotPasswordClick()
    {
        
    }
}