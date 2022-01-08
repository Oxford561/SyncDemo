using PENet;
using System;
using System.Collections.Generic;

namespace Protocol
{
    /// <summary>
    /// 网络通讯协议
    /// </summary>
    public class NetMsg:KCPMsg
    {
        public CMD cmd;
        public ErrorCode error;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
    }

    [Serializable]
    public class ReqLogin
    {
        public string acct;
        public string pass;
    }

    [Serializable]
    public class RspLogin
    {
        public UserData userData;
    }

    [Serializable]
    public class UserData
    {
        public int id;
        public string name;
        public int lv;
        public int exp;
        public int diamond;
        public int ticket;
        public List<HeroSelectData> heroSelectData;
    }

    [Serializable]
    public class HeroSelectData
    {
        public int heroID;
    }

    // 错误码
    public enum ErrorCode
    {
        None,
        AcctIsOnline,
    }

    // 通信协议命令号
    public enum CMD
    {
        None = 0,
        // 登录
        ReqLogin= 1,
        RspLogin= 2,
    }
}
