using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateUsManager : MonoBehaviour
{
    public Button RateNowDum;
    public Button RateNowReal;
    public Transform[] Stars;

    void OnEnable()
    {
        RateNowDum.gameObject.SetActive(true);
        RateNowReal.gameObject.SetActive(false);

        RateNowDum.interactable = false;
        RateNowReal.interactable = false;

        foreach (Transform star in Stars) 
        {
            star.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Rate(int index) 
    {
        for (int i = 0; i < Stars.Length; i++) 
        {
            if (i <= index)
            {
                Stars[i].GetChild(0).gameObject.SetActive(true);
            }
            else 
            {
                Stars[i].GetChild(0).gameObject.SetActive(false);
            }
        }

        if (index > 2)
        {
            RateNowDum.gameObject.SetActive(false);
            RateNowReal.gameObject.SetActive(true);

            RateNowDum.interactable = false;
            RateNowReal.interactable = true;
        }
        else 
        {
            RateNowDum.gameObject.SetActive(true);
            RateNowReal.gameObject.SetActive(false);

            RateNowDum.interactable = true;
            RateNowReal.interactable = false;
        }
    }

    public void RatePlayStore()
    {
        PlayerPrefs.SetInt("Rated",1);
        //Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        //print("https://play.google.com/store/apps/details?id=" + Application.identifier);
        gameObject.SetActive(false);
    }

    public void RateDum()
    {
        PlayerPrefs.SetInt("Rated", 1);
        gameObject.SetActive(false);
    }

    public void RateLater() 
    {
        //PlayerPrefs.SetInt("Rated", 0);
        gameObject.SetActive(false);
    }
}