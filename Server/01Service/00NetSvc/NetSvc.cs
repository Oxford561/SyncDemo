using PENet;
using PEUtils;
using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 网络服务
    /// </summary>
    public class NetSvc:Singleton<NetSvc>
    {
        private KCPNet<ServerSession,NetMsg> server = new KCPNet<ServerSession, NetMsg> ();

        public override void Init()
        {
            base.Init();

            // 给 KCP 网络库设置日志配置
            KCPTool.LogFunc = this.Log;
            KCPTool.WarnFunc = this.Warn;
            KCPTool.ErrorFunc = this.Error;
            KCPTool.ColorLogFunc = (color, msg) =>
            {
                this.ColorLog((LogColor)color, msg);
            };

            server.StartAsServer(ServerConfig.LocalDevInnerIP,ServerConfig.UdpPort);


            this.Log("NetSvc Init Done");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
