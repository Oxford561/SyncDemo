using Protocol;
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
    public StartWnd startWnd;
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

    public void RspLogin(NetMsg msg)
    {
        root.ShowTips("登录成功");
        root.UserData = msg.rspLogin.userData;
        startWnd.SetWndState();
        loginWnd.SetWndState(false);
    }

    public void EnterLobby()
    {
        startWnd.SetWndState(false);

    }
}
