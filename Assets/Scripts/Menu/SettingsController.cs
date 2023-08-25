using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    private bool isFullScreen = true;
    public AudioMixer mixer;
    public Dropdown qualityDropdown;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown resolutionDropdown;
    float audioVolume;
    public Slider volumeSlider;

    private void Start() {
        int resolutionIndex = 0;
        LoadSettings(resolutionIndex);
    }

    private void Awake() {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach(var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions);
    }

    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }

    public void FullScreenToggle()
    {
       isFullScreen = !isFullScreen;
       Screen.fullScreen = isFullScreen; 
    }

    public void AudioVolume(float sliderValue)
    {
        mixer.SetFloat("masterVolume", sliderValue);
        audioVolume = sliderValue;
    }

    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("Volume", audioVolume);
    }

    public void LoadSettings(int resolutionIndex)
    {
        if(PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("Volume"));
        }

        if(PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        }
        else 
        {
            qualityDropdown.value = 3;
        }

        if(PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else 
        {
            resolutionDropdown.value = resolutionIndex;
        }

        if(PlayerPrefs.HasKey("FullScreenPreference"))
        {
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
        }
        else 
        {
            Screen.fullScreen = true;
        }
    }
}
