
using UnityEngine;
using UnityEngine.UI;

public class DebugFPS : MonoBehaviour
{
    float FpsByDeltatime = 1.5f;
    int FrameCount;
    float PassingTime;
    float RealFps;

    private void Start()
    {
        SetFps();
    }
    // Update is called once per frame
    void Update()
    {
        GetFps();
    }
    void SetFps()
    {
        Application.targetFrameRate = 60;
    }
    void GetFps()
    {
        FrameCount++;
        PassingTime += Time.deltaTime;
        if(PassingTime > FpsByDeltatime)
        {
            RealFps = FrameCount / PassingTime;
            this.GetComponent<Text>().text = "" + RealFps;
            PassingTime = 0;
            FrameCount = 0;
        }

    }
}
