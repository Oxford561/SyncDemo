using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySys : MonoBehaviour
{
    public static LobbySys Instance;
    public void InitSys()
    {
        Instance = this;
        this.Log("Init LobbySys done");
    }
}
