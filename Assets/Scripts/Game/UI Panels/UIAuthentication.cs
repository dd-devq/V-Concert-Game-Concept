using System;
using TMPro;
using UnityEngine;

public class UIAuthentication : BaseUI
{
    [Header("Events")] [SerializeField] private GameEvent onLoginClick;
    [SerializeField] private GameEvent onForgotPasswordClick;
    [SerializeField] private GameEvent onRegisterClick;

    [Header("Login")] [SerializeField] private GameObject loginPanel;
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;

    [Header("Register")] [SerializeField] private GameObject registerPanel;
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerEmail;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void OnLoginClick()
    {
        var loginInfo = new Define.LoginInfo
        {
            username = loginUsername.text,
            password = loginPassword.text,
            onLoginFail = OnLoginFail,
            onLoginSuccess = OnLoginSuccess
        };
        onLoginClick.Invoke(this, loginInfo);
    }

    public void OnRegisterClick()
    {
        var registerInfo = new Define.RegisterInfo()
        {
            username = registerUsername.text,
            password = registerPassword.text,
            email = registerEmail.text,
            onRegisterSuccess = OnRegisterSuccess,
            onRegisterFail = OnRegisterFail
        };
        onRegisterClick.Invoke(this, registerInfo);
    }

    public void OnForgotPasswordClick()
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

    private void OnRegisterFail()
    {
        Debug.Log("Register Failed");
    }

    private void OnRegisterSuccess()
    {
        Debug.Log("Register Success");
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
}