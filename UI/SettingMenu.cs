using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class SettingMenu : MonoBehaviour
{
    public AudioSource bgm, sfx;
    public AudioSource[] voice;

    public Dropdown resolutionDropdown;

    public GameObject aboutPage, settingPage;


    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        List<string> options = new List<string>();
        for(int i = 0;i < resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].height == Screen.currentResolution.height &&
               resolutions[i].width == Screen.currentResolution.width)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetBgmVolume(float volume)
    {
        bgmControl.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfx.volume = volume;
    }
    public void SetVoiceVolume(float volume)
    {
        foreach(AudioSource AS in voice)
        {
            AS.volume = volume;
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int index)
    {
        resolutions = Screen.resolutions;
        if (resolutions != null)
        {
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            print("null");
        }
    }

    //Main Pause Menu

    public void Setting_Btn()
    {
        settingPage.SetActive(true);
    }

    //setting page
    public void Close_setting_btn()
    {
        this.gameObject.SetActive(false);
    }

    public void About_Btn()
    {
        aboutPage.SetActive(true);
    }

    public void Quit_Btn()
    {
        Application.Quit();
    }

    //About Page

    public void Close_about_btn()
    {
        aboutPage.SetActive(false);
    }

    public void YT_btn()
    {
        Application.OpenURL("https://youtube.com/@FlyDragonOuO");
    }

    public void IG_btn()
    {
        Application.OpenURL("https://www.instagram.com/kami_65665/");
    }
}
