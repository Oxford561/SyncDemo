using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IRoomState
    {
        void Enter();
        void Update();
        void Exit();
    }

    /// <summary>
    /// 房间状态基类
    /// </summary>
    public abstract class RoomStateBase : IRoomState
    {
        public PVPRoom room;
        public RoomStateBase(PVPRoom room)
        {
            this.room = room;
        }
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }

    public enum RoomStateEnum
    {
        None = 0,
        Confirm,//确认
        Select,//选择
        Load,//加载
        Fight,//战斗
        End,//结算完成
    }
}
