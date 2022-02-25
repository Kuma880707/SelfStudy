using UnityEditor;

public class ObjectToPrefabEditor :Editor
{
    [MenuItem("GameObject/ObjectToPrefab")]
    static void Run()
    {
        EditorWindow.GetWindow<ObjectToPrefab>();
    }
}
