using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public UIButtonEvents UI;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("firstTime", 1);

        //PlayerPrefs.DeleteAll();
        //adsplugin.instance.RequestInterstitial();
        //adsplugin.instance.CreateAndLoadRewardedAd();
        UI.LoadScene("00_MainMenu");
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("firstTime", 0);
    }
}
