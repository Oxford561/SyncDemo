using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    protected void SetActive(GameObject go,bool state = true)
    {
        go.SetActive(state);
    }

    protected void SetActive(Transform trans,bool state = true)
    {
        trans.gameObject.SetActive(state);
    }

    protected void SetActive(RectTransform rectTransform,bool state = true)
    {
        rectTransform.gameObject.SetActive(state);
    }

    protected void SetActive(Image img,bool state = true)
    {
        img.gameObject.SetActive(state);
    }

    protected void SetActive(Text txt,bool state = true)
    {
        txt.gameObject.SetActive(state);
    }

    protected void SetActive(InputField ipt,bool state = true)
    {
        ipt.gameObject.SetActive(state);
    }

    protected Image GetImage(Transform trans,string path)
    {
        if(trans != null)
        {
            return trans.Find(path).GetComponent<Image>();
        }
        else
        {
            return transform.Find(path).GetComponent<Image>();
        }
    }

    protected Image GetImage(Transform trans)
    {
        if (trans != null)
        {
            return trans.GetComponent<Image>();
        }
        else
        {
            return transform.GetComponent<Image>();
        }
    }

    protected Text GetText(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Text>();
        }
        else
        {
            return transform.Find(path).GetComponent<Text>();
        }
    }

    protected Transform GetTrans(Transform trans,string name)
    {
        if(trans != null)
        {
            return trans.Find(name);
        }
        else
        {
            return transform.Find(name);
        }
    }

    protected void SetSprite(Image image,string path)
    {
        Sprite sp = ResSvc.Instance.LoadSprite(path, true);
        image.sprite = sp;
    }
}
