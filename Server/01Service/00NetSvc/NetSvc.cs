using PENet;
using PEUtils;
using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MsgPack
    {
        public ServerSession session;
        public NetMsg msg;
        public MsgPack(ServerSession session, NetMsg msg)
        {
            this.session = session;
            this.msg = msg;
        }
    }

    /// <summary>
    /// 网络服务
    /// </summary>
    public class NetSvc : Singleton<NetSvc>
    {
        private KCPNet<ServerSession, NetMsg> server = new KCPNet<ServerSession, NetMsg>();
        private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();
        public static readonly string pkgque_lock = "pkgque_lock";

        public override void Init()
        {
            base.Init();

            msgPackQue.Clear();

            // 给 KCP 网络库设置日志配置
            KCPTool.LogFunc = this.Log;
            KCPTool.WarnFunc = this.Warn;
            KCPTool.ErrorFunc = this.Error;
            KCPTool.ColorLogFunc = (color, msg) =>
            {
                this.ColorLog((LogColor)color, msg);
            };

            server.StartAsServer(ServerConfig.LocalDevInnerIP, ServerConfig.UdpPort);


            this.Log("NetSvc Init Done");
        }

        public void AddMsgQue(ServerSession session, NetMsg msg)
        {
            lock (pkgque_lock)
            {
                msgPackQue.Enqueue(new MsgPack(session, msg));
            }
        }


        public override void Update()
        {
            base.Update();

            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    MsgPack msg = msgPackQue.Dequeue();
                    HandoutMsg(msg);
                }
            }
        }

        // 消息分发
        private void HandoutMsg(MsgPack pack)
        {
            switch (pack.msg.cmd)
            {
                case CMD.ReqLogin:
                    LoginSys.Instance.ReqLogin(pack);
                    break;
                case CMD.ReqMatch:
                    MatchSys.Instance.ReqMatch(pack);
                    break;
                case CMD.None:
                    break;
            }
        }
    }
}
