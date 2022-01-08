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
            this.ColorLog(PEUtils.LogColor.Green,"Client Online sid="+m_sid);
        }

        protected override void OnDisConnected()
        {
            this.Warn("Client OffLine sid=" + m_sid);
        }

        protected override void OnReciveMsg(NetMsg msg)
        {
            this.Log("RcvPack CMD:{0}", msg.cmd.ToString());
            NetSvc.Instance.AddMsgQue(this,msg);
        }

        protected override void OnUpdate(DateTime now)
        {
        }
    }
}
