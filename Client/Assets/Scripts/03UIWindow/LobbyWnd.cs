using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ´óÌü½çÃæ
/// </summary>
public class LobbyWnd : WindowRoot
{
    public Text txtName;
    public Text txtLevel;
    public Text txtExp;
    public Image fgExp;

    public Text txtCoin;
    public Text txtDiamond;
    public Text txtTicket;

    public Transform transMatchRoot;
    public Text txtPredictTime;
    public Text txtCountTime;

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(transMatchRoot, false);
        UserData ud = root.UserData;
        txtName.text = ud.name;
        txtLevel.text = "LV."+ud.lv;
        txtExp.text = ud.exp + "/100000";
        fgExp.fillAmount = ud.exp * 1.0f / 10000;
        txtCoin.text = ud.exp.ToString();
        txtDiamond.text = ud.diamond.ToString();
        txtTicket.text = ud.ticket.ToString();
    }

    public void ClickMatchBtn()
    {
        audioSvc.PlayUIAudio("matchBtnClick");

    }
}
