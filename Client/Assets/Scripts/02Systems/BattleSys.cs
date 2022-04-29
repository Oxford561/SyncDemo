using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot
{
    public static BattleSys Instance;
    public LoadWnd loadWnd;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        this.Log("Init BattleSys done");
    }

    public void EnterBattle()
    {
        audioSvc.StopBGMusic();
        loadWnd.SetWndState();

    }
}
