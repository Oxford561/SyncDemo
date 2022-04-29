using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 加载
    /// </summary>
    public class RoomStateLoad:RoomStateBase
    {
        public RoomStateLoad(PVPRoom room) : base(room)
        {

        }

        public override void Enter()
        {
            int len = room.sessionArr.Length;

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

            for (int i = 0;i < len;i++)
            {
                msg.ntfLoadRes.posIndex = i;
                room.sessionArr[i].SendMsg(msg);
            }
        }

        public override void Exit()
        {
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
