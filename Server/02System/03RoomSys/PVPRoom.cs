using PENet;
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

        public void BroadcastMsg(NetMsg msg)
        {
            // 性能优化，数据相同，只序列化一次
            byte[] bytes = KCPTool.Serialize(msg);
            if(bytes != null)
            {
                for (int i = 0; i < sessionArr.Length; i++)
                {
                    //sessionArr[i].SendMsg(msg);
                    sessionArr[i].SendMsg(bytes);
                }
            }
        }

        int GetPosIndex(ServerSession session)
        {
            int posIndex = 0;
            for (int i = 0; i < sessionArr.Length; i++)
            {
                if(sessionArr[i]==session)
                {
                    posIndex = i;
                }
            }
            return posIndex;
        }

        public void SndConfirm(ServerSession session)
        {
            if(currentRoomStateEnum == RoomStateEnum.Confirm)
            {
                if(fsm[currentRoomStateEnum] is RoomStateConfirm state)
                {
                    state.UpdateConfirmState(GetPosIndex(session));
                }
            }
        }
    }
}
