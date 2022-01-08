using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 匹配系统
    /// </summary>
    public class MatchSys:SystemRoot<MatchSys>
    {
        private Queue<ServerSession> que1V1 = null;
        private Queue<ServerSession> que2V2 = null;
        private Queue<ServerSession> que5V5 = null;

        public override void Init()
        {
            base.Init();

            que1V1 = new Queue<ServerSession>();
            que2V2 = new Queue<ServerSession>();
            que5V5 = new Queue<ServerSession>();

            this.Log("MatchSys Init Done");
        }

        public override void Update()
        {
            base.Update();
        }

        public void ReqMatch(MsgPack pack)
        {
            ReqMatch data = pack.msg.reqMatch;
            PVPEnum pvpEnum = data.pvPEnum;
            switch (pvpEnum)
            {
                case PVPEnum.None:
                    break;
                case PVPEnum._1v1:
                    que1V1.Enqueue(pack.session);
                    break;
                case PVPEnum._2v2:
                    que2V2.Enqueue(pack.session);
                    break;
                case PVPEnum._5v5:
                    que5V5.Enqueue(pack.session);
                    break;
                default:
                    this.Error("PVPType Error:"+pvpEnum.ToString());
                    break;
            }

            NetMsg msg = new NetMsg
            {
                cmd = CMD.RspMatch,
                rspMatch = new RspMatch
                {
                    predictTime = GetPredictTime(pvpEnum),
                }
            };
            pack.session.SendMsg(msg);
        }

        private int GetPredictTime(PVPEnum pvpEnum)
        {
            int waitCount;
            switch (pvpEnum)
            {
                case PVPEnum._1v1:
                    waitCount = 2 - que1V1.Count;
                    if(waitCount<0)
                    {
                        waitCount = 0;
                    }

                    return waitCount * 10 + 5;
                case PVPEnum._2v2:
                    waitCount = 4 - que2V2.Count;
                    if (waitCount < 0)
                    {
                        waitCount = 0;
                    }

                    return waitCount * 10 + 5;
                case PVPEnum._5v5:
                    waitCount = 10 - que5V5.Count;
                    if (waitCount < 0)
                    {
                        waitCount = 0;
                    }

                    return waitCount * 10 + 5;
                default:
                    this.Error("PVPType Error:" + pvpEnum.ToString());
                    return 0;
            }
        }
    }
}
