using UnityEngine;


[RequireComponent(typeof(SceneMgr))]

public class GameMgr : UnitySingleton<GameMgr>
{
    public float graphicsSet = 1;

    private void Start()
    {
#if UNITY_EDITOR
        this.transform.Find("Camera").GetComponent<Camera>().enabled = true;
#endif
    }
}


