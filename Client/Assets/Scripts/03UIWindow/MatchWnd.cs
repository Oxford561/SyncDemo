using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol;

// 匹配确认
public class MatchWnd : WindowRoot
{
    public Text txtTime;
    public Text txtConfirm;
    public Transform leftPlayerRoot;
    public Transform rightPlayerRoot;
    public Button btnConfirm;

    private int timeCount;

    protected override void InitWnd()
    {
        base.InitWnd();

        timeCount = ServerConfig.ConfirmCountDown;
        btnConfirm.interactable = true;
        audioSvc.PlayUIAudio("matchReminder");
    }

    public void RefreshUI(ConfirmData[] confirmArr)
    {

    }

    private float deltaCount;
    void Update()
    {
        float delta = Time.deltaTime;
        deltaCount+=delta;
        if(deltaCount >= 1)
        {
            deltaCount-=1;
            timeCount-=1;
            if(timeCount < 0)
            {
                timeCount = 0;
            }

            txtTime.text = timeCount.ToString();
        }
    }
}
