using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class QuickImagesList:MonoBehaviour
{
    public List<Sprite> sprite = new List<Sprite>();
    public float angle; 
    [ContextMenu("快速生成Image集合")]
    void CreatImages ()
    {
        if(sprite.Count == 0)
        {
            Debug.Log("未添加图片");
        }
        else
        {
            if (this.transform.childCount == 0)
            {
                for (int i = 0; i < sprite.Count; i++)
                {

                    GameObject go = new GameObject(sprite[i].name);
                    GameObjectUtility.SetParentAndAlign(go, this.gameObject);
                    Image image = go.AddComponent<Image>();
                    image.sprite = sprite[i];
                    image.SetNativeSize();
                    if (i == 0)
                    {
                        go.transform.eulerAngles = new Vector3(angle, 0, 0);
                    }
                }
            }
            else
            {
                List<string> children_name = new List<string>();
                for(int i = 0;i<this.transform.childCount;i++)
                {
                    string child_name = this.transform.GetChild(i).gameObject.name;
                    children_name.Add(child_name);
                }
                if(sprite.Count > children_name.Count)
                {
                    for (int i = children_name.Count; i < sprite.Count; i++)
                    {
                        GameObject go = new GameObject(sprite[i].name);
                        GameObjectUtility.SetParentAndAlign(go, this.gameObject);
                        Image image = go.AddComponent<Image>();
                        image.sprite = sprite[i];
                        image.SetNativeSize();
                    }
                }
            }           
        }
    }
}
