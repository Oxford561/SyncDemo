using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public class CacheSvc : Singleton<CacheSvc>
    {
        private Dictionary<string, ServerSession> onLineAcctDic;
        private Dictionary<ServerSession, UserData> onLineSessionDic;
        public override void Init()
        {
            base.Init();
            onLineAcctDic = new Dictionary<string, ServerSession>();
            onLineSessionDic = new Dictionary<ServerSession, UserData>();

            this.Log("CacheSvc Init Done");
        }

        public override void Update()
        {
            base.Update();
        }

        public bool IsAcctOnLine(string acct)
        {
            return onLineAcctDic.ContainsKey(acct);
        }

        public void AcctOnLine(string acct,ServerSession session,UserData playerData)
        {
            onLineAcctDic.Add(acct, session);
            onLineSessionDic.Add(session, playerData);
        }
    }
}
