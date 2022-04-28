using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySys : SysRoot
{
    public static LobbySys Instance;
    public LobbyWnd lobbyWnd;
    public MatchWnd matchWnd;
    public SelectWnd selectWnd;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        this.Log("Init LobbySys done");
    }

    public void EnterLobby()
    {
        lobbyWnd.SetWndState();
    }

    public void RspMatch(NetMsg msg)
    {
        int predictTime = msg.rspMatch.predictTime;
        lobbyWnd.ShowMatchInfo(true, predictTime);
    }

    public void NtfConfirm(NetMsg msg)
    {
        NtfConfirm ntf = msg.ntfConfirm;
        if (ntf.dissmiss)
        {
            matchWnd.SetWndState(false);
            lobbyWnd.SetWndState();
        }
        else
        {
            root.RoomID = ntf.roomID;
            lobbyWnd.SetWndState(false);
            if(matchWnd.gameObject.activeSelf == false)
            {
                matchWnd.SetWndState();
            }
            matchWnd.RefreshUI(ntf.confirmArr);
        }
    }

    public void NtfSelect(NetMsg msg)
    {
        matchWnd.SetWndState(false);
        selectWnd.SetWndState();
    }
}
