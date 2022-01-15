using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 选择
    /// </summary>
    public class RoomStateSelect : RoomStateBase
    {
        private SelectData[] selectArr = null;
        private int checkTaskID = -1;
        private bool isAllSelected = false;
        public RoomStateSelect(PVPRoom room) : base(room)
        {

        }

        public override void Enter()
        {
            int len = room.sessionArr.Length;
            selectArr = new SelectData[len];
            for (int i = 0; i < len; i++)
            {
                selectArr[i] = new SelectData
                {
                    selectID = 0,
                    selectDone = false,
                };
            }

            NetMsg msg = new NetMsg
            {
                cmd = CMD.NtfSelect,
            };

            room.BroadcastMsg(msg);

            checkTaskID = TimerSvc.Instance.AddTask(ServerConfig.SelectCountUp * 1000, ReachTimeLimit);
        }

        void ReachTimeLimit(int tid)
        {
            //if (isAllSelected)
            //{
            //    return;
            //}

            //this.ColorLog(PEUtils.LogColor.Yellow, "RoomID:{0} 确认超时，解散房间，重新匹配", room.roomID);
            //NetMsg msg = new NetMsg
            //{
            //    cmd = CMD.SndSelect,
                
            //};

            //room.BroadcastMsg(msg);
            //room.ChangeRoomState(RoomStateEnum.End);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
