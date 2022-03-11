
using UnityEngine;
using UnityEngine.UI;

public class BigMapUI : MonoBehaviour
{
    private Button SettingButton;
    private GameObject SettingUI;
    private GameObject ReturnLoginUI;
    private GameObject GetInBudingUI;
    private Slider musicslider;
    private Slider soundslider;
    private int buildingNum;

    void Start()
    {
        SettingButton = transform.Find("SettingButton").GetComponent<Button>();
        SettingButton.interactable = true;
        SettingUI = transform.Find("SettingButton/SettingUI").gameObject;
        ReturnLoginUI = transform.Find("SettingButton/ReturnLoginUI").gameObject;
        GetInBudingUI = transform.Find("GetInBudingUI").gameObject;
        SettingUI.SetActive(false);
        ReturnLoginUI.SetActive(false);
        GetInBudingUI.SetActive(false);
        musicslider = transform.Find("SettingButton/SettingUI/MusicSlider").GetComponent<Slider>();
        musicslider.value = SongMgr.Instance.musicVolumn;
        soundslider = transform.Find("SettingButton/SettingUI/SoundSlider").GetComponent<Slider>();
        soundslider.value = SongMgr.Instance.soundsVolumn;
        if (SongMgr.Instance.musicMuted)
        {
            musicslider.value = 0;
        }
        if (SongMgr.Instance.soundsMuted)
        {
            soundslider.value = 0;
        }
        EventMgr.Instance.add_listener("ChangeSceneConfirm", SetGetInBudingUI);
    }

    public void OnClickSetting()
    {
        SettingUI.SetActive(true);
        UIMgr.Instance.SetUIMask(SettingUI.transform);
        SettingButton.interactable = false;
    }

    public void OnClickSettingExit()
    {
        UIMgr.Instance.RemoveUIMask();
        SettingButton.interactable = true;
        SettingUI.SetActive(false);        
        Invoke(nameof(Dispatch_event), 0.3f);      
    }
    public void OnClickMusic()
    {
        SongMgr.Instance.SetMusicMute(false);
        musicslider.value = 1;
    }
    public void OnClickSound()
    {
        SongMgr.Instance.SetSoundsMute(false);
        soundslider.value = 1;
    }
    public void OnClickMusicMute()
    {
        SongMgr.Instance.SetMusicMute(true);
        musicslider.value = 0;
    }
    public void OnClickSoundMute()
    {
        SongMgr.Instance.SetSoundsMute(true);
        soundslider.value = 0;
    }
    public void MusicSlideChange()
    {
        SongMgr.Instance.SetMusicVolumn(musicslider.value);
    }

    public void SongSliderChange()
    {
        SongMgr.Instance.SetSoundsVolumn(soundslider.value);
    }

    public void ReturnLoginConfig()
    {
        SettingButton.interactable = false;
        ReturnLoginUI.SetActive(true);
        UIMgr.Instance.SetUIMask(ReturnLoginUI.transform);
        SettingUI.SetActive(false);
    }

    public void OnClickReturnLogin()
    {
        UIMgr.Instance.RemoveUIMask();
        EventMgr.Instance.dispatch_event("ChangeScene", "Login");
        this.gameObject.SetActive(false);
    }

    public void OnClickReturnSetting()
    {       
        SettingUI.SetActive(true);
        UIMgr.Instance.SetUIMask(SettingUI.transform);
        ReturnLoginUI.SetActive(false);
    }
    void SetGetInBudingUI(string uname, object udata)
    {
        buildingNum = (int)udata;
        GetInBudingUI.SetActive(true);
        UIMgr.Instance.SetUIMask(GetInBudingUI.transform);
        SettingButton.interactable = false ;
    }
    public void OnClickGetIn()
    {
        switch (buildingNum)
        {
            case 0:
                EventMgr.Instance.dispatch_event("ChangeScene", "Palace");
                break;
            case 3:
                EventMgr.Instance.dispatch_event("ChangeScene", "Mansion");
                break;
            case 4:
                EventMgr.Instance.dispatch_event("ChangeScene", "Yard");
                break;
            case 5:
                EventMgr.Instance.dispatch_event("ChangeScene", "Mine");
                break;
            case 6:
                EventMgr.Instance.dispatch_event("ChangeScene", "Village");
                break;
            case 7:
                EventMgr.Instance.dispatch_event("ChangeScene", "Market");
                break;
        }
        GetInBudingUI.SetActive(false);
        SettingButton.interactable = true;
        Invoke("Dispatch_event", 0.3f);
    }

    public void OnClickGetOut()
    {
        UIMgr.Instance.RemoveUIMask();
        GetInBudingUI.SetActive(false);
        SettingButton.interactable = true;
        
        Invoke(nameof(Dispatch_event), 0.3f);
    }
    public void OnDisable()
    {
        SettingButton.interactable = true;
        ReturnLoginUI.SetActive(false);
        SettingUI.SetActive(false);
        GetInBudingUI.SetActive(false);
        EventMgr.Instance.dispatch_event("UIMoveLock", false);
        EventMgr.Instance.remove_listener("ChangeSceneConfirm", SetGetInBudingUI);
    }
    public void OnEnable()
    {
        EventMgr.Instance.add_listener("ChangeSceneConfirm", SetGetInBudingUI);
    }
    public void Dispatch_event()
    {
        EventMgr.Instance.dispatch_event("UIMoveLock", false);
    }


}
