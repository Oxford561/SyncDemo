using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
*资源服务
*/
public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;
    public void InitSvc()
    {
        Instance = this;
        this.Log("Init ResSvc done");
    }

    private Dictionary<string,AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path,bool cache = false)
    {
        AudioClip au = null;
        if(!adDic.TryGetValue(path,out au))
        {
            au = Resources.Load<AudioClip>(path);
            if(cache)
            {
                adDic.Add(path,au);
            }
        }
        return au;
    }

    private Dictionary<string, Sprite> spDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path,bool cache=false)
    {
        Sprite au = null;
        if (!spDic.TryGetValue(path, out au))
        {
            au = Resources.Load<Sprite>(path);
            if (cache)
            {
                spDic.Add(path, au);
            }
        }
        return au;
    }

    public UnitCfg GetUnitCfgByID(int unitID)
    {
        switch(unitID)
        {
            case 101:
                return new UnitCfg
                {
                    unitID = 101,
                    unitName = "亚瑟",
                    resName ="arthur",
                };
            case 102:
                return new UnitCfg
                {
                    unitID = 102,
                    unitName = "后羿",
                    resName = "houyi",
                };
        }
        return null;
    }

    private Action prgCB = null;
    public void AsyncLoadScene(string sceneName,Action<float> loadRate,Action loaded)
    {
        AsyncOperation scenenAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float progress = scenenAsync.progress;
            loadRate?.Invoke(progress);
            if (progress == 1)
            {
                loaded?.Invoke();
                prgCB = null;
                scenenAsync = null;
            }
        };
    }

    private void Update()
    {
        prgCB?.Invoke();
    }
}
