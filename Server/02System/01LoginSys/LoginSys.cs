using Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class LoginSys:SystemRoot<LoginSys>
    {
        public override void Init()
        {
            base.Init();

            this.Log("LoginSys Init Done");
        }

        public override void Update()
        {
            base.Update();
        }

        public void ReqLogin(MsgPack pack)
        {
            ReqLogin data = pack.msg.reqLogin;

            NetMsg msg = new NetMsg 
            {
                cmd = CMD.RspLogin,
            };

            if(cacheSvc.IsAcctOnLine(data.acct))
            {
                // 已上线，返回错误信息
                msg.error = ErrorCode.AcctIsOnline;
            }
            else
            {
                // 未上线，没有缓存，创建默认账号数据，缓存
                uint sid = pack.session.GetSessionID();
                UserData ud = new UserData
                {
                    id = sid,
                    name = "Jack_"+sid,
                    lv = 17,
                    exp = 1086,
                    diamond = 44,
                    ticket = 0,
                    heroSelectData = new List<HeroSelectData>
                    {
                        new HeroSelectData
                        {
                            heroID = 101,
                        },
                        new HeroSelectData
                        {
                            heroID = 102,
                        }
                    }
                };

                msg.rspLogin = new RspLogin
                {
                    userData = ud
                };

                cacheSvc.AcctOnLine(data.acct, pack.session, ud);
            }
            pack.session.SendMsg(msg);
        }
    }
}
