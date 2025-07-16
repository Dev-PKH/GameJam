using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    public static GraphicManager Instance { get; private set; }

    // ScreenMode
    public Toggle screenModeToggle;
    private FullScreenMode screenMode = FullScreenMode.FullScreenWindow;

    // Resolution
    public int userWidth; // 사용자 가로 해상도
    public int userHeight; // 사용자 세로 해상도
    public int curWidth = 1920;
    public int curHeight = 1080;

    public int seletedResolution = 0;
    public List<ResolutionSize> resolutions;
    private List<ResolutionSize> filteredResolutions;
    public TextMeshProUGUI resolutionText;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        //screenModeToggle.isOn = Screen.fullScreen;
        userWidth = Display.main.systemWidth;
        userHeight = Display.main.systemHeight;
        curWidth = userWidth;
        curHeight = userHeight;

        FilterResolutionList();
        ApplyResolution();
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScreen()
    {
        if(screenModeToggle.isOn)
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            screenMode = FullScreenMode.Windowed;
        }

        ApplyResolution();
    }

    public void LeftButton()
    {
        seletedResolution--;
        if (seletedResolution < 0) seletedResolution = 0;

        ApplyResolution();
    }

    public void RightButton()
    {
        seletedResolution++;
        if (seletedResolution > filteredResolutions.Count -1) seletedResolution = filteredResolutions.Count -1;

        ApplyResolution();
    }

    private void FilterResolutionList()
    {
        filteredResolutions = resolutions.FindAll(r => r.width <= curWidth && r.height <= curHeight);
        filteredResolutions.Sort((a, b) => b.width.CompareTo(a.width));
    }

    public void ApplyResolution()
    {
        curWidth = filteredResolutions[seletedResolution].width;
        curHeight = filteredResolutions[seletedResolution].height;

        resolutionText.text = curWidth.ToString() + " X " + curHeight.ToString();

        Screen.SetResolution(curWidth, curHeight, screenMode);
    }


}

[Serializable]
public class ResolutionSize
{
    public int width, height;
}
