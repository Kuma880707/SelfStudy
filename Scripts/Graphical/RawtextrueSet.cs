using UnityEngine;
using UnityEngine.UI;

public class RawtextrueSet : MonoBehaviour
{
    public  RawImage Rawtextrue;   
    private float GraphicsLerp;
    public BgNav bgNav;
    private RenderTexture rendertex;
    void Awake()
    {
        GraphicsLerp = GameMgr.Instance.graphicsSet;
        Rawtextrue = UIMgr.Instance.defaultUI.rawImage;
        bgNav.GraphicsLerp = GraphicsLerp;
        Camera cam = this.GetComponent<Camera>();
        int width = (int)Mathf.Ceil(Screen.width / GraphicsLerp);
        int height = (int)Mathf.Ceil(Screen.height / GraphicsLerp);
        rendertex = new RenderTexture(width, height, 0);
        cam.targetTexture = rendertex;     
        Rawtextrue.texture = rendertex;       
    }
    private void Start()
    {
        Destroy(this);
    }
}
