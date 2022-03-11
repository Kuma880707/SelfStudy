using UnityEngine;
using UnityEngine.UI;


public class DefaultUI : MonoBehaviour
{
    [HideInInspector]
    public RawImage rawImage;
    [HideInInspector]
    public Image Bg_Mask;
    GameObject loadingSlider;
    bool changeScene;

    void Awake()
    {        
        rawImage = this.transform.Find("SceneImage").GetComponent<RawImage>();
        //rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);       
        Bg_Mask = this.transform.Find("Bg_Mask").GetComponent<Image>();
        loadingSlider = transform.Find("LoadingSlider").gameObject;
        loadingSlider.transform.localScale = new Vector3(Screen.width / 2960, 1, 1);
        EventMgr.Instance.add_listener("ChangeScene", this.SetLoadingSlider);
    }

    void SetLoadingSlider(string uname, object udata)
    {
        loadingSlider.SetActive(true);
        changeScene = true;
    }

    void Update()
    {     
        if(SongMgr.Instance.songlerp.start)
        {            
            Bg_Mask.color = Color.Lerp(Color.black, Color.clear, SongMgr.Instance.songlerp.value);
            //Debug.Log(SongMgr.Instance.songlerp.value);
        }
        if(changeScene)
        {
            loadingSlider.GetComponent<Slider>().value = SceneMgr.Instance.loadingProgress;
            if(SceneMgr.Instance.loadingProgress == 1)
            {
                loadingSlider.SetActive(false);
                loadingSlider.GetComponent<Slider>().value = 0;
            }
        }
    }
}
