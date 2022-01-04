using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
声音服务
*/
public class AudioSvc : MonoBehaviour
{
    public static AudioSvc Instance;
    public void InitSvc()
    {
        Instance = this;
        this.Log("Init AudioSvc done");
    }
}
