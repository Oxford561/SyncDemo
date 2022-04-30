using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWnd : WindowRoot
{
    public Transform blueTeamRoot;
    public Transform redTeamRoot;

    private List<BattleHeroData> heroLst;
    private List<Text> txtPercentLst;
    protected override void InitWnd()
    {
        base.InitWnd();
        txtPercentLst = new List<Text>();
        audioSvc.PlayUIAudio("load");

        heroLst = root.HeroList;
        int count = heroLst.Count / 2;
        // blueTeam
        for (int i = 0; i < 5; i++)
        {
            Transform player = blueTeamRoot.GetChild(i);
            if(i < count)
            {
                SetActive(player);
                UnitCfg cfg = resSvc.GetUnitCfgByID(heroLst[i].heroID);
                SetSprite(GetImage(player, "imgHero"), "ResImages/LoadWnd/" + cfg.resName + "_load");
                SetText(GetText(player, "txtHeroName"), cfg.unitName);
                SetText(GetText(player, "bgName/txtPlayerName"), heroLst[i].userName);
                Text txtPrg = GetText(player, "txtProgress");
                SetText(txtPrg, "0%");
                txtPercentLst.Add(txtPrg);
            }
            else
            {
                SetActive(player,false);
            }
        }

        // redTeam
        for (int i = 0; i < 5; i++)
        {
            Transform player = redTeamRoot.GetChild(i);
            if (i < count)
            {
                SetActive(player);
                UnitCfg cfg = resSvc.GetUnitCfgByID(heroLst[i+count].heroID);
                SetSprite(GetImage(player, "imgHero"), "ResImages/LoadWnd/" + cfg.resName + "_load");
                SetText(GetText(player, "txtHeroName"), cfg.unitName);
                SetText(GetText(player, "bgName/txtPlayerName"), heroLst[i+count].userName);
                Text txtPrg = GetText(player, "txtProgress");
                SetText(txtPrg, "0%");
                txtPercentLst.Add(txtPrg);
            }
            else
            {
                SetActive(player, false);
            }
        }
    }

    public void RefreshPrgData(List<int> percentLst)
    {
        for (int i = 0; i < percentLst.Count; i++)
        {
            txtPercentLst[i].text = percentLst[i] + "%";
        }
    }
}
