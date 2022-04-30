using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 加载
    /// </summary>
    public class RoomStateLoad : RoomStateBase
    {
        private int[] percentArr;
        private bool[] loadArr;

        public RoomStateLoad(PVPRoom room) : base(room)
        {

        }

        public override void Enter()
        {
            int len = room.sessionArr.Length;
            percentArr = new int[len];
            loadArr = new bool[len];

            NetMsg msg = new NetMsg
            {
                cmd = CMD.NtfLoadRes,
                ntfLoadRes = new NtfLoadRes
                {
                    mapID = 101,//默认地图
                    heroList = new List<BattleHeroData>(),
                }
            };

            for (int i = 0; i < room.SelectArr.Length; i++)
            {
                SelectData sd = room.SelectArr[i];
                BattleHeroData hero = new BattleHeroData
                {
                    heroID = sd.selectID,
                    userName = GetUserName(i)
                };
                msg.ntfLoadRes.heroList.Add(hero);
            }

            for (int i = 0; i < len; i++)
            {
                msg.ntfLoadRes.posIndex = i;
                room.sessionArr[i].SendMsg(msg);
            }
        }

        public void UpdateLoadState(int posIndex, int percent)
        {
            percentArr[posIndex] = percent;
            NetMsg msg = new NetMsg
            {
                cmd = CMD.NtfLoadPrg,
                ntfLoadPrg = new NtfLoadPrg
                {
                    percentLst = new List<int>()
                }
            };

            for (int i = 0; i < percentArr.Length; i++)
            {
                msg.ntfLoadPrg.percentLst.Add(percentArr[i]);
            }
            room.BroadcastMsg(msg);
        }

        public void UpdateLoadDone(int posIndex)
        {
            loadArr[posIndex] = true;
            for (int i = 0; i < loadArr.Length; i++)
            {
                if(loadArr[i] == false)
                {
                    return;
                }
            }

            NetMsg msg = new NetMsg
            {
                cmd = CMD.RspBattleStart
            };

            room.BroadcastMsg(msg);
            room.ChangeRoomState(RoomStateEnum.Fight);
            this.ColorLog(PEUtils.LogColor.Green, "RoomID:{0},所有玩家进入战斗", room.roomID);
        }

        public override void Exit()
        {
            percentArr = null;
            loadArr = null;
        }

        public override void Update()
        {
        }
        string GetUserName(int posIndex)
        {
            UserData userData = CacheSvc.Instance.GetUserDataBySession(room.sessionArr[posIndex]);
            if (userData != null)
            {
                return userData.name;
            }
            return "";
        }
    }
}
