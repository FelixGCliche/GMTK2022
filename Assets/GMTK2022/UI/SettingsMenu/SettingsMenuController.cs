using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    void Start()
    {
        //Set Resolution Dropdown
        resolutions = Screen.resolutions;
        System.Array.Reverse(resolutions);
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width+" x "+ resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width==Screen.width &&
            resolutions[i].height==Screen.height)
            {
                currentResolutionIndex = i;
                
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        //Set Fullscreen Toggle
        fullscreenToggle.isOn = Screen.fullScreen;

    }

    public void SetLevel (float sliderValue)
    {
        audioMixer.SetFloat("VolMixer", Mathf.Log10(sliderValue) * 20);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
}

