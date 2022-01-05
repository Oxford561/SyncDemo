using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 弹窗
public class TipsWnd : WindowRoot
{
    public Image bgTips;
    public Text txtTips;
    public Animator ani;

    private Queue<string> tipsQue = new Queue<string>();
    private bool isTipsShow = false;

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(bgTips, false);
        tipsQue.Clear();
    }


    void Update()
    {
        if (tipsQue.Count > 0 && isTipsShow == false)
        {
            string tips = tipsQue.Dequeue();
            isTipsShow = true;
            SetTips(tips);
        }
    }

    private void SetTips(string tips)
    {
        int len = tips.Length;
        SetActive(bgTips);
        txtTips.text = tips;
        bgTips.GetComponent<RectTransform>().sizeDelta = new Vector2(35 * len + 100, 80);
        ani.Play("TipsWindow",0,0f);// 直接重复播放动画
    }

    public void AddTips(string tips)
    {
        tipsQue.Enqueue(tips);
    }

    public void AniPlayDone()
    {
        SetActive(bgTips,false);
        isTipsShow = false;
    }

}
