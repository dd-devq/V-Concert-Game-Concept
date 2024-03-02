using TMPro;
using UI;
using UnityEngine;
using EventData;

public class UIAuthentication : BaseUI
{
    [Header("Events")] 
    [SerializeField] private GameEvent onLoginClick;
    [SerializeField] private GameEvent onResetPasswordClick;
    [SerializeField] private GameEvent onRegisterClick;

    [Header("Login")] 
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;

    [Header("Register")] 
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;

    [Header("Reset")] 
    [SerializeField] private GameObject resetPasswordPanel;
    [SerializeField] private TMP_InputField accountUsername;

    public void OnLoginClick()
    {
        var loginInfo = new LoginInfo
        {
            Username = loginUsername.text,
            Password = loginPassword.text,
            LoginFailCallback = OnLoginFail,
            LoginSuccessCallback = OnLoginSuccess
        };
        onLoginClick.Invoke(this, loginInfo);
    }

    public void OnRegisterClick()
    {
        var registerInfo = new RegisterInfo
        {
            Username = registerUsername.text,
            Password = registerPassword.text,
            Email = registerEmail.text,
            RegisterSuccessCallback = OnRegisterSuccess,
            RegisterFailCallback = OnRegisterFail
        };
        onRegisterClick.Invoke(this, registerInfo);
    }

    public void OnResetPasswordClick()
    {
    }

    private void OnLoginFail()
    {
        Debug.Log("Login Failed");
        // UI Manager Raise Warning
    }

    private void OnLoginSuccess()
    {
        Debug.Log("Login Success");
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
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

    public void ResetToLogin()
    {
        if (registerPanel.activeSelf)
        {
            registerPanel.SetActive(false);
            loginPanel.SetActive(true);
            loginPanel.transform.SetAsLastSibling();
        }
    }

    public void LoginToReset()
    {
        if (loginPanel.activeSelf)
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
            registerPanel.transform.SetAsLastSibling();
        }
    }
}