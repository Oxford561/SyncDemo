using PENet;
using System;
using System.Collections.Generic;

namespace Protocol
{
    /// <summary>
    /// 网络通讯协议
    /// </summary>
    [Serializable]
    public class NetMsg:KCPMsg
    {
        public CMD cmd;
        public ErrorCode error;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqMatch reqMatch;
        public RspMatch rspMatch;
    }

    #region 登录相关

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
        public uint id;
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


    #endregion

    #region 匹配相关

    [Serializable]
    public enum PVPEnum
    {
        None=0,
        _1v1 = 1,
        _2v2 = 2,
        _5v5 = 3,
    }

    [Serializable]
    public class ReqMatch
    {
        public PVPEnum pvPEnum;
    }

    [Serializable]
    public class RspMatch
    {
        public int predictTime;
    }

    #endregion

    // 错误码
    [Serializable]
    public enum ErrorCode
    {
        None,
        AcctIsOnline,
    }

    // 通信协议命令号
    [Serializable]
    public enum CMD
    {
        None = 0,
        // 登录
        ReqLogin= 1,
        RspLogin= 2,

        // 匹配
        ReqMatch = 3,
        RspMatch = 4,
    }
}
