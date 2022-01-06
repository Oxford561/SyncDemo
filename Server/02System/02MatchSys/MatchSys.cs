using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 匹配系统
    /// </summary>
    public class MatchSys:SystemRoot<MatchSys>
    {
        public override void Init()
        {
            base.Init();

            this.Log("MatchSys Init Done");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
