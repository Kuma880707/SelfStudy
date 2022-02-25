using UnityEngine;

public class EffectChangeTime : MonoBehaviour
{
    public float StepTime;
#if UNITY_EDITOR
    [ContextMenu("修改粒子时间")]
    void ParticleTimeChnage()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            ParticleSystem p = this.transform.GetChild(i).GetComponent<ParticleSystem>();
            var main = p.main;
            main.duration = StepTime;
            main.startLifetime = 2 * StepTime;
        }
    }
#endif
}
