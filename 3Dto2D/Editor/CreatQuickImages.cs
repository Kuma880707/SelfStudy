using UnityEngine;
using UnityEditor;

public class CreatQuickImages : Editor
{
    [MenuItem("GameObject/QuickImages")]
    static void Run()
    {
        GameObject go = new GameObject();
        GameObjectUtility.SetParentAndAlign(go, Selection.activeGameObject);
        Selection.activeObject = go;
        go.AddComponent<QuickImagesList>();
    }   
}
