using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    static float currentSoundVolume = 1f;
    static float currentMusicVolume = 1f;

    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private TMP_InputField soundVolumeField;
    
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TMP_InputField musicVolumeField;
    
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    private void Start()
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
                currentResolutionIndex = i;
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        //Set Fullscreen Toggle
        fullscreenToggle.isOn = Screen.fullScreen;

        soundVolumeSlider.value = currentSoundVolume;
        SetVolumeField(soundVolumeField, currentSoundVolume);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
        soundVolumeField.onValueChanged.AddListener(TrySetSoundVolumeFromString);

        musicVolumeSlider.value = currentMusicVolume;
        SetVolumeField(musicVolumeField, currentMusicVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        musicVolumeField.onValueChanged.AddListener(TrySetMusicVolumeFromString);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SetSoundVolume(float value)
    {
        currentSoundVolume = value;
        SetVolumeField(soundVolumeField, currentSoundVolume);
        SoundManager.Instance.SetSoundVolume(currentSoundVolume);
    }

    private void SetMusicVolume(float value)
    {
        currentMusicVolume = value;
        SetVolumeField(musicVolumeField, currentMusicVolume);
        SoundManager.Instance.SetMusicVolume(currentMusicVolume);
    }
    
    private void SetVolumeField(TMP_InputField volumeField, float value)
    {
        var volumeString = Mathf.FloorToInt(value * 100f).ToString();
        volumeField.text = volumeString;
    }

    private void TrySetSoundVolumeFromString(string value)
    {
        if (float.TryParse(value, out var volume))
        {
            currentSoundVolume = volume / 100;
            soundVolumeSlider.value = currentSoundVolume;
            SoundManager.Instance.SetSoundVolume(currentSoundVolume);
        }
    }

    private void TrySetMusicVolumeFromString(string value)
    {
        if (float.TryParse(value, out var volume))
        {
            currentMusicVolume = volume / 100;
            musicVolumeSlider.value = currentMusicVolume;
            SoundManager.Instance.SetSoundVolume(currentMusicVolume);
        }
    }
}

