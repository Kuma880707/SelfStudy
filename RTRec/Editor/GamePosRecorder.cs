using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GamePosRecorder 
{
    public List<ObjTransform> obj = new List<ObjTransform>();
    public static void AllTransSave()
    {
        for (int i = 0; i < Selection.activeTransform.childCount; i++)
        {
            ObjTransform o = new ObjTransform();
            string n = Selection.activeTransform.GetChild(i).name;
            Vector3 vec = Selection.activeTransform.GetChild(i).position;
            Vector3 angle = Selection.activeTransform.GetChild(i).eulerAngles;
            Vector3 scale = Selection.activeTransform.GetChild(i).localScale;
            o.name = n;
            o.pos = vec;
            o.euler = angle;
            o.scale = scale;
            string json_string = JsonUtility.ToJson(o);
            PlayerPrefs.SetString(o.name + ""+i, json_string);         
        }
        //ObjTransform O = new ObjTransform();
        //string N = Selection.activeTransform.name;
        //Vector3 Vec = Selection.activeTransform.position;
        //Vector3 Angle = Selection.activeTransform.eulerAngles;
        //Vector3 Scale = Selection.activeTransform.localScale;
        //O.name = N;
        //O.pos = Vec;
        //O.euler = Angle;
        //O.scale = Scale;
        //string Json_string = JsonUtility.ToJson(O);
        //PlayerPrefs.SetString(O.name, Json_string);
    }
    public static void OriTransSave()
    {
        if (Selection.activeTransform.childCount != 0)
        {
            for (int i = 0; i < Selection.activeTransform.childCount; i++)
            {
                ObjTransform o = new ObjTransform();
                string n = Selection.activeTransform.GetChild(i).name;
                Vector3 vec = Selection.activeTransform.GetChild(i).position;
                Vector3 angle = Selection.activeTransform.GetChild(i).eulerAngles;
                Vector3 scale = Selection.activeTransform.GetChild(i).localScale;
                o.name = n;
                o.pos = vec;
                o.euler = angle;
                o.scale = scale;
                string json_string = JsonUtility.ToJson(o);
                PlayerPrefs.SetString(""+i, json_string);
            }
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            Vector3 Vec = Selection.activeTransform.position;
            Vector3 Angle = Selection.activeTransform.eulerAngles;
            Vector3 Scale = Selection.activeTransform.localScale;
            O.name = N;
            O.pos = Vec;
            O.euler = Angle;
            O.scale = Scale;
            string Json_string = JsonUtility.ToJson(O);
            PlayerPrefs.SetString("-1", Json_string);
        }
        else
        {
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            Vector3 Vec = Selection.activeTransform.position;
            Vector3 Angle = Selection.activeTransform.eulerAngles;
            Vector3 Scale = Selection.activeTransform.localScale;
            O.name = N;
            O.pos = Vec;
            O.euler = Angle;
            O.scale = Scale;
            string Json_string = JsonUtility.ToJson(O);
            PlayerPrefs.SetString("-1", Json_string);
        }
    }
    public static void TransLoad()
    {
        if(Selection.activeTransform.childCount != 0)
        {
            for (int i = 0; i < Selection.activeTransform.childCount; i++)
            {
                ObjTransform o = new ObjTransform();
                string n = Selection.activeTransform.GetChild(i).name;
                string json_string = PlayerPrefs.GetString(n + "" + i);
                JsonUtility.FromJsonOverwrite(json_string, o);               
                Selection.activeTransform.GetChild(i).position = o.pos;
                Selection.activeTransform.GetChild(i).eulerAngles = o.euler;
                Selection.activeTransform.GetChild(i).localScale = o.scale;
            }
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            string Json_string = PlayerPrefs.GetString(N);
            JsonUtility.FromJsonOverwrite(Json_string, O);
            Selection.activeTransform.position = O.pos;
            Selection.activeTransform.eulerAngles = O.euler;
            Selection.activeTransform.localScale = O.scale;
        }
        else
        {
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            string Json_string = PlayerPrefs.GetString(N);
            JsonUtility.FromJsonOverwrite(Json_string, O);
            Selection.activeTransform.position = O.pos;
            Selection.activeTransform.eulerAngles = O.euler;
            Selection.activeTransform.localScale = O.scale;
        }
    }
    public static void OriTransLoad()
    {
        if (Selection.activeTransform.childCount != 0)
        {
            for (int i = 0; i < Selection.activeTransform.childCount; i++)
            {
                ObjTransform o = new ObjTransform();
                string n = Selection.activeTransform.GetChild(i).name;
                string json_string = PlayerPrefs.GetString(""+i);
                JsonUtility.FromJsonOverwrite(json_string, o);
                Selection.activeTransform.GetChild(i).position = o.pos;
                Selection.activeTransform.GetChild(i).eulerAngles = o.euler;
                Selection.activeTransform.GetChild(i).localScale = o.scale;
            }
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            string Json_string = PlayerPrefs.GetString("-1");
            JsonUtility.FromJsonOverwrite(Json_string, O);
            Selection.activeTransform.position = O.pos;
            Selection.activeTransform.eulerAngles = O.euler;
            Selection.activeTransform.localScale = O.scale;
        }
        else
        {
            ObjTransform O = new ObjTransform();
            string N = Selection.activeTransform.name;
            string Json_string = PlayerPrefs.GetString("-1");
            JsonUtility.FromJsonOverwrite(Json_string, O);
            Selection.activeTransform.position = O.pos;
            Selection.activeTransform.eulerAngles = O.euler;
            Selection.activeTransform.localScale = O.scale;
        }
    }


}
