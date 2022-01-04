using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
登录系统
*/
public class LoginSys : MonoBehaviour
{
    public static LoginSys Instance;
    public LoginWnd loginWnd;
    public void InitSys()
    {
        Instance = this;
        this.Log("Init LoginSys done");
    }

    public void EnterLogin()
    {
        loginWnd.SetWndState();
    }
}