using UnityEngine;
using UnityEngine.UI;

public class GraphicsSet : MonoBehaviour
{
    public  RawImage Rawtextrue;   
    public float GraphicsLerp;
    public BgNav bgNav;
    private RenderTexture rendertex;
    void Awake()
    {
        bgNav.GraphicsLerp = GraphicsLerp;

        Camera cam = this.GetComponent<Camera>();
        int width = (int)Mathf.Ceil(Screen.width / GraphicsLerp);
        int height = (int)Mathf.Ceil(Screen.height / GraphicsLerp);
        rendertex = new RenderTexture(width, height, 0);
        cam.targetTexture = rendertex;
        Rawtextrue.gameObject.SetActive(true);
        Rawtextrue.texture = rendertex;
        Rawtextrue.rectTransform.sizeDelta = new Vector2(Screen.width,Screen.height);
    }
    private void Start()
    {
        Destroy(this);
    }
}
