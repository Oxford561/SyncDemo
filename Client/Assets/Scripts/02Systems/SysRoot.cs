using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 业务系统基类
public class SysRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetSvc netSvc;
    protected AudioSvc audioSvc;
    protected ResSvc resSvc;

    public virtual void InitSys()
    {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}
