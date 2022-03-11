using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    bool startgame;
    GameObject startButton;
    GameObject Bg;
    // Start is called before the first frame update
    void Start()
    {
        startButton = transform.Find("StartButton").gameObject;      
        Bg = transform.Find("Bg").gameObject;
        Bg.gameObject.SetActive(true);
    }

    public void OnClickStart()
    {
        startgame = true;      
        startButton.SetActive(false);
        EventMgr.Instance.dispatch_event("LoginUISounds", 0);
        Invoke(nameof(SetStartSounds), 0.5f);  
        EventMgr.Instance.dispatch_event("ChangeScene", "BigMap");
    }

    private void Update()
    {
        if (SceneMgr.Instance.loadingProgress == 1&& startgame)
        {
            Bg.gameObject.SetActive(false);
            Invoke("SetOff", 1f);
        }
    }
    void SetOff()
    {
        this.gameObject.SetActive(false);
        CancelInvoke("SetOff");
    }
    void SetStartSounds()
    {
        EventMgr.Instance.dispatch_event("LoginUISounds", 1);
        Debug.Log("PlaySounds1");
        CancelInvoke(nameof(SetStartSounds));
    }
    private void OnDisable()
    {
        Bg.gameObject.SetActive(true);
        startButton.SetActive(true);
        startgame = false;
    }
}
