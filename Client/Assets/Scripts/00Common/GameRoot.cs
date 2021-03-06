using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEUtils;
using Protocol;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;

    public Transform uiRoot;
    public TipsWnd tipsWnd;

    void Start()
    {
        Instance = this;
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
        tipsWnd.SetWndState();
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


        loginSys.EnterLogin();
        this.Log("EnterLogin");
    }

    public void ShowTips(string tips)
    {
        tipsWnd.AddTips(tips);
    }

    #region
    UserData userData;
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private uint roomID;
    public uint RoomID
    {
        get { return roomID; }
        set { roomID = value; }
    }

    private int mapID;
    public int MapID { get { return mapID; } set { mapID = value; } }

    private int selfIndex;
    public int SelfIndex { get { return selfIndex; } set { selfIndex = value; } }

    private List<BattleHeroData> heroList;
    public List<BattleHeroData> HeroList { get { return heroList; } set { heroList = value; } }
    #endregion
}
