using System;
using UnityEngine;

public class UIAuthentication : BaseUI
{
    public GameEvent onLoginClick;
    public GameEvent onForgotPasswordClick;
    public GameEvent onRegisterClick;

    public GameObject loginPanel;
    public GameObject registerPanel;


    public void OnLogin()
    {
        var loginInfo = new Define.LoginInfo
        {
            username = "test",
            password = "123456",
            onLoginFail = OnLoginFail,
            onLoginSuccess = OnLoginSuccess
        };
        onLoginClick.Invoke(this, loginInfo);
    }

    public void OnRegisterClick()
    {
    }

    private void OnLoginFail()
    {
        Debug.Log("Login Failed");
    }

    private void OnLoginSuccess()
    {
        Debug.Log("Login Success");
    }

    public void RegisterToLogin()
    {
        if (registerPanel.activeSelf)
        {
            registerPanel.SetActive(false);
            loginPanel.SetActive(true);
            loginPanel.transform.SetAsLastSibling();
        }
    }

    public void LoginToRegister()
    {
        if (loginPanel.activeSelf)
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
            registerPanel.transform.SetAsLastSibling();
        }
    }

    public void OnForgotPasswordClick()
    {
    }
}