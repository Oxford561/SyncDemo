using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 定时服务
    /// </summary>
    public class TimerSvc : Singleton<TimerSvc>
    {
        public override void Init()
        {
            base.Init();

            this.Log("TimerSvc Init Done");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
