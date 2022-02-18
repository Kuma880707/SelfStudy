using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CanvasMenu : EditorWindow
{
    public string stringToEdit = "";
    public int angel;

    public void OnGUI()
    {
        Camera main = Camera.main;
        GUILayout.Label("摄像机角度(整数)");
        stringToEdit = GUILayout.TextField(stringToEdit);
        if (Selection.activeTransform == null)
        {
            GUILayout.Label("请选择一个Bg");
        }
        if (Selection.activeTransform != null && Selection.activeTransform.name != "Bg")
        {
            GUILayout.Label("请选择一个Bg");
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
                for (int i = 0; i < count;i++)
                {
                    Transform trans = Selection.activeTransform.GetChild(i);
                    trans.eulerAngles = Vector3.zero;
                }
            }
            if (stringToEdit == "")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "没有输入角度"));
            }
            if (Selection.activeTransform == null|| Selection.activeTransform.name != "Bg")
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
                for (int i = 0; i < count;i++)
                {
                    Transform trans = Selection.activeTransform.GetChild(i);
                    trans.eulerAngles = Vector3.zero;
                }
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
                List <Transform> Trans = new List<Transform>();
                List<float> PosY = new List<float>();
                List<Transform> ChangeTrans = new List<Transform>();
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    Transform tran = Selection.activeTransform.GetChild(i);
                    float y = tran.localPosition.y - i/100;
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
                
                for (int i = 0; i < ChangeTrans.Count;i++)
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
    }
    private void OnSelectionChange()
    {
        this.Repaint();
    }
}

