using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class settings_4 : MonoBehaviour
{
    public Dropdown m_Dropdown_2;
    public Toggle m_Toggle;
    Resolution[] resolutions;

    void Awake()
    {
        Rez();
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        m_Toggle.isOn = Screen.fullScreen;
    }

    public void setResuliution(int id)
    {
        Resolution resolution = resolutions[id];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Rez()
    { resolutions = Screen.resolutions;
        m_Dropdown_2.ClearOptions();
        List<string> options = new List<string>();
        int currentresolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentresolutionIndex = i;
            }

        }
        m_Dropdown_2.AddOptions(options);
        m_Dropdown_2.value = currentresolutionIndex; 

    }

}
