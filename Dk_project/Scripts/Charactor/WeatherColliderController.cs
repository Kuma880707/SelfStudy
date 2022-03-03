using UnityEngine;
using AkilliMum.Standard.D2WeatherEffects;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;

public enum WeatherType
{
    Fine,
    Snow,
    SandFog,
    Rain
}
public class WeatherColliderController : MonoBehaviour
{
    private D2SnowsPE snowsPE;
    private D2FogsNoiseTexPE fogPE;
    private D2RainsFastPE rainPE;
    private LerpZeroToOne lerpZeroToOne;
    private PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private Bloom bloom;
    WeatherType weather;
    private bool weatherChange;
    public Effect_Rain effectRain;
    private float effectRainOriCount;
    public List<Color> overColor = new List<Color>();
    private List<Color> vignetteColor = new List<Color>();
    


    private void Awake()
    {
        for(int i = 0; i< overColor.Count;i++)
        {
            Color color = new Color(overColor[i].r * 0.2f, overColor[i].g * 0.2f, overColor[i].b * 0.2f);
            vignetteColor.Add(color);
        }
        postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();
      
        bloom = postProcessVolume.profile.GetSetting<Bloom>();
        bloom.color.value = overColor[0];

        vignette = postProcessVolume.profile.GetSetting<Vignette>();
        vignette.smoothness.value = 0.5f;

        vignette.color.value = vignetteColor[0];

        snowsPE = Camera.main.GetComponent<D2SnowsPE>();
        snowsPE.enabled = false;
        snowsPE.ParticleMultiplier = 1;
        fogPE = Camera.main.GetComponent<D2FogsNoiseTexPE>();
        fogPE.enabled = false;
        fogPE.Density = 0;
        rainPE = Camera.main.GetComponent<D2RainsFastPE>();
        rainPE.enabled = false;
        rainPE.Density = 1;
        effectRainOriCount = effectRain.rainCount;
        effectRain.gameObject.SetActive(false);
        weather = WeatherType.Fine;

        lerpZeroToOne = new LerpZeroToOne
        {
            value = 0,
            start = false,
            Speed = 0.5f
        };

    }
    private void Update()
    {
        lerpZeroToOne.Update();

    }
    private void LateUpdate()
    {
        if (weatherChange)
        {
            lerpZeroToOne.start = true;
            Debug.Log(lerpZeroToOne.value);
            switch (weather)
            {
                case WeatherType.Snow:
                    snowsPE.enabled = true;
                    FineToSnow(lerpZeroToOne.value);                 
                    break;
                case WeatherType.SandFog:
                    fogPE.enabled = true;
                    FineToSandFog(lerpZeroToOne.value);                  
                    break;
                case WeatherType.Rain:
                    rainPE.enabled = true;
                    effectRain.gameObject.SetActive(true);
                    FineToRain(lerpZeroToOne.value);                    
                    break;
            }
            if (lerpZeroToOne.value < 0 || lerpZeroToOne.value > 1)
            {                        
                if(lerpZeroToOne.value < 0)
                {
                    effectRain.gameObject.SetActive(false);
                    snowsPE.enabled = false;
                    fogPE.enabled = false;
                    rainPE.enabled = false;
                    weather = WeatherType.Fine;
                }
                weatherChange = false;
                Debug.Log(weatherChange);
            }
        }
        Debug.Log(weather);
    }
    void FineToSnow(float lerp)
    {
        snowsPE.ParticleMultiplier = Mathf.Lerp(1, 4, lerp);
        vignette.smoothness.value = Mathf.Lerp(0.5f, 1, lerp);
        vignette.color.value = Color.Lerp(vignetteColor[0], vignetteColor[1], lerp);
        bloom.color.value = Color.Lerp(overColor[0], overColor[1], lerp);
    }

    void FineToSandFog(float lerp)
    {
        fogPE.Density = Mathf.Lerp(0, 0.75f, lerp);
        vignette.smoothness.value = Mathf.Lerp(0.5f, 1, lerp);
        vignette.color.value = Color.Lerp(vignetteColor[0], vignetteColor[2], lerp);
        bloom.color.value = Color.Lerp(overColor[0], overColor[2], lerp);
    }

    void FineToRain(float lerp)
    {
        rainPE.Density = Mathf.Lerp(1, 1.5f, lerp);
        vignette.smoothness.value = Mathf.Lerp(0.5f, 1, lerp);
        vignette.color.value = Color.Lerp(vignetteColor[0], vignetteColor[3], lerp);
        bloom.color.value = Color.Lerp(overColor[0], overColor[3], lerp);
        effectRain.rainCount = Mathf.Lerp(0, effectRainOriCount, lerp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Snow")
        {
            weather = WeatherType.Snow;          
        }
        if(collision.tag == "SandFog")
        {
            weather = WeatherType.SandFog;
        }
        if(collision.tag == "Rain")
        {
            weather = WeatherType.Rain;
        }
        weatherChange = true;
        lerpZeroToOne.deltaT = 0.05f;
        Debug.Log(weatherChange);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {        
        weatherChange = true;
        lerpZeroToOne.deltaT = -0.05f;
        Debug.Log("OUT");
    }
}
