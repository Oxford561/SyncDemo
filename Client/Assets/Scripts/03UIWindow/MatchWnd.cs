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
        int count = confirmArr.Length / 2;
        for (int i = 0; i < 5; i++)
        {
            Transform player = leftPlayerRoot.GetChild(i);
            if(i < count)
            {
                SetActive(player);
                string iconPath = "ResImages/MatchWnd/icon_"+confirmArr[i].iconIndex;
                string framePath = "ResImages/MatchWnd/frame_" + (confirmArr[i].confirmDone?"Sure":"normal");
                Image imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);
                Image imgFrame = GetImage(player,"img_state");
                SetSprite(imgFrame, framePath);
                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player, false);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            Transform player = rightPlayerRoot.GetChild(i);
            if (i < count)
            {
                SetActive(player);
                string iconPath = "ResImages/MatchWnd/icon_" + confirmArr[i+count].iconIndex;
                string framePath = "ResImages/MatchWnd/frame_" + (confirmArr[i+count].confirmDone ? "Sure" : "normal");
                Image imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);
                Image imgFrame = GetImage(player, "img_state");
                SetSprite(imgFrame, framePath);
                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player, false);
            }
        }

        int confirmCount = 0;
        for (int i = 0; i < confirmArr.Length; i++)
        {
            if(confirmArr[i].confirmDone)
            {
                confirmCount++;
            }
        }

        txtConfirm.text = confirmCount + "/" + confirmArr.Length+"就绪";
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
