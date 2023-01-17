using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject Pausepanel;

    void Start()
    {
        Pausepanel.SetActive(false);
    }

    public void ShowPause() 
    {
        Pausepanel.SetActive(true);
        Time.timeScale = 0;

        //GA Event
        //MyAnalytics.instance.ScreenEvent("PauseScreen");
    }

    public void HidePause()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
    }
}
