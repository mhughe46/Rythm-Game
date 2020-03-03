using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    Resolution[] resolutions;

    [SerializeField]
    Dropdown resolutionDropdown = null;

    [SerializeField]
    Slider musicSlider = null;

    [SerializeField]
    Slider soundSlider = null;

    [SerializeField]
    GameObject optionsPanel = null;

    public bool isPaused = false;

    private void Start()
    {
        GetResolutions();

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            SetMusicLevel(PlayerPrefs.GetFloat("MusicVol"));
            print("Has Key");
        }

        if (PlayerPrefs.HasKey("SFXVol"))
            SetSoundLevel(PlayerPrefs.GetFloat("SFXVol"));

        if (PlayerPrefs.HasKey("IsFullscreen"))
            SetFullscreen(System.Convert.ToBoolean(PlayerPrefs.GetInt("IsFullscreen")));

        audioMixer.GetFloat("MusicVolume", out float musicVol);
        audioMixer.GetFloat("SFXVolume", out float sfxVol);
        musicSlider.value = Mathf.Pow(10, musicVol / 20f);
        soundSlider.value = Mathf.Pow(10, sfxVol / 20f);
    }

    private void Update()
    {
        if (optionsPanel != null && Input.GetButtonDown("Cancel"))
        {
            EnableOptions();
        }
    }

    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMusicLevel(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
        print("Music Volume " + sliderValue);
        PlayerPrefs.Save();
    }

    public void SetSoundLevel(float sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
        print("Sound Volume " + sliderValue);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", System.Convert.ToInt32(isFullscreen));
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void EnableOptions()
    {
        if (!optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
            GameObject.FindGameObjectWithTag("Processor").GetComponent<AudioSource>().Pause();
            isPaused = true;
            Time.timeScale = 0;
        }else if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().UnPause();
            GameObject.FindGameObjectWithTag("Processor").GetComponent<AudioSource>().UnPause();
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}
