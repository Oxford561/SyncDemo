using Protocol;
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
        private List<PVPRoom> pvpRoomLst = null;
        private Dictionary<uint, PVPRoom> pvpRoomDic = null;

        public override void Init()
        {
            base.Init();

            pvpRoomLst = new List<PVPRoom>();
            pvpRoomDic = new Dictionary<uint, PVPRoom>();

            this.Log("RoomSys Init Done");
        }

        public void AddPVPRoom(ServerSession [] sessionArr,PVPEnum pvpEnum)
        {
            uint roomID = GetUniqueRoomID();
            PVPRoom room = new PVPRoom(roomID, pvpEnum,sessionArr);
            pvpRoomLst.Add(room);
            pvpRoomDic.Add(roomID, room);
        }

        public override void Update()
        {
            base.Update();
        }

        uint roomID = 0;
        public uint GetUniqueRoomID()
        {
            roomID += 1;
            return roomID;
        }
    }
}
