using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 对战房间
    /// </summary>
    public class PVPRoom
    {
        public uint roomID;
        public PVPEnum pvpEnum = PVPEnum.None;
        public ServerSession[] sessionArr;

        private Dictionary<RoomStateEnum, RoomStateBase> fsm = new Dictionary<RoomStateEnum, RoomStateBase>();
        private RoomStateEnum currentRoomStateEnum = RoomStateEnum.None;

        public PVPRoom(uint roomID,PVPEnum pvpEnum,ServerSession[] sessionArr)
        {
            this.roomID = roomID;
            this.pvpEnum = pvpEnum;
            this.sessionArr = sessionArr;

            fsm.Add(RoomStateEnum.Confirm, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.Select, new RoomStateSelect(this));
            fsm.Add(RoomStateEnum.Load, new RoomStateLoad(this));
            fsm.Add(RoomStateEnum.Fight, new RoomStateFight(this));
            fsm.Add(RoomStateEnum.End, new RoomStateEnd(this));

            ChangeRoomState(RoomStateEnum.Confirm);
        }

        public void ChangeRoomState(RoomStateEnum targetState)
        {
            if (currentRoomStateEnum == targetState) return;
            if(fsm.ContainsKey(targetState))
            {
                if(currentRoomStateEnum!= RoomStateEnum.None)
                {
                    fsm[currentRoomStateEnum].Exit();
                }
                fsm[targetState].Enter();
                currentRoomStateEnum = targetState;
            }
        }
    }
}
