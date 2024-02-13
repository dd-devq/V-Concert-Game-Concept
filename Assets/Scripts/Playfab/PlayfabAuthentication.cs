using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class PlayfabAuthentication : MonoBehaviour
{
    public void Login(Component sender, object data)
    {
        var tmp = (Define.LoginInfo)data;
        tmp.onLoginSuccess();
        tmp.onLoginFail();
    }
}
