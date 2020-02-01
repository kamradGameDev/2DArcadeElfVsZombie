using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolutionSprite : MonoBehaviour
{
    void Start()
    {
        if(Screen.width == 1280 & Screen.height == 720 || Screen.width == 1920 & Screen.height == 1080)
        {
            Camera.main.orthographicSize = 49f;
        }

        if(Screen.width == 2160 & Screen.height == 1080)
        {
            Camera.main.orthographicSize = 43.5f;
        }
    }

    void Update()
    {
        if(Screen.width == 1280 & Screen.height == 720 || Screen.width == 1920 & Screen.height == 1080 || Screen.width == 2560 & Screen.height == 1440)
        {
            Camera.main.orthographicSize = 49f;
        }

        if(Screen.width == 800 & Screen.height == 480)
        {
            Camera.main.orthographicSize = 52;
        }

        if(Screen.width == 2160 & Screen.height == 1080 || Screen.width == 2960 & Screen.height == 1440)
        {
            Camera.main.orthographicSize = 44.46609f;
        }
    }
}
