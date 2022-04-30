using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot
{
    public static BattleSys Instance;
    public LoadWnd loadWnd;
    public PlayWnd playWnd;
    private int mapID;
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
        mapID = root.MapID;

        resSvc.AsyncLoadScene("map_" + mapID, SceneLoadProgess, SceneLoadDone);
    }

    private int lastPercent = 0;
    void SceneLoadProgess(float val)
    {
        int percent = (int)(val * 100);
        if(lastPercent != percent)
        {
            NetMsg msg = new NetMsg
            {
                cmd = CMD.SndLoadPrg,
                sndLoadPrg = new SndLoadPrg
                {
                    roomID = root.RoomID,
                    percent = percent
                }
            };
            netSvc.SendMsg(msg);
            lastPercent = percent;
        }
    }

    void SceneLoadDone()
    {
        //��ʼ�� UI
        playWnd.SetWndState();
        // ���ؽ�ɫ
        //��ʼ��ս��


        NetMsg msg = new NetMsg
        {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart
            {
                roomID = root.RoomID
            }
        };

        netSvc.SendMsg(msg);
    }

    public void NtfLoadPrg(NetMsg msg)
    {
        loadWnd.RefreshPrgData(msg.ntfLoadPrg.percentLst);
    }

    public void RspBattleStart(NetMsg msg)
    {
        loadWnd.SetWndState(false);
        audioSvc.PlayBGMusic(NameDefine.BattleBGMusic);

    }
}
