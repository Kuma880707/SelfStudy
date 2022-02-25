using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgNav : MonoBehaviour
{
   //[HideInInspector]
    public Vector2 Pos;
    //[HideInInspector]
    public GameObject pos;
    //[HideInInspector]
    public float locktime;
    //[HideInInspector]
    Ray ray;
    //[HideInInspector]
    RaycastHit hit;

    public void GetInputPos()
    {        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);      
        if (Physics.Raycast(ray, out hit))
        {
            pos.transform.position = hit.point;
            Pos = pos.transform.position;      
        }
    }
}
