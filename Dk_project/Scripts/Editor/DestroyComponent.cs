using UnityEditor;
using UnityEngine;

public class DestroyComponent : Editor
{
    [MenuItem("Component/DestoryComponetForEditor")]
    static void DestoryComponet()
    {
        QuickImagesList[] quickImages =FindObjectsOfType<QuickImagesList>();      
        foreach (Component item in quickImages)
        {
            DestroyImmediate(item);
        }
    }
}
