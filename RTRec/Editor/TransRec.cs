using UnityEditor;
using UnityEngine;
using System.IO;

public class TransRec : EditorWindow
{
    public string stringToEdit = "";
    public void OnGUI()
    {
        GUILayout.Label("注意：不清记录未保存时读取会出错，运行时不要修改父子结构");
        if (GUILayout.Button("存自己和所有子物体"))
        {
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount != 0)
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    CreateRTPrefabSave();
                }
                GamePosRecorder.AllTransSave();
                Debug.Log("保存成功");
                name = Selection.activeTransform.name;
                PlayerPrefs.SetString("GUI" + name, name);
                Debug.Log("保存物体名" + name);
            }
            if (!EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "非运行无法保存"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无选择无法保存"));
            }
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount == 0)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无子物体无法保存"));
            }
        }
        GUILayout.Label("注意：新建物体放在CreateRTPrefab里才可以被保存和读取");

        if (Selection.activeTransform != null)
        {
            GUILayout.Label("正选择物体名" + Selection.activeTransform.name);
        }
        if (GUILayout.Button("读取记录"))
        {
            if (!EditorApplication.isPlaying && Selection.activeTransform != null && PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    CreateRTPrefabLoad();
                }
                GamePosRecorder.OriTransSave();
                GamePosRecorder.TransLoad();
                Debug.Log("读取成功");
            }
            if (EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "运行无法读取"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无选择无法读取"));
            }
            if (!PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "选择物体名不一致"));
            }

        }
        if (GUILayout.Button("返回原位"))
        {
            if (!EditorApplication.isPlaying && Selection.activeTransform != null && PlayerPrefs.HasKey("-1") && PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    DeletRTPrefab();
                }
                GamePosRecorder.OriTransLoad();
                Debug.Log("复位成功");
            }
            if (EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "运行无法复位"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无选择无法复位"));
            }
            if (!PlayerPrefs.HasKey("-1"))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "未读取无需复位"));
            }
            if (PlayerPrefs.HasKey("-1") && !PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "选择物体名不一致"));
            }
        }

        if (GUILayout.Button("清除存档"))
        {
            int count = PlayerPrefs.GetInt("PrefabRes");
            for (int i =0,h = 1; i< count;i++,h++)
            {
                string name = PlayerPrefs.GetString("PrefabRes" + "" + h);
                AssetDatabase.DeleteAsset("Assets/RTRec/Editor/GameObjectMenuCtrl/" + name + "_MenuSrc" + ".cs");
            }                
            PlayerPrefs.DeleteAll();        
            Debug.Log("清除成功");
        }

        GUILayout.Label("PrefabPath");
        stringToEdit = GUILayout.TextField(stringToEdit);
        if (GUILayout.Button("读取路径并生成脚本"))
        {
            string path = string.Format(stringToEdit);
            if (Directory.Exists(path))
            {
                GetFileNameINPath(path);
            }
            else
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "路径出错"));
            }
        }
        GUILayout.Label("注意：生成脚本后，下次点击不会重新生成");
        GUILayout.Label("生成完成后需切换引擎一下重新编译");
        GUILayout.Label("创建出的物体不要改名字");
    }
    public void GetFileNameINPath(string path)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            Debug.Log(string.Format("<color=#ff0000>{0}</color>", "路径内无文件"));
        }
        else
        {
            for (int i = 0, j = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                j++;
                string filename = Path.GetFileNameWithoutExtension(files[i].Name);
                if (PlayerPrefs.HasKey("PrefabRes"))
                {
                    PlayerPrefs.DeleteKey("PrefabRes");
                }
                PlayerPrefs.SetInt("PrefabRes", j);
                PlayerPrefs.SetString("PrefabRes" + "" + j, filename);
                //自动编写代码，生成gameObject右键菜单栏GUI代码
                CreatGameObjectMenuCtrl(filename, path + "/" + files[i].Name);
                //Debug.Log(PlayerPrefs.GetInt("PrefabRes"));
                //Debug.Log(PlayerPrefs.GetString("PrefabRes" + "" + j));
                //      
                //Debug.Log(files[i].Name + "生成完成");
                //Debug.Log(PrefabName.Count);
            }
        }
    }
    public void CreateRTPrefabSave()
    {
        string path = string.Format(stringToEdit);
        if (PlayerPrefs.HasKey("PrefabRes"))
        {
            int count = PlayerPrefs.GetInt("PrefabRes");
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount != 0)
            {
                for (int i = 0, h = 1; i < count; i++, h++)
                {
                    string name = PlayerPrefs.GetString("PrefabRes" + "" + h);
                    for (int j = 0, k = 0; j < Selection.activeTransform.childCount; j++)
                    {
                        if (Selection.activeTransform.GetChild(j).name == name + "(Clone)")
                        {
                            k++;
                            if (PlayerPrefs.HasKey(name))
                            {
                                PlayerPrefs.DeleteKey(name);
                            }
                            PlayerPrefs.SetInt(name, k);
                        }
                    }
                }
                Debug.Log("prefab存取成功");
                //for (int i = 0; i < PrefabName.Count; i++)
                //{
                //    Debug.Log(PlayerPrefs.GetInt(PrefabName[i]));
                //}
            }
            if (!EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "非运行无法保存"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无选择无法保存"));
            }
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount == 0)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无子物体无法保存"));
            }
        }
        else
        {
            Debug.Log("路径文件需重新记录");
        }
    }
    public void CreateRTPrefabLoad()
    {
        string path = string.Format(stringToEdit);
        if (!PlayerPrefs.HasKey("PrefabRes"))
        {
            Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无路径文件记录无法读取"));
        }
        else
        {
            string Name = PlayerPrefs.GetString("PrefabRes" + "" + 1);
            if (!EditorApplication.isPlaying && Selection.activeTransform != null && PlayerPrefs.HasKey(Name))
            {
                int count = PlayerPrefs.GetInt("PrefabRes");
                for (int i = 0, h = 1; i < count; i++, h++)
                {
                    string name = PlayerPrefs.GetString("PrefabRes" + "" + h);
                    GameObject res = (GameObject)AssetDatabase.LoadAssetAtPath(path + "/" + name + ".prefab", typeof(GameObject));
                    for (int j = 0; j < PlayerPrefs.GetInt(name); j++)
                    {
                        Instantiate(res, Selection.activeTransform);
                    }
                }
            }
            if (EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "运行无法读取"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "无选择无法读取"));
            }
            if (!PlayerPrefs.HasKey(Name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "未存取无法读取"));
            }
        }
    }
    public void DeletRTPrefab()
    {
        for(int i = 0; i< Selection.activeTransform.childCount;)
        {
            GameObject obj = Selection.activeTransform.GetChild(0).gameObject;
            DestroyImmediate(obj);
        }
    }

    //自动编写代码，生成gameObject右键菜单栏GUI代码
    public static void CreatGameObjectMenuCtrl(string name, string path)
    {
        string Filename = name + "_MenuSrc";
        string classname = name + "_MenuCtrl";
        string MenuPath = "GameObject / CreatPrefabRes /" + name;

        StreamWriter sw = new StreamWriter("Assets/RTRec/Editor/GameObjectMenuCtrl/" + Filename + ".cs");

        sw.WriteLine("using UnityEditor;\nusing UnityEngine;" + "\n");
        sw.WriteLine("public partial class " + classname + "{");
        sw.WriteLine("\t" + "[MenuItem(" + "\"" + MenuPath + "\"" + ")]");
        sw.WriteLine("\t" + "static void Run(){");
        sw.WriteLine("\t\t" + "GameObject res = (GameObject)AssetDatabase.LoadAssetAtPath(" + "\"" + path + "\"" + ",typeof(GameObject));");
        sw.WriteLine("\t\t" + "Object.Instantiate(res, Selection.activeTransform);");
        sw.WriteLine("\t\t" + "Undo.RegisterCreatedObjectUndo(res," + "\"" + "Create" + "\"" + " + res.name);");
        sw.WriteLine("\t\t" + "Selection.activeObject = res;");
        sw.WriteLine("\t" + "}");
        sw.WriteLine("}");
        sw.Flush();
        sw.Close();
        if (File.Exists("Assets/RTRec/Editor/GameObjectMenuCtrl/" + Filename + ".cs"))
        {
            return;
        }
    }
    private void OnSelectionChange()
    {
        this.Repaint();
    }

}
