using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 窗口基类
public class WindowRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetSvc netSvc;
    protected AudioSvc audioSvc;
    protected ResSvc resSvc;

    public virtual void SetWndState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            UnInitWnd();
        }
    }

    protected virtual void InitWnd()
    {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }

    protected virtual void UnInitWnd()
    {
        root = null;
        netSvc = null;
        resSvc = null;
        audioSvc = null;
    }
}
