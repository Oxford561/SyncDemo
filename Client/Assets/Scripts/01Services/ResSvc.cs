using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
