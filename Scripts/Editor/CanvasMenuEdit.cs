using UnityEditor;
public class CanvasMenuEdit
{
    [MenuItem("3DCanvasChange/3DChange&Recover")]
    static void Run()
    {
        EditorWindow.GetWindow<CanvasMenu>();
    }
}


