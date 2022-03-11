using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraEditor))]
public class CameraEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var target = (CameraEditor)(serializedObject.targetObject);        
        Camera.main.transform.localPosition = new Vector3(0, 300, -target.z);       
        
        float dx = Mathf.Lerp(3, 0.5f, 0.25f);
        target.x = EditorGUILayout.Slider("◊Û”“", target.x, -220f, 220f );
        target.y = EditorGUILayout.Slider("…œœ¬", target.y, -450f, 450f);
        target.z = EditorGUILayout.FloatField("æ‡¿Î", -Camera.main.transform.localPosition.z);
        target.transform.localPosition = new Vector3(target.x, target.y, (-target.z * 0.5f+ target.y*Mathf.Tan((target.transform.eulerAngles.x*Mathf.PI)/180)));
        target.Fov = Camera.main.fieldOfView;
        target.Fov = EditorGUILayout.Slider("Õ∏ ”", target.Fov, 0f, 100f);
        Camera.main.fieldOfView = target.Fov;
    }

}
