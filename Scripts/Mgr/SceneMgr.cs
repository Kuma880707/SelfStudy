using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : UnitySingleton<SceneMgr>
{
    [HideInInspector]
    public float loadingProgress;
    private string sceneName;
    private AsyncOperation async;
    [HideInInspector]
    //LerpZeroToOne lerpZeroToOne;
    private void Start()
    {
        sceneName = "Login";
        SongMgr.Instance.SetAudio();
        EventMgr.Instance.add_listener("ChangeScene", this.ChangeScene);
        LoadingScene();
        //Invoke(nameof(LoadingScene), 1f);               
    }

    void ChangeScene(string uname, object udata)
    {
        sceneName = (string)udata;
        SongMgr.Instance.MusicVolumnDown();
        SongMgr.Instance.AllSoundsDown();
        Invoke(nameof(LoadingScene), 3f);
    }
    void LoadingScene()
    {       
        if (sceneName == "Login")
        {
            //场景加载
            //UI加载
            UIMgr.Instance.bigMapUI.gameObject.SetActive(false);
            UIMgr.Instance.defaultUI.rawImage.gameObject.SetActive(false);
            UIMgr.Instance.loginUI.gameObject.SetActive(true);
        }
        if (sceneName == "BigMap")
        {
            if(SceneManager.GetActiveScene().name != "BigMap")
            {
                async = SceneManager.LoadSceneAsync(sceneName);
            }
            UIMgr.Instance.defaultUI.rawImage.gameObject.SetActive(true);
            UIMgr.Instance.bigMapUI.gameObject.SetActive(true);
        }
        if(sceneName == "Palace")
        {
            Debug.Log("SceneToPalace");
        }
        if(sceneName == "Mansion")
        {
            Debug.Log("SceneToMansion");
        }
        if(sceneName == "Yard")
        {
            Debug.Log("SceneToYard");
        }
        if(sceneName == "Mine")
        {
            Debug.Log("SceneToMine");            
        }
        if (sceneName == "Village")
        {
            Debug.Log("SceneToVillage");
        }
        if (sceneName == "Market")
        {
            Debug.Log("SceneToMarket");
        }
        //声音加载           
        SetSceneMusic(sceneName);
        SoundsMgr.Instance.SetSoundFromScene(sceneName);
        CancelInvoke(nameof(LoadingScene));
    }
    void SetSceneMusic(string name)
    {

        if(name == "BigMap")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        if(name == "Login")
        {
            SongMgr.Instance.PlayMusic("brass");
        }
        if(name == "Palace")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        if(name == "Mansion")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        if(name == "Yard")
        {
            SongMgr.Instance.PlayMusic("harp");
        }    
        if(name == "Mine")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        if (name == "Village")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        if (name == "Market")
        {
            SongMgr.Instance.PlayMusic("harp");
        }
        SongMgr.Instance.MusicVolumnUp();
        SongMgr.Instance.AllSoundsUp();
    }

    private void Update()
    {
        if(async != null && loadingProgress < 1)
        {
            loadingProgress = async.progress;
        }    
    }
    //void OnDestory()
    //{
    //    EventMgr.Instance.remove_listener("StartGame", this.OnStartGame);
    //}
}
