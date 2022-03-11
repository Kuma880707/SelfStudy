using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRaycast :MonoBehaviour
{
    public int buildingNum;
    private GameObject getInBuilding;
    Animator anim;
    int ButtonOn = Animator.StringToHash("ButtonOn");
    Ray ray;
    RaycastHit hit;
    int laymask;
    bool rayOff;

    private void Awake()
    {      
        getInBuilding = this.transform.Find("GetInBuilding").gameObject;      
        laymask = 1 << 14;
        anim = getInBuilding.GetComponent<Animator>();
        anim.SetBool(ButtonOn, false);
        EventMgr.Instance.add_listener("UIMoveLock", SetRayOFF);
    }

    void SetRayOFF(string uname, object udata)
    {
        rayOff = (bool)udata;
    }
    private void LateUpdate()
    {
        if(!rayOff)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition / GameMgr.Instance.graphicsSet);
            if (Physics.Raycast(ray, out hit, 10000, laymask) && Input.GetMouseButtonDown(0))
            {
                GetInBuildingScene();
                EventMgr.Instance.dispatch_event("UIMoveLock", true);
            }
        }
        Debug.Log(rayOff);
        //if(Input.GetMouseButtonDown(0)&&!Physics.Raycast(ray, out hit, 10000, laymask))
        //{
        //    EventMgr.Instance.dispatch_event("UIMoveLock", false);
        //}
    }
    void GetInBuildingScene()
    {
        EventMgr.Instance.dispatch_event("ChangeSceneConfirm", buildingNum);
    }

    public void SetGetInButton(bool b)
    {       
        anim.SetBool(ButtonOn, b);
    }
    private void OnDisable()
    {
        EventMgr.Instance.dispatch_event("UIMoveLock", false);
        EventMgr.Instance.remove_listener("UIMoveLock", SetRayOFF);
    }
}
