using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_StepSmoke : MonoBehaviour
{
    EffectChangeTime effectChangeTime;
    List<ParticleSystem> Smoke = new List<ParticleSystem>();
    private int num;
    [HideInInspector]
    public bool Play;
    void Awake()
    {
        effectChangeTime = this.GetComponent<EffectChangeTime>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            ParticleSystem p = this.transform.GetChild(i).GetComponent<ParticleSystem>();
            Smoke.Add(p);
        }
    }
    public void SmokeParticclePlay()
    {
        Smoke[num].Play();
        num++;
        if (num == Smoke.Count)
        {
            num = 0;
        }
        CancelInvoke(nameof(SmokeParticclePlay));
        SongMgr.Instance.clearEndSounds = true;
        EventMgr.Instance.dispatch_event("Footstep",true);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Play == true)
        {
            Invoke(nameof(SmokeParticclePlay), effectChangeTime.StepTime);
        }
    }
}
