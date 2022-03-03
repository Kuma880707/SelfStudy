
using UnityEngine;

public class Effect_Rain : MonoBehaviour
{
    ParticleSystem.EmissionModule rainEmission;
    [HideInInspector]
    public float rainCount;
    private void Awake()
    {       
        rainEmission = GetComponentInChildren<ParticleSystem>().emission;
        rainCount = rainEmission.rateOverTime.constant;       
    }

    private void LateUpdate()
    {
        rainEmission.rateOverTime = new ParticleSystem.MinMaxCurve(rainCount);
    }   
}
