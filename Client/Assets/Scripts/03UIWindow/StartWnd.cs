using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面
/// </summary>
public class StartWnd : WindowRoot
{
    public Text txtName;
    private UserData userData = null;
    protected override void InitWnd()
    {
        base.InitWnd();
        userData = root.UserData;
        txtName.text = userData.name;
    }

    public void ClickStartBtn()
    {
        audioSvc.PlayUIAudio("com_click1");
        LoginSys.Instance.EnterLobby();
    }
}
