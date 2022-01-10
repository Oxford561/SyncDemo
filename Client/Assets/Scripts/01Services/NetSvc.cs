using PENet;
using PEUtils;
using Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/**
网络服务
*/
public class NetSvc : MonoBehaviour
{
    public static NetSvc Instance;
    private KCPNet<ClientSession, NetMsg> client = null;
    private Queue<NetMsg> msgPackQue = null;
    public static readonly string pkgque_lock = "pkgque_lock";
    private Task<bool> checkTask = null;
    public void InitSvc()
    {
        Instance = this;
        client = new KCPNet<ClientSession, NetMsg>();
        msgPackQue = new Queue<NetMsg>();

        // 给 KCP 网络库设置日志配置
        KCPTool.LogFunc = this.Log;
        KCPTool.WarnFunc = this.Warn;
        KCPTool.ErrorFunc = this.Error;
        KCPTool.ColorLogFunc = (color, msg) =>
        {
            this.ColorLog((LogColor)color, msg);
        };

        client.StartAsClient(ServerConfig.LocalDevInnerIP, ServerConfig.UdpPort);
        checkTask = client.ConnectServer(100);

        this.Log("Init NetSvc done");
    }

    public void AddMsgQue(NetMsg msg)
    {
        lock (pkgque_lock)
        {
            msgPackQue.Enqueue(msg);
        }
    }

    private int counter = 0;
    public void Update()
    {
        if(checkTask != null && checkTask.IsCompleted)
        {
            if(checkTask.Result)
            {
                //GameRoot.Instance.ShowTips("连接服务器成功");
                this.ColorLog(LogColor.Green,"Connect Server Success");
                checkTask = null;
                // todo 发送 ping

            }
            else
            {
                ++counter;
                if(counter > 4)
                {
                    this.Error(string.Format("Connect Failed {0} times,check your Network Connection. ",counter));
                    GameRoot.Instance.ShowTips("无法连接服务器，请检查网络状况");
                    checkTask=null;
                }
                else
                {
                    this.Warn(string.Format("COnnect Failed {0} times, Retry ...",counter));
                    checkTask = client.ConnectServer(100);
                }
            }
        }

        if(client != null && client.clientSession != null)
        {
            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    NetMsg msg = msgPackQue.Dequeue();
                    HandoutMsg(msg);
                }
            }
        }

    }

    public void SendMsg(NetMsg msg,Action<bool> cb = null)
    {
        if(client.clientSession != null && client.clientSession.IsConnected())
        {
            client.clientSession.SendMsg(msg);
            cb?.Invoke(true);
        }
        else
        {
            GameRoot.Instance.ShowTips("服务器未连接");
            this.Error("服务器未连接");
            cb?.Invoke(false);
        }
    }

    // 消息分发
    private void HandoutMsg(NetMsg msg)
    {
        if(msg.error != ErrorCode.None)
        {
            switch(msg.error)
            {
                case ErrorCode.AcctIsOnline:
                    GameRoot.Instance.ShowTips("当前账号已经上线了");
                    break;
                default:
                    break;
            }
            return;
        }

        switch (msg.cmd)
        {
            case CMD.RspLogin:
                LoginSys.Instance.RspLogin(msg);
                break;
            case CMD.RspMatch:
                LobbySys.Instance.RspMatch(msg);
                break;
            case CMD.NtfConfirm:
                LobbySys.Instance.NtfConfirm(msg);
                break;
            default:
                break;
        }
    }
}
