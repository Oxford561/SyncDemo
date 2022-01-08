using PENet;
using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// Session 连接
    /// </summary>
    public class ServerSession : KCPSession<NetMsg>
    {
        protected override void OnConnected()
        {
        }

        protected override void OnDisConnected()
        {
        }

        protected override void OnReciveMsg(NetMsg msg)
        {
            NetSvc.Instance.AddMsgQue(this,msg);
        }

        protected override void OnUpdate(DateTime now)
        {
        }
    }
}
