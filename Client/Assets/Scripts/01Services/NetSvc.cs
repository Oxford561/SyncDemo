using PENet;
using PEUtils;
using Protocol;
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
    private KCPNet<ClientSession, NetMsg> client = new KCPNet<ClientSession, NetMsg>();
    private Queue<NetMsg> msgPackQue = new Queue<NetMsg>();
    public static readonly string pkgque_lock = "pkgque_lock";
    private Task<bool> checkTask = null;
    public void InitSvc()
    {
        Instance = this;

        msgPackQue.Clear();

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
                GameRoot.Instance.AddTips("连接服务器成功");
                this.Log("Connect Server Success");
                checkTask = null;
                // todo 发送 ping

            }
            else
            {
                ++counter;
                if(counter > 4)
                {
                    this.Error(string.Format("Connect Failed {0} times,check your Network Connection. ",counter));
                    GameRoot.Instance.AddTips("无法连接服务器，请检查网络状况");
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

    // 消息分发
    private void HandoutMsg(NetMsg pack)
    {

    }
}
