using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
声音服务
*/
public class AudioSvc : MonoBehaviour
{
    public static AudioSvc Instance;
    public bool TurnOnVoice;
    public AudioSource bgAudio;
    public AudioSource uiAudio;
    public void InitSvc()
    {
        Instance = this;
        this.Log("Init AudioSvc done");
    }

    public void StopBGMusic()
    {
        if (bgAudio != null)
        {
            bgAudio.Stop();
        }
    }

    public void PlayBGMusic(string name, bool isLoop = true)
    {
        if(!TurnOnVoice)
        {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/"+name,true);
        if(bgAudio.clip == null || bgAudio.clip.name != audio.name)
        {
            bgAudio.clip = audio;
            bgAudio.loop = true;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name)
    {
        if(!TurnOnVoice)
        {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/"+name,true);
        uiAudio.clip = audio;
        uiAudio.Play();
    }
}
