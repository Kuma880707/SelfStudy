using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBindBuilding : MonoBehaviour
{
    public BuildingRaycast buildingRaycast;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buildingRaycast.SetGetInButton(true);
       // Debug.Log("OnTrigger");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        buildingRaycast.SetGetInButton(false);
       // Debug.Log("OnTrigger");
    }

}
