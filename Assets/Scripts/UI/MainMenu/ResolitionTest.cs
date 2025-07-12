using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolitionTest : MonoBehaviour
{
    // Start is called before the first frame update
    FullScreenMode screenMode = FullScreenMode.FullScreenWindow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Screen.SetResolution(1600, 900, screenMode);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Screen.SetResolution(1920, 1080, screenMode);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Screen.SetResolution(2560, 1440, screenMode);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            screenMode = FullScreenMode.Windowed;
        }
    }
}
