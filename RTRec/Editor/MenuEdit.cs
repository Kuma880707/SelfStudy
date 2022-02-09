using UnityEditor;


public class MenuEdit
{
    [MenuItem("RTRec/TransRec")]
    static void Run()
    {
        EditorWindow.GetWindow<TransRec>();
    }
}
