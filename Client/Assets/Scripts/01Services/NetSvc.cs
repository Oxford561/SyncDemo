using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
网络服务
*/
public class NetSvc : MonoBehaviour
{
    public static NetSvc Instance;
    public void InitSvc()
    {
        Instance = this;
        this.Log("Init NetSvc done");
    }
}