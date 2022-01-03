using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEUtils;

public class GameRoot : MonoBehaviour
{
    void Start()
    {
        LogConfig cfg = new LogConfig{
            enableLog = true,
            logPrefix = "",
            enableTime = true,
            logSeparate = ">",
            enableThreadID = true,
            enableTrace = true,
            enableSave = true,
            enableCover = true,
            saveName = "ClientLog.txt",
            loggerType = LoggerType.Unity
        };
        PELog.InitSettings(cfg);

        PELog.ColorLog(LogColor.Green,"InitLog");
    }

    void Update()
    {
        
    }
}
