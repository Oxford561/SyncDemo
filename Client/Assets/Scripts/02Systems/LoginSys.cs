using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
登录系统
*/
public class LoginSys : SysRoot
{
    public static LoginSys Instance;
    public LoginWnd loginWnd;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        this.Log("Init LoginSys done");
    }

    public void EnterLogin()
    {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(NameDefine.MainCityBGMusic);
    }
}
