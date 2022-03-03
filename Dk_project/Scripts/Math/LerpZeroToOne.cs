using UnityEngine;
public class LerpZeroToOne
{
    public bool start;
    public float value;
    public float Speed ;
    public float deltaT;
    public void Update()
    {
        if (value > 1)
        {
            start = false;
            value = 1;
        }
        if (value < 0)
        {
            start = false;
            value = 0;
        }
        if(start)
        {
            value += deltaT * Speed;
        }  
    }
 }
