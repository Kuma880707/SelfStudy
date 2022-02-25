using UnityEngine;
public class FollowCamera : MonoBehaviour
{   
    [HideInInspector]
    public bool moveon;

    private void Awake()
    {
#if UNITY_EDITOR
        Transform parent = this.transform.parent;
        CameraEditor editor = parent.GetComponent<CameraEditor>();
        DestroyImmediate(editor);
#endif
    }
    public void CameraMove(Vector3 charactorPos)
    {
        if (moveon)
        {
            this.transform.localPosition = new Vector3(charactorPos.x + 220, charactorPos.y + 700, -170);
            if (charactorPos.x < -220)
            {
                this.transform.localPosition = new Vector3(0, charactorPos.y + 700, -170);
               
            }
             if (charactorPos.x > 220)
            {
                this.transform.localPosition = new Vector3(440 , charactorPos.y + 700, -170);
               
            }
             if(charactorPos.y < -400)
            {
                this.transform.localPosition = new Vector3(charactorPos.x + 220 , 300, -170);
               
            }
             if(charactorPos.y > 500)
            {
                this.transform.localPosition = new Vector3(charactorPos.x + 220, 1200, -170);
              
            }
        }

    }
    public void CameraMoveOn(Vector3 charactorPos)
    {
        if (charactorPos.x < -220 && charactorPos.y < -400)
        {
            moveon = false;
        }
        else if (charactorPos.x > 280 && charactorPos.y < -400)
        {
            moveon = false;
        }
        else if (charactorPos.x < -200 && charactorPos.y > 500)
        {
            moveon = false;
        }
        else if (charactorPos.x > 280 && charactorPos.y > 500)
        {
            moveon = false;
        }
        else
        {
            moveon = true;
        }
    }
}
