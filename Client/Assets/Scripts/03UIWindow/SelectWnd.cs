using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄的选择界面
/// </summary>
public class SelectWnd : WindowRoot
{
    public Image imgHeroShow;
    public Text txtCountTime;
    public Transform transScrollRoot;
    public GameObject heroItem;
    public Button btnSure;
    public Transform transSkillIconRoot;

    private int timeCount;
    private List<HeroSelectData> heroSelectLst = null;
    private bool isSelected = false;

    protected override void InitWnd()
    {
        base.InitWnd();
        btnSure.interactable = true;
        isSelected = false;
        timeCount = ServerConfig.SelectCountDown;
        heroSelectLst = root.UserData.heroSelectData;

        for (int i = 0;i<transScrollRoot.childCount;i++)
        {
            DestroyImmediate(transScrollRoot.GetChild(i).gameObject);
        }

        for (int i = 0;i<heroSelectLst.Count;i++)
        {
            int heroID = heroSelectLst[i].heroID;
            GameObject go = Instantiate(heroItem);
            go.name = heroID.ToString();
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.SetParent(transScrollRoot);
            rect.localScale = Vector3.one;
        }
    }
}
