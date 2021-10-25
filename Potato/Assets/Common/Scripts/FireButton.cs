using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    private float timer = 0.0f;
    private float fullDrawTime = 5.0f;


    public void StartDraw()
    {
        if (timer == 0.0f)
        {
            timer = Time.deltaTime;
        }
    }

    public void FinishDraw()
    {
        float drawTime = Time.deltaTime - timer;
        if (drawTime >= fullDrawTime)
        {
            drawTime = fullDrawTime;
        }

        print(drawTime);
        float arrowSpeed = drawTime;
    }
}
