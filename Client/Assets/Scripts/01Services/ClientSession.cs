using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PENet;
using Protocol;
using System;

/// <summary>
/// �ͻ����������ӻỰ
/// </summary>
public class ClientSession : KCPSession<NetMsg>
{
    protected override void OnConnected()
    {
    }

    protected override void OnDisConnected()
    {
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        NetSvc.Instance.AddMsgQue(msg);
    }

    protected override void OnUpdate(DateTime now)
    {
    }
}
