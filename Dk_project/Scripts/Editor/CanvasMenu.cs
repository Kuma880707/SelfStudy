using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class CanvasMenu : EditorWindow
{
    public string stringToEdit = "";
    public int angel;
    public float colliderX = 1.0f;
    public float colliderY = 1.0f;
    public void OnGUI()
    {
        GameObject BgMesh = GameObject.Find("BgMesh");
        Camera main = Camera.main;
        GUILayout.Label("摄像机角度(整数)");

        stringToEdit = GUILayout.TextField(stringToEdit);
        if (Selection.activeTransform == null)
        {
            GUILayout.Label("请选择一个Bg");
        }
        if (Selection.activeTransform != null && Selection.activeTransform.name != "Bg")
        {
            GUILayout.Label("请选择Bg");
        }
        if (Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
        {
            GUILayout.Label("选择Bg成功");
        }
        if (GUILayout.Button("2DTo3D"))
        {
            if (stringToEdit != "" && Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
            {
                angel = int.Parse(stringToEdit);
                main.transform.parent.eulerAngles = new Vector3(angel, 0, 0);
                main.transform.eulerAngles = Vector3.zero;
                Selection.activeTransform.transform.eulerAngles = new Vector3(angel, 0, 0);
                int count = Selection.activeTransform.childCount;
                for (int i = 0; i < count; i++)
                {
                    Transform trans = Selection.activeTransform.GetChild(i);
                    trans.eulerAngles = Vector3.zero;
                }
                BgMesh.transform.eulerAngles = new Vector3(angel, 0, 0);
                Selection.activeGameObject = main.transform.parent.gameObject;
            }
            if (stringToEdit == "")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有输入角度"));
            }
            if (Selection.activeTransform == null || Selection.activeTransform.name != "Bg")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有选择Bg"));
            }

        }
        if (GUILayout.Button("3DTo2D"))
        {
            if (stringToEdit != "" && Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
            {
                Selection.activeTransform.transform.eulerAngles = Vector3.zero;
                main.transform.parent.eulerAngles = Vector3.zero;
                main.transform.eulerAngles = Vector3.zero;
                int count = Selection.activeTransform.childCount;
                for (int i = 0; i < count; i++)
                {
                    Transform trans = Selection.activeTransform.GetChild(i);
                    trans.eulerAngles = Vector3.zero;
                }
                BgMesh.transform.eulerAngles = Vector3.zero;
                Selection.activeGameObject = main.transform.parent.gameObject;
            }
            if (stringToEdit == "")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有输入角度"));
            }
            if (Selection.activeTransform == null || Selection.activeTransform.name != "Bg")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有选择Bg"));
            }

        }
        if (GUILayout.Button("ChangeLayer"))
        {
            if (Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
            {
                List<Transform> Trans = new List<Transform>();
                List<float> PosY = new List<float>();
                List<Transform> ChangeTrans = new List<Transform>();
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    Transform tran = Selection.activeTransform.GetChild(i);
                    float y = tran.localPosition.y - i / 100;
                    Trans.Add(tran);
                    PosY.Add(y);
                }
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    float[] posy = new float[PosY.Count];
                    PosY.CopyTo(posy);
                    float MaxY = Mathf.Max(posy);
                    int count = PosY.IndexOf(MaxY);
                    PosY.Remove(MaxY);
                    ChangeTrans.Add(Trans[count]);
                    Trans.RemoveAt(count);
                }

                for (int i = 0; i < ChangeTrans.Count; i++)
                {
                    ChangeTrans[i].SetSiblingIndex(i);
                }
                Debug.Log("图片层级变化成功");
            }

            if (Selection.activeTransform == null || Selection.activeTransform.name != "Bg")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有选择Bg"));
            }
        }
        colliderX = EditorGUILayout.FloatField("碰撞宽度", colliderX);
        colliderY = EditorGUILayout.FloatField("碰撞高度", colliderY);
        if (GUILayout.Button("Creat2DCollider"))
        {
            if (Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
            {
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    RectTransform each = Selection.activeTransform.GetChild(i) as RectTransform;
                    if (each.transform.gameObject.GetComponent<BoxCollider2D>() == null)
                    {
                        BoxCollider2D collider2D = each.transform.gameObject.AddComponent<BoxCollider2D>();
                        collider2D.size = new Vector2(each.sizeDelta.x * colliderX, each.sizeDelta.y * colliderY);
                        float offsetY = each.pivot.y * each.sizeDelta.y;
                        collider2D.offset = new Vector2(0, offsetY);
                    }
                    else
                    {
                        BoxCollider2D collider2D_old = each.transform.gameObject.GetComponent<BoxCollider2D>();
                        DestroyImmediate(collider2D_old);
                        BoxCollider2D collider2D = each.transform.gameObject.AddComponent<BoxCollider2D>();
                        collider2D.size = new Vector2(each.sizeDelta.x * colliderX, each.sizeDelta.y * colliderY);
                        float offsetY = each.pivot.y * each.sizeDelta.y;
                        collider2D.offset = new Vector2(0, offsetY);
                    }
                }
                Debug.Log("碰撞添加成功");
            }

            if (Selection.activeTransform == null || Selection.activeTransform.name != "Bg")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有选择Bg"));
            }
        }
        if (GUILayout.Button("Delte2DCollider"))
        {
            if (Selection.activeTransform != null && Selection.activeTransform.name == "Bg")
            {
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    RectTransform each = Selection.activeTransform.GetChild(i) as RectTransform;
                    if (each.transform.gameObject.GetComponent<BoxCollider2D>() != null)
                    {
                        BoxCollider2D collider2D = each.transform.gameObject.GetComponent<BoxCollider2D>();
                        DestroyImmediate(collider2D);
                    }
                    else
                    {
                        Debug.Log(string.Format("<color=#ff0000>{0}</color>", each.name + "没有碰撞"));
                        continue;
                    }
                    Debug.Log("碰撞删除成功");

                }
            }
            if (Selection.activeTransform == null || Selection.activeTransform.name != "Bg")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有选择Bg"));
            }
        }
        if (GUILayout.Button("UseNav2D"))
        {
            GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach (GameObject pObject in pAllObjects)
            {
                if (pObject.tag == "JoyStick")
                {
                    pObject.SetActive(false);
                }
                if (pObject.tag == "Nav2D")
                {
                    pObject.SetActive(true);
                }
            }
        }
        if (GUILayout.Button("UseNav_JoyStick"))
        {
            GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach (GameObject pObject in pAllObjects)
            {
                if (pObject.tag == "Nav2D")
                {
                    pObject.SetActive(false);
                }
                if (pObject.tag == "JoyStick")
                {
                    pObject.SetActive(true);                
                }
            }
        }
    }
    private void OnSelectionChange()
    {
        this.Repaint();
    }
    void OnInspectorUpdate()
    {
        this.Repaint();
    }

}

