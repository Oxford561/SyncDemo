using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*资源服务
*/
public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;
    public void InitSvc()
    {
        Instance = this;
        this.Log("Init ResSvc done");
    }
}
