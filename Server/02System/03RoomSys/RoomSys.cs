using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 房间系统
    /// </summary>
    public class RoomSys:SystemRoot<RoomSys>
    {
        public override void Init()
        {
            base.Init();

            this.Log("RoomSys Init Done");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
