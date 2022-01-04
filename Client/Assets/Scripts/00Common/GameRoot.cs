using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEUtils;

public class GameRoot : MonoBehaviour
{
    [SerializeField]
    private Transform uiRoot;
    void Start()
    {
        LogConfig cfg = new LogConfig
        {
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
        DontDestroyOnLoad(this);
        InitRoot();

        PELog.ColorLog(LogColor.Green, "InitRoot");

        Init();

        this.Log("Init Done");
    }

    void InitRoot()
    {
        for (int i = 0; i < uiRoot.childCount; i++)
        {
            Transform trans = uiRoot.GetChild(i);
            trans.gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }

    private NetSvc netSvc;
    private AudioSvc audioSvc;
    private ResSvc resSvc;
    private LoginSys loginSys;
    private LobbySys lobbySys;
    private BattleSys battleSys;

    void Init()
    {
        netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();

        audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();

        resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();

        loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();

        lobbySys = GetComponent<LobbySys>();
        lobbySys.InitSys();

        battleSys = GetComponent<BattleSys>();
        battleSys.InitSys();

    }
}
