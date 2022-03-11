using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundsMgr : UnitySingleton<SoundsMgr>
{
    public List<AudioClip> Sounds = new List<AudioClip>();
    public BigMapSounds BigMapSounds;
    public LoginSounds LoginSounds;

    private GameObject SoundPrefab;
    public void SetSoundFromScene(string sceneName)
    {
        
        if(transform.root.Find(sceneName + "Sounds") == null)
        {
            SoundPrefab = (GameObject)Resources.Load("Sounds/Prefabs/" + sceneName + "Sounds");
            Instantiate(SoundPrefab);
            SoundPrefab.name = sceneName + "Sounds";
        }     
        if(transform.root.Find(sceneName + "Sounds") != null)
        {
            SoundPrefab = transform.root.Find(sceneName + "Sounds").gameObject;
        }      
        ClearSoundEvent();
        if(sceneName == "Login")
        {
            LoginSounds = SoundPrefab.GetComponent<LoginSounds>();
            for (int i = 0; i < LoginSounds.LoginUISounds.Count; i++)
            {
                AudioClip sound = LoginSounds.LoginUISounds[i];
                Sounds.Add(sound);
            }
            EventMgr.Instance.add_listener("LoginUISounds", this.SetSounds);
        }
        if (sceneName == "BigMap")
        {
            BigMapSounds = SoundPrefab.GetComponent<BigMapSounds>();
            for ( int i = 0; i < BigMapSounds.Footstep_audio.Count;i++)
            {
                AudioClip sound = BigMapSounds.Footstep_audio[i];
                Sounds.Add(sound);
            }
            EventMgr.Instance.add_listener("FootstepSounds", this.SetSounds);
        }
        if(sceneName != "Login"&& sceneName != "BigMap")
        {
            BigMapSounds = SoundPrefab.GetComponent<BigMapSounds>();
            for (int i = 0; i < BigMapSounds.Footstep_audio.Count; i++)
            {
                AudioClip sound = BigMapSounds.Footstep_audio[i];
                Sounds.Add(sound);
            }
            EventMgr.Instance.add_listener("FootstepSounds", this.SetSounds);
        }
    }
    public void ClearSoundEvent()
    {
        if(Sounds.Count !=0)
        {
            EventMgr.Instance.remove_listener("FootstepSounds", this.SetSounds);
            Sounds.Clear();
        }      
    }
    // Update is called once per frame
    void SetSounds(string uname, object udata)
    {
        SongMgr.Instance.PlaySounds(Sounds[(int)udata].name);       
    }
}
