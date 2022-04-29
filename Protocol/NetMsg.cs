using PENet;
using System;
using System.Collections.Generic;

namespace Protocol
{
    /// <summary>
    /// 网络通讯协议
    /// </summary>
    [Serializable]
    public class NetMsg : KCPMsg
    {
        public CMD cmd;
        public ErrorCode error;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqMatch reqMatch;
        public RspMatch rspMatch;
        public NtfConfirm ntfConfirm;
        public SndConfirm sndConfirm;
        //public NtfSelect ntfSelect;
        public SndSelect sndSelect;
        public NtfLoadRes ntfLoadRes;
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
        None = 0,
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

    [Serializable]
    public class NtfConfirm
    {
        public uint roomID;
        public bool dissmiss;//解散
        public ConfirmData[] confirmArr;
    }

    [Serializable]
    public class ConfirmData
    {
        public int iconIndex;
        public bool confirmDone;
    }

    [Serializable]
    public class SndConfirm
    {
        public uint roomID;
    }

    #endregion

    #region 选择加载

    //[Serializable]
    //public class NtfSelect
    //{

    //}

    [Serializable]
    public class SelectData
    {
        public int selectID;
        public bool selectDone;
    }

    [Serializable]
    public class SndSelect
    {
        public uint roomID;
        public int heroID;
    }

    [Serializable]
    public class NtfLoadRes
    {
        public int mapID;
        public List<BattleHeroData> heroList;
        public int posIndex;
    }

    [Serializable]
    public class BattleHeroData
    {
        public string userName;//玩家名称
        public int heroID;
        // 级别，皮肤，边框，称号

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
        ReqLogin = 1,
        RspLogin = 2,

        // 匹配
        ReqMatch = 3,
        RspMatch = 4,

        //确认
        NtfConfirm = 5,
        SndConfirm = 6,

        // 选择
        NtfSelect = 7,
        SndSelect = 8,

        // 加载
        NtfLoadRes = 9,
    }
}
