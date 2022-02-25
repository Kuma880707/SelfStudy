using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.U2D;
using UnityEditor.U2D;
using System.Collections.Generic;

public class CreatAltas : Editor
{
    static Object textrue;
    [MenuItem("Assets/CreatAltas")]
    static void ChangeImageAddAltas()
    {
        List<Object> obj = new List<Object>();
        SpriteAtlas atlas = new SpriteAtlas();

        SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
        {
            blockOffset = 1,
            enableRotation = false,
            enableTightPacking = false,
            padding = 2,
        };
        atlas.SetPackingSettings(packSet);
        SpriteAtlasTextureSettings textureSet = new SpriteAtlasTextureSettings()
        {
            readable = true,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear,
        };
        atlas.SetTextureSettings(textureSet);

        var select = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(select);
        string Atlasname = select.name + ".spriteatlas";
        string Atlaspath = "Assets/Dk_Project/Atlas/" + Atlasname;
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);           
        AssetDatabase.CreateAsset(atlas, Atlaspath);

        for (int i = 0,j = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta"))
            {
                continue;
            }
            j++;
            textrue = AssetDatabase.LoadAssetAtPath(path + "/" + files[i].Name, typeof(Texture2D));
            obj.Add(textrue);
        }      
        atlas.Add(obj.ToArray());
        AssetDatabase.SaveAssets();
        Debug.Log("图集已生成");
        //AssetImporter atlasAssetImporter = AssetImporter.GetAtPath(Atlaspath);
        //atlasAssetImporter.assetBundleName = direction.Name;
        //atlasAssetImporter.assetBundleVariant = "ps";
    }
    [MenuItem("Assets/CreatAltas",true)]
    static bool HideCreatAltas()
    {
        var select = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(select);
        return !string.IsNullOrEmpty(path) && !Path.HasExtension(path);
    }
}