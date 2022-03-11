using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMgr : UnitySingleton<UIMgr>
{
    public DefaultUI defaultUI;
    public LoginUI loginUI;
    public BigMapUI bigMapUI;
    private GameObject UIMask;

    private void Start()
    {
        UIMask = defaultUI.transform.Find("UIMask").gameObject;
        UIMask.SetActive(false);
    }
    public void SetUIMask(Transform UIRoot)
    {
        UIMask.transform.SetParent(UIRoot);
        UIMask.transform.SetAsFirstSibling();
        UIMask.SetActive(true);
        EventMgr.Instance.dispatch_event("UIMoveLock", true);
    }
    public void RemoveUIMask()
    {
        UIMask.transform.SetParent(defaultUI.transform);
        UIMask.transform.SetAsFirstSibling();
        UIMask.SetActive(false);
        EventMgr.Instance.dispatch_event("UIMoveLock", false);
    }
    // Start is called before the first frame update

    public void SetLoginUI()
    {
//#if UNITY_EDITOR
//        GameObject UIPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Dk_Project/Art/UI/LoginUI.prefab", typeof(GameObject));
//        loginUI = Instantiate(UIPrefab, defaultUI.transform).GetComponent<LoginUI>();
//        loginUI.transform.SetSiblingIndex(0);
        
//#endif
    }
    public void SetbigMapUI()
    {
//#if UNITY_EDITOR
//        GameObject UIPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Dk_Project/Art/UI/BigMapUI.prefab", typeof(GameObject));
//        bigMapUI = Instantiate(UIPrefab, defaultUI.transform).GetComponent<BigMapUI>();
//        bigMapUI.transform.SetSiblingIndex(2);
//#endif
    }

}
