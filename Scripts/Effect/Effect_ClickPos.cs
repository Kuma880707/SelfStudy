using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_ClickPos : MonoBehaviour
{
    public float partical_time;
    public BgNav bgNav;
    public List<GameObject> Click_Pos = new List<GameObject>();
    private GameObject effect_click;
    private int num;
    private void Awake()
    {
        if(bgNav !=null)
        {
            effect_click = this.transform.GetChild(0).gameObject;
            Click_Pos.Add(effect_click);
            if (bgNav.locktime < 2 && bgNav.locktime > 0)
            {
                num = (int)Mathf.Ceil(partical_time / bgNav.locktime);
            }
            else
            {
                Debug.Log("locktime is out of range");

            }
            for (int i = 0; i < num; i++)
            {
                GameObject effect_clone = Instantiate(effect_click);
                effect_clone.transform.SetParent(this.transform);
                effect_clone.transform.localPosition = effect_click.transform.localPosition;
                effect_clone.transform.localScale = effect_click.transform.localScale;


                Click_Pos.Add(effect_clone);
            }
        }

    }
}
