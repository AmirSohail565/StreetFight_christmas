using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehavior : MonoBehaviour
{
    public Slider SFX;
    public Slider Music;
    public GameSettings settings;

    /*private void Awake()
    {
        SFX.value = PlayerPrefs.GetFloat("Volume", 1);
        Music.value = PlayerPrefs.GetFloat("Music", 1);
    }*/

    private void OnEnable()
    {
        SFX.value = PlayerPrefs.GetFloat("Volume", 1);
        Music.value = PlayerPrefs.GetFloat("Music", 1);

        //GA Event
        //MyAnalytics.instance.ScreenEvent("SettingsScreen");
    }

    public void changeSFX() 
    {
        PlayerPrefs.SetFloat("Volume", SFX.value);
        GlobalAudioPlayer.audioPlayer.applySettings();
    }

    public void changeMusic() 
    {
        PlayerPrefs.SetFloat("Music", Music.value);
        GlobalAudioPlayer.audioPlayer.applySettings();
    }
}