using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settings_4 : MonoBehaviour
{


    public Dropdown m_Dropdown, m_Dropdown_2;
    public Toggle m_Toggle;
    Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        m_Toggle.isOn = Screen.fullScreen;
        Rez();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        m_Toggle.isOn = Screen.fullScreen;
     
    }

    public void setQuality(int id)
    {
        QualitySettings.SetQualityLevel(id);
        PlayerPrefs.SetInt("gpu_lvl", id);
    }

    public void setResuliution(int id)
    {
        Resolution resolution = resolutions[id];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("rez_", id);
    }

    public void Rez()
    {

        resolutions = Screen.resolutions;
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
        PlayerPrefs.SetInt("rez_", currentresolutionIndex);

    }

}
