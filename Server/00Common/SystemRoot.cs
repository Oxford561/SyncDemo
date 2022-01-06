using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public abstract class SystemRoot<T>:Singleton<T> where T : new()
    {
        protected NetSvc netSvc = null;
        protected CacheSvc cacheSvc = null;
        protected TimerSvc timerSvc = null;

        public override void Init()
        {
            base.Init();
            netSvc = NetSvc.Instance;
            cacheSvc = CacheSvc.Instance;
            timerSvc = TimerSvc.Instance;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
