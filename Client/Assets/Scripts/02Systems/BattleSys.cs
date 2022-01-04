using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : MonoBehaviour
{
    public static BattleSys Instance;
    public void InitSys()
    {
        Instance = this;
        this.Log("Init BattleSys done");
    }
}
