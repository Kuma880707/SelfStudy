using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgNav : MonoBehaviour
{
    [HideInInspector]
    public Vector2 Pos;
    [HideInInspector]
    public GameObject pos;
    [HideInInspector]
    public float locktime = 0.3f;
    [HideInInspector]
    public Ray ray;
    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public float GraphicsLerp;


    public void GetInputPos()
    {        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition/GraphicsLerp); 
        if (Physics.Raycast(ray, out hit))
        {
            pos.transform.position = hit.point;
            Pos = pos.transform.position;
        }
    }
}
