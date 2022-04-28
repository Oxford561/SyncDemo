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

            checkTaskID = TimerSvc.Instance.AddTask(ServerConfig.SelectCountDown * 1000, ReachTimeLimit);
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

        void CheckSelectState()
        {
            for (int i = 0;i < selectArr.Length;i++)
            {
                if(selectArr[i].selectDone==false)
                {
                    return;
                }
            }

            isAllSelected = true;

        }

        public void UpdateHeroSelect(int posIndex,int heroID)
        {
            selectArr[posIndex].selectID = heroID;
            selectArr[posIndex].selectDone = true;
            CheckSelectState();
            if(isAllSelected)
            {
                // 进入load 状态
                if (TimerSvc.Instance.DeleteTask(checkTaskID))
                {
                    this.ColorLog(PEUtils.LogColor.Green,"RoomID:{0}所有玩家选择英雄完成，进入游戏加载",room.roomID)
                }
                else
                {
                    this.Warn("Romve CheckTaskID Failed.");
                }

                room.SelectArr = selectArr;
                room.ChangeRoomState(RoomStateEnum.Load);
            }
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
