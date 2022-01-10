using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 等待确认
    /// </summary>
    public class RoomStateConfirm : RoomStateBase
    {
        private ConfirmData[] confirmArr = null;
        private int checkTaskID = -1;

        public RoomStateConfirm(PVPRoom room):base(room)
        {

        }

        public override void Enter()
        {
            int len = room.sessionArr.Length;
            confirmArr = new ConfirmData[len];
            for (int i = 0; i < len; i++)
            {
                confirmArr[i] = new ConfirmData
                {
                    iconIndex = i,
                    confirmDone = false
                };
            }

            NetMsg msg = new NetMsg
            {
                cmd = CMD.NtfConfirm,
                ntfConfirm = new NtfConfirm
                {
                    roomID = room.roomID,
                    dissmiss = false,
                    confirmArr = confirmArr
                }
            };

            room.BroadcastMsg(msg);

            checkTaskID = TimerSvc.Instance.AddTask(ServerConfig.ConfirmCountDown * 1000, ReachTimeLimit);
        }

        void ReachTimeLimit(int tid)
        {

        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
