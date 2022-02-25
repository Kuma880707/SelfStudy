using System.IO;
using UnityEngine;
using UnityEditor;


public class ObjectToPrefab : EditorWindow
{
    public string stringToEdit = "";

    public void OnGUI()
    {
        GUILayout.Label("Prefab·��");
        stringToEdit = GUILayout.TextField(stringToEdit);

        if(GUILayout.Button("ObjectsToPrefab"))
        {
            if (stringToEdit != "" )
            {    
                if(File.Exists(stringToEdit))
                {
                   
                    GameObject obj = AssetDatabase.LoadAssetAtPath(stringToEdit, typeof(Object)) as GameObject;
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                       GameObject go = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                        go.transform.SetParent(Selection.gameObjects[i].transform.parent);
                        go.transform.localPosition = Selection.gameObjects[i].transform.localPosition;
                        go.transform.eulerAngles = Selection.gameObjects[i].transform.eulerAngles;
                        go.transform.localScale = Selection.gameObjects[i].transform.localScale;
                        int num = Selection.gameObjects[i].transform.GetSiblingIndex();
                        go.transform.SetSiblingIndex(num);
                        go.name = obj.name + i;                       
                    }
                        Debug.Log("���ɳɹ�");
                }
                else
                {
                    Debug.Log(string.Format("<color=#ff0000>{0}</color>", "·������"));
                }
            }
            if(stringToEdit == "")
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "û����д·��"));
            }
        }
        if (GUILayout.Button("DeleteObject"))
        {
            GameObject[] obj = Selection.gameObjects;
            for(int i = 0; i< obj.Length;i++)
            {
                DestroyImmediate(obj[i]);
            }
           
        }
    }
}
