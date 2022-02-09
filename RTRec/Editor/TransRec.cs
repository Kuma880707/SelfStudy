using UnityEditor;
using UnityEngine;
using System.IO;

public class TransRec : EditorWindow
{
    public string stringToEdit = "";
    public void OnGUI()
    {
        GUILayout.Label("ע�⣺�����¼δ����ʱ��ȡ���������ʱ��Ҫ�޸ĸ��ӽṹ");
        if (GUILayout.Button("���Լ�������������"))
        {
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount != 0)
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    CreateRTPrefabSave();
                }
                GamePosRecorder.AllTransSave();
                Debug.Log("����ɹ�");
                name = Selection.activeTransform.name;
                PlayerPrefs.SetString("GUI" + name, name);
                Debug.Log("����������" + name);
            }
            if (!EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "�������޷�����"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��ѡ���޷�����"));
            }
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount == 0)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "���������޷�����"));
            }
        }
        GUILayout.Label("ע�⣺�½��������CreateRTPrefab��ſ��Ա�����Ͷ�ȡ");

        if (Selection.activeTransform != null)
        {
            GUILayout.Label("��ѡ��������" + Selection.activeTransform.name);
        }
        if (GUILayout.Button("��ȡ��¼"))
        {
            if (!EditorApplication.isPlaying && Selection.activeTransform != null && PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    CreateRTPrefabLoad();
                }
                GamePosRecorder.OriTransSave();
                GamePosRecorder.TransLoad();
                Debug.Log("��ȡ�ɹ�");
            }
            if (EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "�����޷���ȡ"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��ѡ���޷���ȡ"));
            }
            if (!PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "ѡ����������һ��"));
            }

        }
        if (GUILayout.Button("����ԭλ"))
        {
            if (!EditorApplication.isPlaying && Selection.activeTransform != null && PlayerPrefs.HasKey("-1") && PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                if (Selection.activeTransform.name == "CreateRTPrefab")
                {
                    DeletRTPrefab();
                }
                GamePosRecorder.OriTransLoad();
                Debug.Log("��λ�ɹ�");
            }
            if (EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "�����޷���λ"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��ѡ���޷���λ"));
            }
            if (!PlayerPrefs.HasKey("-1"))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "δ��ȡ���踴λ"));
            }
            if (PlayerPrefs.HasKey("-1") && !PlayerPrefs.HasKey("GUI" + Selection.activeTransform.name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "ѡ����������һ��"));
            }
        }

        if (GUILayout.Button("����浵"))
        {
            int count = PlayerPrefs.GetInt("PrefabRes");
            for (int i =0,h = 1; i< count;i++,h++)
            {
                string name = PlayerPrefs.GetString("PrefabRes" + "" + h);
                AssetDatabase.DeleteAsset("Assets/RTRec/Editor/GameObjectMenuCtrl/" + name + "_MenuSrc" + ".cs");
            }                
            PlayerPrefs.DeleteAll();        
            Debug.Log("����ɹ�");
        }

        GUILayout.Label("PrefabPath");
        stringToEdit = GUILayout.TextField(stringToEdit);
        if (GUILayout.Button("��ȡ·�������ɽű�"))
        {
            string path = string.Format(stringToEdit);
            if (Directory.Exists(path))
            {
                GetFileNameINPath(path);
            }
            else
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "·������"));
            }
        }
        GUILayout.Label("ע�⣺���ɽű����´ε��������������");
        GUILayout.Label("������ɺ����л�����һ�����±���");
        GUILayout.Label("�����������岻Ҫ������");
    }
    public void GetFileNameINPath(string path)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            Debug.Log(string.Format("<color=#ff0000>{0}</color>", "·�������ļ�"));
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
                //�Զ���д���룬����gameObject�Ҽ��˵���GUI����
                CreatGameObjectMenuCtrl(filename, path + "/" + files[i].Name);
                //Debug.Log(PlayerPrefs.GetInt("PrefabRes"));
                //Debug.Log(PlayerPrefs.GetString("PrefabRes" + "" + j));
                //      
                //Debug.Log(files[i].Name + "�������");
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
                Debug.Log("prefab��ȡ�ɹ�");
                //for (int i = 0; i < PrefabName.Count; i++)
                //{
                //    Debug.Log(PlayerPrefs.GetInt(PrefabName[i]));
                //}
            }
            if (!EditorApplication.isPlaying)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "�������޷�����"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��ѡ���޷�����"));
            }
            if (EditorApplication.isPlaying && Selection.activeTransform != null && Selection.activeTransform.childCount == 0)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "���������޷�����"));
            }
        }
        else
        {
            Debug.Log("·���ļ������¼�¼");
        }
    }
    public void CreateRTPrefabLoad()
    {
        string path = string.Format(stringToEdit);
        if (!PlayerPrefs.HasKey("PrefabRes"))
        {
            Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��·���ļ���¼�޷���ȡ"));
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
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "�����޷���ȡ"));
            }
            if (!Selection.activeTransform)
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "��ѡ���޷���ȡ"));
            }
            if (!PlayerPrefs.HasKey(Name))
            {
                Debug.Log(string.Format("<color=#ff0000>{0}</color>", "δ��ȡ�޷���ȡ"));
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

    //�Զ���д���룬����gameObject�Ҽ��˵���GUI����
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
