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
        public override void Init()
        {
            base.Init();

            this.Log("NetSvc Init Done");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
